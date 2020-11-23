using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;

namespace ReadHX711
{
    public partial class Form1 : Form
    {
        ConcurrentQueue<Int32> dataQueue = new ConcurrentQueue<Int32>(); // for the Queue side of the solution
        double time = 0;
        bool connected = false;
        int numberInReadBuffer = 0;
        int inbedvalue = 8050000;
        DateTime TimeInBed;
        bool inBed = false;
        TimeSpan Time;
        public Form1()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            connected = true; //HW fix indicator seral is on
            int newByte = 0;
            int bytesToRead;
            bytesToRead = serialPort1.BytesToRead;
            numberInReadBuffer = serialPort1.BytesToRead;

            while (bytesToRead != 0)
            {
                newByte = serialPort1.ReadByte();
                dataQueue.Enqueue(newByte);
                bytesToRead = serialPort1.BytesToRead;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (!serialPort1.IsOpen)
            {
                if (cmbPorts.Items.Count > 0)
                {
                    try
                    {
                        serialPort1.BaudRate = Convert.ToInt32(txtBaud.Text);
                        serialPort1.PortName = cmbPorts.SelectedItem.ToString();
                        serialPort1.Open();
                        cmbPorts.Enabled = false;
                        btnConnect.Text = "Disconnect";

                        //chartDCPosition.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                        
                        chart1.Series["hx711"].Points.Clear();
                        timer1.Enabled = true;
                        time = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    btnConnect.Text = "Connect";
                    cmbPorts.Enabled = true;
                    serialPort1.Dispose();                    
                    timer1.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ComPortUpdate()
        {
            cmbPorts.Items.Clear();
            string[] comPortArray = System.IO.Ports.SerialPort.GetPortNames().ToArray();
            Array.Reverse(comPortArray);
            cmbPorts.Items.AddRange(comPortArray);
            if (cmbPorts.Items.Count != 0)
                cmbPorts.SelectedIndex = 0;
            else
                cmbPorts.Text = "No Ports Found!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComPortUpdate();
        }

        private void cmbPorts_MouseClick(object sender, MouseEventArgs e)
        {
            ComPortUpdate();
        }

        private void cmbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = cmbPorts.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int newpoint;
            int data;
            while (dataQueue.Count > 4)
            {
                dataQueue.TryDequeue(out newpoint);
                dataQueue.TryDequeue(out newpoint);
                data = newpoint << 16;
                dataQueue.TryDequeue(out newpoint);
                data = data + (newpoint << 8);
                dataQueue.TryDequeue(out newpoint);
                data = data + (newpoint);

                chart1.Series["hx711"].Points.AddY(data);
                textBox1.Text = data.ToString("0");

                if (data > inbedvalue && inBed == false)
                {
                    TimeInBed = DateTime.Now;
                    inBed = true;
                }else if (inBed == true && data<inbedvalue){
                    inBed = false;
                    

                }
                else if (inBed == true && data > inbedvalue)
                {
                    Time = DateTime.Now - TimeInBed;
                    textBox2.Text = Time.ToString("hh\\:mm\\:ss");
                }
            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            saveFileDialog1.DefaultExt = ".CSV";
            saveFileDialog1.Filter = "CSV (.CSV)|*.csv";
            saveFileDialog1.ShowDialog();
            string filename = saveFileDialog1.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                try
                {
                    System.IO.StreamWriter outputfile;
                    outputfile = new System.IO.StreamWriter(filename);
                    outputfile.WriteLine("Time, Xo [counts], Vo [counts/s]");
                    int length = chart1.Series["hx711"].Points.Count;
                    string newline;

                    for (int i = 0; i < length; i++)
                    {
                        newline = "";                        
                        newline += chart1.Series["hx711"].Points[i].YValues[0].ToString();
                        outputfile.WriteLine(newline);
                    }
                    outputfile.Close();

                }

                catch (Exception)
                {
                    MessageBox.Show("ERROR");
                }
            }
            else
                MessageBox.Show("ERROR");

            timer1.Enabled = true;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            chart1.Series["hx711"].Points.Clear();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
