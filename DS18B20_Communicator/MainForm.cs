using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; // work with com ports
using System.Collections;
using System.Threading;

namespace DS18B20_Communicator
{
    public partial class MainForm : Form
    {
        SerialPort _serialPort;
        public MainForm()
        {
            InitializeComponent();
            button_disconnect.Enabled = false;                              //disable disconnect button

            ArrayList ports = new ArrayList();                              //collecting portnames

            foreach (string s in SerialPort.GetPortNames())                 //get all aviliable ports
            {
                comboBox_PortSelector.Items.Add(s);                         //put port name into combox
                comboBox_PortSelector.SelectedIndex = 0;                    //set default port to first port founded
                ports.Add(s);                                               //put port name into collections
            }
            
            StatusLabel1.Text = "Ready to use, pleace select port and press connect";

        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            _serialPort = new SerialPort(comboBox_PortSelector.Text, 9600);     //lets try to open port
            try { _serialPort.Open(); }                                     //port opening

            catch (UnauthorizedAccessException)
            {
                StatusLabel1.Text = "Port access ERROR!";
                MessageBox.Show("Port access ERROR!", "Error");
            }

            StatusLabel1.Text = "OPENING PORT";                       //Show message

            if (_serialPort.IsOpen)                                 //if port open succes
            {
                
                button_connect.Enabled = false;                     //disable connect button
                button_disconnect.Enabled = true;                   //and enable disconnect button
                comboBox_PortSelector.Enabled = false;              //disable port selector
                StatusLabel1.Text = "CONNECTED";                       //Show message
                StatusLabel1.ForeColor = Color.Blue;                       //Show message


            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            StatusLabel1.Text = "CLOSING PORT";                       //show message in form
            if (_serialPort.IsOpen)                                 //if Serial Port opened
            {
                
                button_connect.Enabled = true;                      //enable connect button
                button_disconnect.Enabled = false;                  //disable disconnect button
                comboBox_PortSelector.Enabled = true;              //disable port selector
                _serialPort.Close();                                //closing port
                StatusLabel1.Text = "DISCONNECTED";                        //show message
                StatusLabel1.ForeColor = Color.Red;                       //Show message
            }
        }
    }
}
