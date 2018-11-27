using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.DirectInput;
using System.Runtime.InteropServices;

namespace UP_lab10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetSticks();
            sticks = GetSticks();
            timer1.Enabled = true;
        }
        DirectInput input = new DirectInput();
        SlimDX.DirectInput.Joystick stick;
        Joystick[] sticks;
        bool mouseClicked = false;
        int xVal = 0;
        int yVal = 0;
        int zVal = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(uint flag, uint _x, uint _y, uint bnt, uint exInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;  
        private const uint MOUSEEVENTF_LEFTUP = 0x04;  
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08; 
        private const uint MOUSEEVENTF_RIGHTUP = 0x10; 

        public Joystick[] GetSticks()
        {
            List<SlimDX.DirectInput.Joystick> sticks = new List<SlimDX.DirectInput.Joystick>();
            foreach(DeviceInstance device in input.GetDevices(DeviceClass.GameController,DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    stick = new SlimDX.DirectInput.Joystick(input, device.InstanceGuid);
                    label10.Text = device.ProductName;
                    stick.Acquire();
                    foreach(DeviceObjectInstance deviceObject in stick.GetObjects()){

                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        {
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
         
                        }
                    }
                    sticks.Add(stick);
                }
                catch (DirectInputException)
                {

                }
            }
            return sticks.ToArray();
        }

        void stickHandle(Joystick stick, int id)
        {
            JoystickState state = new JoystickState();
            state = stick.GetCurrentState();
            xVal = state.X;
            yVal = state.Y;
            zVal = state.Z;
            MouseMove(xVal, yVal);
            bool[] buttons = state.GetButtons();
            label3.Text = xVal.ToString();
            label4.Text = yVal.ToString();
            label12.Text = zVal.ToString();
            if (id == 0)
            {
                if(buttons[0])
                {
                   if(mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label5.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[1])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label18.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[2])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label17.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[3])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label15.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[4])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label16.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[5])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label6.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[6])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label8.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[7])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label13.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[8])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label14.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[9])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label9.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                if (buttons[10])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                        label7.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                    }
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouseClicked = false;
                    this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
                }
     
            }
        }

        public void MouseMove(int posx, int posy)
        {
            Cursor.Position = new Point(Cursor.Position.X + posx/3, Cursor.Position.Y + posy/3);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < sticks.Length; i++)
            {
                stickHandle(sticks[i], i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Joystick[] joystick = GetSticks();
        }

    }
}
