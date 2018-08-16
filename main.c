#include <24FV16KM202.h>
#DEVICE ADC=12

#device ICD=3
#include <math.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#FUSES FRC_PLL
#fuses SOSC_DIGITAL //SOSC pins set for digital mode for use with external clock or digital I/O
#fuses NOOSCIO //OSC2 is general purpose output
#fuses NOPR    //Primary oscillator disabled
#fuses LPRCHIGH   //Low-Power FRC High Power and High Accuracy

#use delay(clock = 32MHZ, internal = 8MHZ)
#USE RS232(UART2, BAUD = 115200, PARITY = N, BITS = 8, STOP = 1, TIMEOUT = 500))

#define SPI_CS PIN_B4
#define SPI_SCK PIN_A4
#define SPI_SI PIN_A2

#USE SPI(DO=SPI_SI, CLK=SPI_SCK, BITS=16, BAUD=115200, SAMPLE_RISE)

#define COEF_LENGTH 64
#define DATA_LENGTH 100

int1 serial_flag = 0, timer_flag = 0; char key;  // global flags and uart recieved character

signed int32 fir_coef[COEF_LENGTH];  // array for Filter coefficients array values in fixed-point notation
unsigned int32 input_samples[DATA_LENGTH]; // array used as a circular buffer for the input samples
unsigned int8 coef_index = 0; // used as the index for the filter coefficients array in the difference equation calculation
unsigned int8 input_index = 0; // used as the index for the input samples array in the difference equation calculation
unsigned int8 cur = 0; // keeps track of the curent position of the circular buffer
unsigned int8 temp_cur = 0;  // keeps track of the sending of the input/output data to the GUI
unsigned int8 output_data[DATA_LENGTH];  // array used as a circlular buffer for the outpat data
signed int32 accumulator = 0; // accumulator of the output value in the difference equation calculation
unsigned int8 out; // holds the current output value
int16 freq; // used to store the value to calculate the sampling frequency for the timer
int8 state = 0;   // 3 stated used Congiguration, Filterind and resetting
int1 neg_flag = 0, state_flag = 1;  //local flags used to track a negative number and the changing of states
int16 shift = 0, off_set = 0;  // used for the shifinting of the filtered input before going to dac and the DC amount to be added back in
int8 j = 0, k = 0;   // counters
signed int32 temp;   // used to track the incoming coefficient unitl it is all here and changed to an integer

#INT_RDA2
void isr_uart()   // recieving from the GUI
{
   key = getc(); serial_flag = 1;   // get the character recieved
   //printf("%c U ", key);
}

#INT_TIMER1
void  timer1_isr(void)  // used for sampling
{    
   output_toggle (PIN_A2);       //add additional tick to get to intterupt faster
   set_timer1(freq + get_timer1());  //since the time isn't equal to overflow
   timer_flag = 1;
}

void main()
{    
   // Setup ADC
   setup_adc(ADC_CLOCK_DIV_2 | ADC_TAD_MUL_4);
   setup_adc_ports(sAN0 | VSS_VDD);
   // Setup DAC
   setup_dac(1,DAC_REF_VDD | DAC_ON);
   setup_opamp1(OPAMP_ENABLED | OPAMP_PI_TO_DAC | OPAMP_NI_TO_OUTPUT | OPAMP_HIGH_POWER_MODE);   
   // Setup Timer to desired sampling frequency (Fs) (2KHz)
   clear_interrupt(INT_TIMER1);
   setup_timer1(T1_INTERNAL | T1_DIV_BY_1);   
   enable_interrupts(INT_TIMER1);
   enable_interrupts(GLOBAL);
   set_timer1(0);
   //setup serial interrupt
   enable_interrupts(INT_RDA2);
   enable_interrupts(INTR_GLOBAL);
   
   printf("Sarah Ngo");
     
   // Initialize the input samples array with zeros
   for(int i = 0; i < COEF_LENGTH; i++)
   {
      input_samples[i] =  0;
   }
    
   while(true)
   {
      while (state == 0) //obtain the coefficents
      {  
         if(state_flag) // display what is happening
         {
            printf("   Configuring  ");
            state_flag = 0;
         }
         if (serial_flag)  // transmission recieved
         { 
            if (key == 'X')
            {     // set the timer for a Fs of 1 KHz
                  freq = 49536; serial_flag = 0;
                  printf(" Sampling at 1 Khz ");
            }
            else if (key == 'Y')
            {     // set the timer for a Fs of 1.5 KHz 
                  freq = 54869; serial_flag = 0;
                  printf(" Sampling at 1.5 Khz ");
            }
            else if (key == 'Z')
            {     // set the timer for a Fs of 2 KHz
                  freq = 57536; serial_flag = 0;
                  printf(" Sampling at 2 Khz ");
            }
            else if(key >= '0' && key <= '9')
            {  // a number has been recived and needs to be converted
               key = key - '0';
               temp = 10*temp + key; 
               serial_flag = 0;
            }
            else if (key == '-')
            {     // flag that the number is a negative number
               neg_flag = 1;
               serial_flag = 0;
            }
            else if (key == ',')
            {     // end of the coefficient value
               if (neg_flag)
               {  // convert if negative then store
                  fir_coef[j] = -1*temp; 
                  neg_flag = 0;
               }
               else
               {     // store the value
                  fir_coef[j] = temp;
               }
               temp = 0; j++;
               serial_flag = 0;
            }
            else if (key == 'D')
            {  // transmission of the coefficients has finished
               if (neg_flag)
               {  // convert the negative and store the last value
                  fir_coef[j] = -1*temp; 
                  neg_flag = 0;
               }
               else
               {     // store the last vale
                  fir_coef[j] = temp;
               }
               state = 1; serial_flag = 0;  // move to next stae
               neg_flag = 0; j = 0;    // reset flags and counters
               off_set = 128;    // default dc offset value
               state_flag = 1;   // moving to a new state
               printf(" Offset = %d ", off_set);
               temp = 0;
            } 
            else if (key =='S')
            {  // stop the system move to state 2 and reset
               state = 2; serial_flag = 0;
               state_flag = 1;      // trasitioning states
            }            
            else if (key == 'L')
            {     // lowpass filter being used 20 default shift value
                  shift = 21; serial_flag = 0;
                  printf(" Shift = %d ", shift);
            }
            else if (key == 'B')
            {     // bandpass filter being used 19 is default shift value
                  shift = 19; serial_flag = 0;
                  printf(" Shift = %d ", shift);
            }          
         }
      }
    
      while (state == 1)  // Filtering Sate
      {
         if (state_flag)   // display what is happening
         {
            printf("   Filtering  "); state_flag = 0;
         }
         if(serial_flag)   // transmission recieved
         {
               if (key == 'S')
               {     // stop filtering and move to reset state
                  state = 2; 
                  serial_flag = 0; state_flag = 1;
               }
               else if (key == 'A')
               {     //increment the shift value by 1
                  if (shift>=0 && shift<32)
                  {
                     shift++;
                  }
                  printf("Shift = %d ", shift);
                  serial_flag = 0;
               }
               else if (key == 'C')
               {     // decrement the shift value by 1
                  if (shift>0 && shift<=32)
                  {
                     shift--;
                  }
                  printf(" Shift = %d ", shift);
                  serial_flag = 0;
               }
               else if (key == 'G')
               {     //increment the dc offset value by 1
                  if (off_set>=0 && off_set<128)
                  {
                     off_set++;
                  }
                  printf(" Offset = %d ", off_set);
                  serial_flag = 0;
               }
               else if (key == 'H')
               {     //decrement the dc offset value by 1
                  if (off_set>0 && off_set<=128)
                  {
                     off_set--;
                  }
                  printf(" Offset = %d ", off_set);
                  serial_flag = 0;
               }
               else if (key == 'I')
               {  //increment the dc offset value by 10
                  if (off_set>=0 && off_set<119)
                  {
                     off_set += 10;
                  }
                  printf(" Offset = %d ", off_set);
                  serial_flag = 0;
               }
               else if (key == 'J')
               {  // dencrement the dc offset value by 10
                  if (off_set>=10 && off_set<=128)
                  {
                     off_set -= 10;
                  }
                  printf(" Offset = %d ", off_set);
                  serial_flag = 0;
               }
               else if (key == 'P')
               {     // sent the array of input samples
                  temp_cur = cur;
                  printf("%i,", input_samples[temp_cur]);
                  temp_cur--;
                  while (temp_cur != cur) // keep sending until back to start
                  {
                     printf("%i,",input_samples[temp_cur--]);
                     if(temp_cur == 0)
                     {
                       temp_cur = DATA_LENGTH-1;
                     }
                  }
                  printf("@");   // done sending the inputs
                  serial_flag = 0;
               }
               else if (key == 'M')
               {     // send the array of output samples
                  temp_cur = cur;
                  printf("%u,", output_data[temp_cur--]);
                  while (temp_cur != cur)    // keep sending until back to start
                  {
                     printf("%u,",output_data[temp_cur--]);                    
                     if(temp_cur == 0)
                     {
                       temp_cur = DATA_LENGTH-1;
                     }
                  }
                  printf("#");      //done sending the outputs
                  serial_flag = 0;
               }
         }
        while (k < COEF_LENGTH)
        {   // send coefficient values back to be displayed in the GUI
            printf(" %d", fir_coef[k++]);           
        }
        
         //start = get_timer1();
         if(timer_flag)    // timer interrupt happened so get sample and filter
         {
            timer_flag = 0;
            input_samples[cur] =  read_adc(ADC_START_AND_READ);   // store input value
      
            input_index = cur;
            accumulator = 0;
            coef_index = 0;
            while(coef_index < COEF_LENGTH)
            {     // filter
               accumulator += input_samples[input_index]*fir_coef[coef_index];
               
               // condition for the circular buffer
               if(input_index == DATA_LENGTH - 1)  // circle around
                  input_index = 0;
               else
                  input_index++;
            
               coef_index++;
            }
      
            // Scale the output value from 16-bit integer to 8-bit integer
            // 256/32768 = 0.007812
            // Offset (128) may not be needed depending on the offset of the input signal
            out = (accumulator >> shift) + off_set;      // shift to 8 bits and add back in dc offset
            dac_write(1,out);    // send the value to the dac
            output_data[cur] = out;    // store the output value
            
            // condition for the circular buffer
            if(cur == 0)
               cur = DATA_LENGTH - 1;  // circle around
            else
               cur--;
   
         //end = get_timer1();
         //printf("\r\nFs = %f Hz",(1.0/((float)(end-start)))*1.6*pow(10,7));
         //delay_ms(500);
         }
      }
      
      while (state == 2)  // reset state
      { 
         if (state_flag)
         {
            printf("   Resetting  "); state_flag = 0;
         }
         // Initialize the input samples array with zeros
         for(int i = 0; i < DATA_LENGTH; i++)   
         {     // reset the input, output arrays
            input_samples[i] =  0;
            output_data[i] = 0;          
         }  
         for(int n = 0; n < COEF_LENGTH; n++)   
         {     // reset the coefficient arrays
            fir_coef[n] =  0;          
         }  
          state = 0; state_flag = 1; // transition to new state
          k = 0;  // reset counter
      }
   }
}

