using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.IO;


namespace UP_lab14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public BluetoothRadio[] adapters { get; set; }
        public BluetoothClient client = new BluetoothClient();
        public BluetoothDeviceInfo[] devices { get; set; }
        public BluetoothDeviceInfo devChosen { get; set; }

        private void Adapter_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            adapters = BluetoothRadio.AllRadios;
            foreach (var adapter in adapters)
            {
                listBox1.Items.Add(adapter.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            devices = client.DiscoverDevices();
            foreach (BluetoothDeviceInfo device in devices)
            {
                listBox2.Items.Add(device);
            }
            listBox2.DisplayMember = "device.DeviceName";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MAC;
            foreach (BluetoothDeviceInfo dev in listBox2.SelectedItems)
            {
                MAC = dev.DeviceAddress.ToString();
                label2.Text = MAC;
                devChosen = dev;
            }
        }

        Thread th = new Thread(listen);

        static private void listen()
        {
            while (true)
            {
                var listener = new ObexListener(ObexTransport.Bluetooth);
                listener.Start();
                ObexListenerContext con = listener.GetContext();
                ObexListenerRequest req = con.Request;
                String[] pathSplits = req.RawUrl.Split('/');
                String filename = pathSplits[pathSplits.Length - 1];
                req.WriteFile(filename);
                listener.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                }
                catch (IOException)
                {
                }

                var fileToSend = new Uri("obex://" + devChosen.DeviceAddress + "/" + file);
                var obexReq = new ObexWebRequest(fileToSend);
                obexReq.ReadFile(file);
                var obexResp = obexReq.GetResponse();
                obexResp.Close();
            }
        }
    }
}
