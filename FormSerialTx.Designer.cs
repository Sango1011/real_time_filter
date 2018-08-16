namespace SerialPort_Demo
{
    partial class FormSerialTx
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSerialTx));
            this.tbAscii = new System.Windows.Forms.TextBox();
            this.lblHex = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.comPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Send_btn = new System.Windows.Forms.Button();
            this.Stop_btn = new System.Windows.Forms.Button();
            this.Passband = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseFiles = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.SampFreq = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ShiftText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Shift_P1 = new System.Windows.Forms.Button();
            this.Shift_M1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.OffsetText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Off_M1 = new System.Windows.Forms.Button();
            this.Off_P10 = new System.Windows.Forms.Button();
            this.Off_M10 = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.Plot = new System.Windows.Forms.Button();
            this.Get_Input = new System.Windows.Forms.Button();
            this.Get_Output = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAscii
            // 
            this.tbAscii.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAscii.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAscii.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAscii.ForeColor = System.Drawing.Color.DodgerBlue;
            this.tbAscii.Location = new System.Drawing.Point(12, 29);
            this.tbAscii.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbAscii.Multiline = true;
            this.tbAscii.Name = "tbAscii";
            this.tbAscii.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAscii.Size = new System.Drawing.Size(325, 220);
            this.tbAscii.TabIndex = 0;
            this.tbAscii.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAscii_KeyPress);
            // 
            // lblHex
            // 
            this.lblHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHex.AutoSize = true;
            this.lblHex.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHex.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHex.Location = new System.Drawing.Point(12, 352);
            this.lblHex.Name = "lblHex";
            this.lblHex.Size = new System.Drawing.Size(0, 25);
            this.lblHex.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comPortToolStripMenuItem,
            this.clearTextToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1050, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // comPortToolStripMenuItem
            // 
            this.comPortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.comPortToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.comPortToolStripMenuItem.Name = "comPortToolStripMenuItem";
            this.comPortToolStripMenuItem.Size = new System.Drawing.Size(75, 21);
            this.comPortToolStripMenuItem.Text = "&Com Port";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.connectToolStripMenuItem.Text = "&Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.disconnectToolStripMenuItem.Text = "&Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // clearTextToolStripMenuItem
            // 
            this.clearTextToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.clearTextToolStripMenuItem.Name = "clearTextToolStripMenuItem";
            this.clearTextToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.clearTextToolStripMenuItem.Text = "Clear Te&xt";
            this.clearTextToolStripMenuItem.Click += new System.EventHandler(this.clearTextToolStripMenuItem_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // Send_btn
            // 
            this.Send_btn.Location = new System.Drawing.Point(331, 319);
            this.Send_btn.Name = "Send_btn";
            this.Send_btn.Size = new System.Drawing.Size(79, 43);
            this.Send_btn.TabIndex = 9;
            this.Send_btn.Text = "Send";
            this.Send_btn.UseVisualStyleBackColor = true;
            this.Send_btn.Click += new System.EventHandler(this.Send_btn_Click);
            // 
            // Stop_btn
            // 
            this.Stop_btn.Location = new System.Drawing.Point(331, 379);
            this.Stop_btn.Name = "Stop_btn";
            this.Stop_btn.Size = new System.Drawing.Size(79, 43);
            this.Stop_btn.TabIndex = 10;
            this.Stop_btn.Text = "Stop";
            this.Stop_btn.UseVisualStyleBackColor = true;
            this.Stop_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // Passband
            // 
            this.Passband.FormattingEnabled = true;
            this.Passband.Items.AddRange(new object[] {
            "(none)",
            "Lowpass",
            "Bandpass"});
            this.Passband.Location = new System.Drawing.Point(12, 337);
            this.Passband.Name = "Passband";
            this.Passband.Size = new System.Drawing.Size(109, 25);
            this.Passband.TabIndex = 11;
            this.Passband.Text = "(none)";
            this.Passband.SelectedIndexChanged += new System.EventHandler(this.Passband_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filter Band";
            // 
            // BrowseFiles
            // 
            this.BrowseFiles.Location = new System.Drawing.Point(331, 264);
            this.BrowseFiles.Name = "BrowseFiles";
            this.BrowseFiles.Size = new System.Drawing.Size(107, 42);
            this.BrowseFiles.TabIndex = 13;
            this.BrowseFiles.Text = "Browse Files";
            this.BrowseFiles.UseVisualStyleBackColor = true;
            this.BrowseFiles.Click += new System.EventHandler(this.BrowseFiles_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(12, 264);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(313, 25);
            this.FilePath.TabIndex = 14;
            // 
            // SampFreq
            // 
            this.SampFreq.FormattingEnabled = true;
            this.SampFreq.Items.AddRange(new object[] {
            "(none)",
            "1 KHz",
            "1.5 KHz",
            "2 KHz"});
            this.SampFreq.Location = new System.Drawing.Point(164, 337);
            this.SampFreq.Name = "SampFreq";
            this.SampFreq.Size = new System.Drawing.Size(122, 25);
            this.SampFreq.TabIndex = 15;
            this.SampFreq.Text = "(none)";
            this.SampFreq.SelectedIndexChanged += new System.EventHandler(this.SampFreq_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sampling Frequency";
            // 
            // ShiftText
            // 
            this.ShiftText.Location = new System.Drawing.Point(54, 419);
            this.ShiftText.Name = "ShiftText";
            this.ShiftText.Size = new System.Drawing.Size(46, 25);
            this.ShiftText.TabIndex = 17;
            this.ShiftText.TextChanged += new System.EventHandler(this.ShiftText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Right Shift (0 - 32)";
            // 
            // Shift_P1
            // 
            this.Shift_P1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Shift_P1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Shift_P1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Shift_P1.Location = new System.Drawing.Point(105, 419);
            this.Shift_P1.Name = "Shift_P1";
            this.Shift_P1.Size = new System.Drawing.Size(32, 23);
            this.Shift_P1.TabIndex = 19;
            this.Shift_P1.Text = "+1";
            this.Shift_P1.UseVisualStyleBackColor = false;
            this.Shift_P1.Click += new System.EventHandler(this.Shift_P1_Click);
            // 
            // Shift_M1
            // 
            this.Shift_M1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Shift_M1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Shift_M1.Location = new System.Drawing.Point(17, 421);
            this.Shift_M1.Name = "Shift_M1";
            this.Shift_M1.Size = new System.Drawing.Size(31, 23);
            this.Shift_M1.TabIndex = 20;
            this.Shift_M1.Text = "-1";
            this.Shift_M1.UseVisualStyleBackColor = true;
            this.Shift_M1.Click += new System.EventHandler(this.Shift_M1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 382);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "DC offset (0 - 128)";
            // 
            // OffsetText
            // 
            this.OffsetText.Location = new System.Drawing.Point(204, 402);
            this.OffsetText.Name = "OffsetText";
            this.OffsetText.Size = new System.Drawing.Size(44, 25);
            this.OffsetText.TabIndex = 24;
            this.OffsetText.TextChanged += new System.EventHandler(this.OffsetText_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(254, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "+1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Off_M1
            // 
            this.Off_M1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Off_M1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Off_M1.Location = new System.Drawing.Point(167, 404);
            this.Off_M1.Name = "Off_M1";
            this.Off_M1.Size = new System.Drawing.Size(31, 23);
            this.Off_M1.TabIndex = 26;
            this.Off_M1.Text = "-1";
            this.Off_M1.UseVisualStyleBackColor = true;
            this.Off_M1.Click += new System.EventHandler(this.Off_M1_Click);
            // 
            // Off_P10
            // 
            this.Off_P10.Location = new System.Drawing.Point(227, 431);
            this.Off_P10.Name = "Off_P10";
            this.Off_P10.Size = new System.Drawing.Size(60, 23);
            this.Off_P10.TabIndex = 27;
            this.Off_P10.Text = "+10";
            this.Off_P10.UseVisualStyleBackColor = true;
            this.Off_P10.Click += new System.EventHandler(this.Off_P10_Click);
            // 
            // Off_M10
            // 
            this.Off_M10.Location = new System.Drawing.Point(165, 431);
            this.Off_M10.Name = "Off_M10";
            this.Off_M10.Size = new System.Drawing.Size(56, 23);
            this.Off_M10.TabIndex = 28;
            this.Off_M10.Text = "-10";
            this.Off_M10.UseVisualStyleBackColor = true;
            this.Off_M10.Click += new System.EventHandler(this.Off_M10_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(444, 29);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(594, 441);
            this.zedGraphControl1.TabIndex = 29;
            // 
            // Plot
            // 
            this.Plot.Location = new System.Drawing.Point(359, 193);
            this.Plot.Name = "Plot";
            this.Plot.Size = new System.Drawing.Size(79, 46);
            this.Plot.TabIndex = 30;
            this.Plot.Text = "Plot";
            this.Plot.UseVisualStyleBackColor = true;
            this.Plot.Click += new System.EventHandler(this.Plot_Click);
            // 
            // Get_Input
            // 
            this.Get_Input.Location = new System.Drawing.Point(359, 38);
            this.Get_Input.Name = "Get_Input";
            this.Get_Input.Size = new System.Drawing.Size(79, 43);
            this.Get_Input.TabIndex = 31;
            this.Get_Input.Text = "Get Input Data";
            this.Get_Input.UseVisualStyleBackColor = true;
            this.Get_Input.Click += new System.EventHandler(this.Get_Input_Click);
            // 
            // Get_Output
            // 
            this.Get_Output.Location = new System.Drawing.Point(359, 103);
            this.Get_Output.Name = "Get_Output";
            this.Get_Output.Size = new System.Drawing.Size(79, 65);
            this.Get_Output.TabIndex = 32;
            this.Get_Output.Text = "Get Ouput Data";
            this.Get_Output.UseVisualStyleBackColor = true;
            this.Get_Output.Click += new System.EventHandler(this.Get_Output_Click);
            // 
            // FormSerialTx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1050, 475);
            this.Controls.Add(this.Get_Output);
            this.Controls.Add(this.Get_Input);
            this.Controls.Add(this.Plot);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.Off_M10);
            this.Controls.Add(this.Off_P10);
            this.Controls.Add(this.Off_M1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OffsetText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Shift_M1);
            this.Controls.Add(this.Shift_P1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ShiftText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SampFreq);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.BrowseFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Passband);
            this.Controls.Add(this.Stop_btn);
            this.Controls.Add(this.Send_btn);
            this.Controls.Add(this.lblHex);
            this.Controls.Add(this.tbAscii);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSerialTx";
            this.ShowInTaskbar = false;
            this.Text = "Sampling Selector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSerialTx_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAscii;
        private System.Windows.Forms.Label lblHex;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem comPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTextToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button Send_btn;
        private System.Windows.Forms.Button Stop_btn;
        private System.Windows.Forms.ComboBox Passband;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseFiles;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.ComboBox SampFreq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ShiftText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Shift_P1;
        private System.Windows.Forms.Button Shift_M1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OffsetText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Off_M1;
        private System.Windows.Forms.Button Off_P10;
        private System.Windows.Forms.Button Off_M10;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button Plot;
        private System.Windows.Forms.Button Get_Input;
        private System.Windows.Forms.Button Get_Output;
    }
}

