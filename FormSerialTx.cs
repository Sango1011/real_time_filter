using System;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using  static System.String;
using System.Threading.Tasks;
using ZedGraph;
using System.Drawing;

namespace SerialPort_Demo
{
    /// <summary>
    /// Simple Serial Communication
    /// 
    /// Reference URLs:
    /// 
    /// Example codes: http://csharp.simpleserial.com/
    /// Official MSDN documentation: https://msdn.microsoft.com/en-us/library/system.io.ports.serialport.aspx
    /// 
    /// Keywords: "C# SerialPort", "C# serial port"
    /// </summary>
    public partial class FormSerialTx : Form
    {
        private string _rxString;       //variable for incoming data
        private bool stop_flag = false; //used to only allow changes to the system after stop has been pressed
        private bool error_flag = false;    //goes high when missing information
        private string file;    //used to sotre the string of the file read
        private char[] data;    //used to converting the file string to char array
        private int shift = 0, off = 128;   //varialbes used for displaying in GUI
        private int[] in_array = new int[100];  //used for graphing input data
        private int[] out_array = new int[100]; //used for graphing output data
        private string in_string;       //incoming string of input data
        private string out_string;      //incoming string of outpit data
        private string[] input;     //input data split into array of strings
        private string[] output;    //output data split into array of strings
        OpenFileDialog theDialog = new OpenFileDialog();        //used to open a window to browse for a file on the computer

        public void CreateChart(ZedGraphControl zedGraphControl1)
        {
            //Declare a new GraphPane object
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "Signal Processing";
            myPane.XAxis.Title.Text = "Sample";
            myPane.YAxis.Title.Text = "Input Amplitude";
            myPane.Y2Axis.Title.Text = "Output Amplitude";

            // Make up some data points based on the data
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();

            //create the data points to be plotted
            for (int i = 0; i < in_array.Length -1; i++)
            {
                double x = (double)i;
                double y = in_array[i]*0.00136798;      //convert input to 0 to 5 volts
                double y2 = out_array[i]*0.0196078;     //convert outptut to 0 to 5 v
                list.Add(x, y);         //add points to the list
                list2.Add(x, y2);
            }

            // Note: All data being plotted by zedgraph have to be “Double” format.
            // Data should be saved as a PointPairList before plotting.
            // So “List.Add()” method should be called after you define or
            // change the data that are plotted.
            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem myCurve = myPane.AddCurve("Alpha", list, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);
            // Generate a blue curve with circle symbols, and "Beta" in the legend
            myCurve = myPane.AddCurve("Beta", list2, Color.Blue, SymbolType.Circle);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = true;
            //Some enhancing setting:
            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;
            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 5;
            myPane.Y2Axis.Scale.Min = 0;
            myPane.Y2Axis.Scale.Max = 5;
            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;
            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);
            // Calculate the Axis Scale Ranges
            //Note: ZedGraphControl.AxisChange() command keep checking and
            // adjusting the display axis setting according to the List changes.
            zedGraphControl1.AxisChange();
        }

        public FormSerialTx()
        {
            InitializeComponent();
            //disable shift and offset buttons 
            Shift_P1.Enabled = false;       
            Shift_M1.Enabled = false;
            Off_M1.Enabled = false;
            button1.Enabled = false;
            Off_M10.Enabled = false;
            Off_P10.Enabled = false;
        }

        private void Connect(SerialPort handle)
        {
            //Configure serial port
            serialPort1.PortName = "COM5";
            serialPort1.BaudRate = 9600;

            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();  //attempt to open the configured serial port
                    if (serialPort1.IsOpen)
                    {
                        //Com Port is connected
                        connectToolStripMenuItem.Enabled = false;
                        disconnectToolStripMenuItem.Enabled = true;
                        tbAscii.ReadOnly = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to open serial port");
                }
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //handle serial port connect
            Connect(serialPort1);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                //Disconnect serial port
                serialPort1.Close();
                connectToolStripMenuItem.Enabled = true;
                disconnectToolStripMenuItem.Enabled = false;
                tbAscii.ReadOnly = true;
            }
        }

        private void FormSerialTx_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Note: SerialPort object operates on a seperate thread.
            //      Therefore, DataReceived event can not interact directly with other WinForm controls.
            //      Doing so will cause cross-thread action exception.
            //      In order to display received data to UI, delegate method must be used.

            //Read data from serial port object
            string data = serialPort1.ReadExisting();

            //Choose one of the approaches below

            //Approach 1: call Invoke method and pass data via external variable
            _rxString = data;  //assign to external variable
            
            this.Invoke(new EventHandler(ShowText));
            
            if (_rxString.Contains("@"))
            {
                Invoke(new EventHandler(Input_Convert));
            }

            if (_rxString.Contains("#"))
            {
                Invoke(new EventHandler(Output_Convert));
            }

            //Approach 2: pass data via parameter
            //this.Invoke(new EventHandler(DisplayText), new object[] { data });
        }

        //private void DisplayText(object sender, EventArgs e)
        //{
        //    tbAscii.AppendText((string)sender);
        //}

        private void Input_Convert(object sender, EventArgs e)
        {
            in_string = tbAscii.Text;       //transfer the text from the textbox into a string
            in_string = in_string.Substring(0, in_string.IndexOf('@') - 1); //get rid of ending char
            input = in_string.Split(',');   //split based on , into an array of small strings
            for (int i = 0; i < input.Length; i++)
            {       //convert each small string into an integer
                in_array[i] = Convert.ToInt32(input[i]);
            }
        }

        private void Output_Convert(object sender, EventArgs e)
        {
            out_string = tbAscii.Text;      //transfer the text from the textbox into a string
            out_string = out_string.Substring(0, out_string.IndexOf('#') - 1);  //get rid of ending char
            output = out_string.Split(',');     //split based on , into an array of small strings
            for (int i = 0; i < output.Length; i++)
            {       //convert each small string into an integer
                out_array[i] = Convert.ToInt16(output[i]);
            }
        }
        private void ShowText(object sender, EventArgs e)
        {
            tbAscii.AppendText(_rxString);      //display incoming data
        }

        private void tbAscii_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] key = new char[1];
            key[0] = e.KeyChar;  //Read keypress character

            byte[] buffer = Encoding.ASCII.GetBytes(key);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(key, 0, 1);
            }

            // Set the KeyPress event as handled so the character won't
            // display locally. If you want it to display, comment out the next line.
            //e.Handled = true;
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbAscii.Clear();
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            //reset dropdowns to none and clear textboxes
            Passband.Text = "(none)"; tbAscii.Clear();
            SampFreq.Text = "(none)"; ShiftText.Clear();
            theDialog.FileName = ""; OffsetText.Clear();
            FilePath.Text = theDialog.FileName;
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("S"); //send stop flag to chip
            }
            //reset stop flag
            stop_flag = false;
            //disable buttons
            Shift_P1.Enabled = false;
            Shift_M1.Enabled = false;
            Off_M1.Enabled = false;
            button1.Enabled = false;
            Off_M10.Enabled = false;
            Off_P10.Enabled = false;
        }

        private void Passband_SelectedIndexChanged(object sender, EventArgs e)
        {       //value handeled in send button
        }

        private void SampFreq_SelectedIndexChanged(object sender, EventArgs e)
        {       //value handled in send button
        }

        private void Send_btn_Click(object sender, EventArgs e)
        {
            if (stop_flag == false) //stop button pressed and ready for new data
            {
                error_flag = false;
                OffsetText.Clear();
                OffsetText.AppendText("128");   //default offset & display
                if (Passband.Text == "Lowpass")
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("L"); //send lowpass flag to chip
                        Thread.Sleep(20);
                    }
                    shift = 21;     //default shift value
                    ShiftText.Clear();
                    ShiftText.AppendText("21");    //display 21
                }
                else if (Passband.Text == "Bandpass")
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("B");     //send bandpass flag to chip
                        Thread.Sleep(20);                    
                    }
                    shift = 19;     //default shift value
                    ShiftText.Clear();
                    ShiftText.AppendText("19"); //display value
                }
                else if(Passband.Text == "(none)")
                {   //send error if nothing selecting
                    error_flag = true;
                    MessageBox.Show("A Passband Must be Selected.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (SampFreq.Text == "1 KHz")
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("X");   //send flag
                        Thread.Sleep(20);
                    }
                }
                else if (SampFreq.Text == "1.5 KHz")
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("Y"); //send falg
                        Thread.Sleep(20);
                    }
                }
                else if (SampFreq.Text == "2 KHz")
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("Z"); //send flag
                        Thread.Sleep(20);
                    }
                }
                else if (SampFreq.Text == "(none)")
                {       //send error if nothing selecting
                    error_flag = true;
                    MessageBox.Show("A Sampling Frequency Must be Selected.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (FilePath.Text == "")
                {       //send error if nothing selecting
                    MessageBox.Show("A File Must be Selected.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error_flag = true;
                }

                if (!error_flag)
                {      //open and read the file
                    file = File.ReadAllText(theDialog.FileName);
                    file = file.Substring(file.IndexOf('{') + 2);   //delete the { and everything before
                    file = file.Substring(0, file.IndexOf('}') - 1);    //delete the } and everything after
                    file = file.Replace(" ","");    //relpace spaces with nothing
                    file = file.Replace("\n", "");  //replace end line with nothing
                    data = file.ToCharArray();  //convert to char array
                    for (int i = 0; i < data.Length; i++)
                    {   //send on char at a time
                        if (serialPort1.IsOpen)
                        {
                            string c = data[i].ToString();
                            serialPort1.Write(c);
                            Thread.Sleep(20);
                        }
                    }
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("D");     //send done flag
                    }
                    stop_flag = true;   //wait for stop button to be pressed
                    theDialog.FileName = "";    //clear file name
                    FilePath.Text = theDialog.FileName;
                    //enable shift and off set buttons
                    Shift_P1.Enabled = true;
                    Shift_M1.Enabled = true;
                    Off_M1.Enabled = true;
                    button1.Enabled = true;
                    Off_M10.Enabled = true;
                    Off_P10.Enabled = true;
                }
            }
           
        }

        private void BrowseFiles_Click(object sender, EventArgs e)
        {       //function to browse computer for a file
            theDialog.Title = "Open Text File";
            theDialog.Filter = "Header Files (*.h*)|*.h*";  //only header files
            if(theDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = theDialog.FileName; //store the path
            }
        }

        private void ShiftText_TextChanged(object sender, EventArgs e)
        {           
        }

        private void Shift_P1_Click(object sender, EventArgs e)
        {       //button to increment shift by 1
            if (shift >= 0 && shift < 32)
            {
                shift = shift + 1;
            }
            ShiftText.Clear();
            string n = Convert.ToString(shift);
            ShiftText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("A"); //send flag
                Thread.Sleep(10);
            }
        }

        private void Shift_M1_Click(object sender, EventArgs e)
        {   //button todecrement shift by 1
            if (shift > 0 && shift < 32)
            {
                shift = shift - 1;
            }
                ShiftText.Clear();
            string n = Convert.ToString(shift);
            ShiftText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("C");     //send flag
                Thread.Sleep(10);
            }
        }

        private void OffsetText_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {       //button to increment offset by 1
            if (off >= 0 && off < 128)
            {
                off = off + 1;
            }
            OffsetText.Clear();
            string n = Convert.ToString(off);
            OffsetText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("G");  //send flag
                Thread.Sleep(10);
            }
        }

        private void Off_M10_Click(object sender, EventArgs e)
        {   //button to decrement offset by 10
            if (off >= 10 && off <= 128)
            {
                off = off - 10;
            }
            OffsetText.Clear();
            string n = Convert.ToString(off);
            OffsetText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("J");  //send flag
                Thread.Sleep(10);
            }
        }

        private void Off_P10_Click(object sender, EventArgs e)
        {       //button to increment offset by 10
            if (off >= 0 && off <= 118)
            {
                off = off + 10;
            }
            OffsetText.Clear();
            string n = Convert.ToString(off);
            OffsetText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("I");     //send flag
                Thread.Sleep(10);
            }
        }

        private void Off_M1_Click(object sender, EventArgs e)
        {       //button to decrement offset by 1
            if (off > 0 && off < 129)
            {
                off = off - 1;
            }
            OffsetText.Clear();
            string n = Convert.ToString(off);
            OffsetText.AppendText(n);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("H");     //send flag
                Thread.Sleep(10);
            }
        }

        private void Get_Input_Click(object sender, EventArgs e)
        {   //request input data from the chip
            tbAscii.Clear();
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("P");  //send flag
            }
        }

        private void Get_Output_Click(object sender, EventArgs e)
        {   //request ouput data from the chip
            tbAscii.Clear();
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("M"); //send flag
            }
        }

        private void Plot_Click(object sender, EventArgs e)
        {  
            zedGraphControl1.GraphPane.CurveList.Clear();   //clear the graph
            CreateChart(zedGraphControl1);  //create the graph
            zedGraphControl1.AxisChange();  //change the axis
            zedGraphControl1.Invalidate();  //confirm
            zedGraphControl1.Refresh();     //update
        }
    }
}
