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
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            _serialPort = new SerialPort(comboBox_PortSelector.Text, 9600);     //lets try to open port
            try { _serialPort.Open(); }                                     //port opening

            catch (UnauthorizedAccessException)
            {
                label1.Text = "Access denied";
                MessageBox.Show("Port access ERROR!", "Error");
            }

            if (_serialPort.IsOpen)                                 //if port open succes
            {
                label1.Text = "OPENING PORT";                       //Show message
                button_connect.Enabled = false;                     //disable connect button
                button_disconnect.Enabled = true;                   //and enable disconnect button
                comboBox_PortSelector.Enabled = false;              //disable port selector
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {

            if (_serialPort.IsOpen)                                 //if Serial Port opened
            {
                label1.Text = "CLOSING PORT";                       //show message in form
                button_connect.Enabled = true;                      //enable connect button
                button_disconnect.Enabled = false;                  //disable disconnect button
                comboBox_PortSelector.Enabled = true;              //disable port selector
                _serialPort.Close();                                //closing port
                label1.Text = "PORT CLOSED";                        //show message
            }
        }
    }
}
