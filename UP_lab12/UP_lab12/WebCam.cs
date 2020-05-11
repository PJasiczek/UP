using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP_lab12
{
    class WebCam
    {
        private const int WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WM_CAP_SAVEDIB = WM_CAP + 25;
        private const int WM_CAP_GRAB_FRAME_NOSTOP = WM_CAP + 61;
        private const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP + 41;
        private const int WM_CAP_STOP = WM_CAP + 68;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_FILE_SET_CAPTURE_FILE = WM_CAP + 20;
        private const int WM_CAP_SEQUENCE = WM_CAP + 62;
        private const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP + 42;

        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(int wDriverIndex, [MarshalAs(UnmanagedType.VBByRefStr)]ref String ldevicename, int namelength, [MarshalAs(UnmanagedType.VBByRefStr)] ref String deviceversion, int versionlength);
        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string windowname,
            int style, int x, int y, int width, int height, int parent, int id);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int window, int x, [MarshalAs(UnmanagedType.AsAny)] object y);

        public int deviceHandle;

        string devicename, deviceversion;

        public WebCam(string devicename, string deviceversion)
        {
            this.devicename = devicename;
            this.deviceversion = deviceversion;

        }

        public static void LoadAllDevices(ListBox lstDevices)
        {
            String devicename = "".PadRight(100);
            String deviceversion = "".PadRight(100);
            lstDevices.Items.Clear();
            for (int i = 0; i <= 10; i++)
            {
                bool isDeviceReady = capGetDriverDescriptionA(i, ref devicename, 100, ref deviceversion, 100);
                if (!isDeviceReady)
                    continue;
                {
                    WebCam d = new WebCam(devicename, deviceversion);
                    lstDevices.Items.Add(d);
                }
            }
        }

        public System.Drawing.Image GetImage()
        {
            IDataObject data;
            System.Drawing.Image snapshot;

            SendMessage(deviceHandle, WM_CAP_EDIT_COPY, 0, 0);

            data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                snapshot = (System.Drawing.Image)data.GetData(typeof(System.Drawing.Bitmap));
                return snapshot;
            }
            return null;
        }

        public void SaveImage(int deviceno)
        {
            string deviceIndex = "" + deviceno;
            SendMessage(deviceHandle, WM_CAP_SAVEDIB, deviceno, "image.jpg");
        }

        public void Record(int deviceno, bool record)
        {
            if (record)
            {

                SendMessage(deviceHandle, WM_CAP_STOP, deviceno, "video.avi");
                record = false;
            }
            else
            {
                SendMessage(deviceHandle, WM_CAP_FILE_SET_CAPTURE_FILE, 0, "video.avi");
                SendMessage(deviceHandle, WM_CAP_SEQUENCE, deviceno, 0);
                record = true;
            }

        }

        public void ChangeResolution(int deviceno)
        {
            SendMessage(deviceHandle, WM_CAP_DLG_VIDEOFORMAT, deviceno, 0);
        }

        public void VideoSource(int deviceno)
        {
            SendMessage(deviceHandle, WM_CAP_DLG_VIDEOSOURCE, deviceno, 0);
        }

        public override string ToString()
        {
            return this.devicename + " " + this.deviceversion;
        }

        public void StartWebCam(int height, int width, int handle, int deviceno)
        {
            string deviceIndex = "" + deviceno;
            deviceHandle = capCreateCaptureWindowA(ref deviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, width, height, handle, 0);

            if (SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, deviceno, 0) > 0)
            {
                SendMessage(deviceHandle, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, 0x42, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, -1, 0);
            }
        }

        public void DisplayWebCam(PictureBox pic, int deviceno)
        {
            StartWebCam(pic.Height, pic.Width, (int)pic.Handle, deviceno);
        }


        public void StopWebCam(int deviceno)
        {
            SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, deviceno, 0);
        }

    }
}

