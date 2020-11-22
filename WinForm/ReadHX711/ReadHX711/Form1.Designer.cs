namespace ReadHX711
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.Label label6;
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(12, 128);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "hx711";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(426, 335);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // txtBaud
            // 
            this.txtBaud.BackColor = System.Drawing.SystemColors.Menu;
            this.txtBaud.Location = new System.Drawing.Point(220, 12);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(68, 20);
            this.txtBaud.TabIndex = 13;
            this.txtBaud.Text = "9600";
            // 
            // cmbPorts
            // 
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(93, 12);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(121, 21);
            this.cmbPorts.TabIndex = 12;
            this.cmbPorts.SelectedIndexChanged += new System.EventHandler(this.cmbPorts_SelectedIndexChanged);
            this.cmbPorts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbPorts_MouseClick);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(305, 76);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(116, 20);
            this.textBox1.TabIndex = 14;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(163, 57);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(86, 55);
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // btnclear
            // 
            this.btnclear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.Location = new System.Drawing.Point(44, 57);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(86, 55);
            this.btnclear.TabIndex = 24;
            this.btnclear.Text = "CLEAR";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(302, 57);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(119, 16);
            label6.TabIndex = 26;
            label6.Text = "Current ADC Value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 516);
            this.Controls.Add(label6);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtBaud);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

