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
        int inbedvalue = 9000000;
        DateTime EnteredBed;
        bool inBed = false;
        TimeSpan Time;
        DateTime startConnection = DateTime.Now;
        DateTime cyclestart;
        DateTime cycleEnd;
        DateTime leftTheBed;
        DateTime alarmrang;
        int state = 0;
        int data = 0;
        bool inACycle = false;

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
                        startConnection = DateTime.Now;
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
            
            textBox3.Text = numberInReadBuffer.ToString("0");
            textBox4.Text = (DateTime.Now-startConnection).ToString("hh\\:mm\\:ss");
            while (dataQueue.Count > 0)
            {
                switch (state)
                {
                    case 0:
                        dataQueue.TryDequeue(out newpoint);
                        if (newpoint == 255)
                            state = 1;
                        break;
                    case 1:
                        dataQueue.TryDequeue(out newpoint);
                        data = newpoint << 16;
                        state = 2;
                        break;
                    case 2:
                        dataQueue.TryDequeue(out newpoint);
                        data = data + (newpoint << 8);
                        state = 3;
                        break;
                    case 3:
                        dataQueue.TryDequeue(out newpoint);
                        data = data + (newpoint);

                        chart1.Series["hx711"].Points.AddY(data);
                        textBox1.Text = data.ToString("0");

                        /*
                        if (data < 8300000 && connection == 0)
                        {
                            label3.Text = Time.ToString("hh\\:mm\\:ss");
                            connection = 1;

                        }*/

                        if (data > inbedvalue && inBed == false) // entered the bed
                        {
                            EnteredBed = DateTime.Now;
                            inBed = true;

                        }
                        else if (inBed == true && data < inbedvalue) // left the bed
                        {
                            inBed = false;
                            leftTheBed = DateTime.Now;
                            if (inACycle) {
                                txtWakingTime.Text = (leftTheBed - alarmrang).ToString("hh\\:mm\\:ss");
                            }


                        }
                        else if (inBed == true && data > inbedvalue) // while in bed
                        {

                            textBox2.Text = (DateTime.Now - EnteredBed).ToString("hh\\:mm\\:ss");
                        }
                        state = 4;
                        break;
                    case 4:

                        dataQueue.TryDequeue(out newpoint);

                        if (newpoint == 1)
                        { // alarm sounded
                            inACycle = true;
                            alarmrang = DateTime.Now;
                            if(inBed == false)
                                txtTimeSlep.Text = (leftTheBed - EnteredBed).ToString("hh\\:mm\\:ss");
                            else
                                txtTimeSlep.Text = (alarmrang - EnteredBed).ToString("hh\\:mm\\:ss");
                            txtBedTime.Text = EnteredBed.ToString("hh\\:mm\\:ss");
                        }
                        else if (newpoint == 2)
                        {// left bed by msp

                        }
                        else if (newpoint == 3) { // full cycle end
                            inACycle = false;
                                                       
                            listBox1.Items.Add(txtBedTime.Text);
                            listBox2.Items.Add(txtTimeSlep.Text);
                            listBox3.Items.Add(txtWakingTime.Text);
                            txtBedTime.Text = "";
                            txtTimeSlep.Text = "";
                            txtWakingTime.Text = "";
                        }

                        state = 0;
                        break;
                    default:
                        
                        break;
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
