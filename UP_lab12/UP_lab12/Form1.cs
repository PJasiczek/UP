using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP_lab12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebCam cam;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            WebCam.LoadAllDevices(listBox1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Wybierz urzadzenie z dostepnej listy");
                return;
            }
            cam = (WebCam)listBox1.SelectedItem;

            cam.DisplayWebCam(pictureBox1, listBox1.SelectedIndex);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cam.StopWebCam(listBox1.SelectedIndex);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cam.SaveImage(listBox1.SelectedIndex);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cam.Record(listBox1.SelectedIndex, false);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            cam.Record(listBox1.SelectedIndex, true);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            cam.ChangeResolution(listBox1.SelectedIndex);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            cam.VideoSource(listBox1.SelectedIndex);
        }

    }
}
