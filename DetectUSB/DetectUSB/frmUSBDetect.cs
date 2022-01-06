using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;

namespace DetectUSB
{
    public partial class frmUSBDetect : Form
    {
        public const int WM_DEVICECHANGE = 0x00000219;
        // 
        List<USBDeviceInfo> usbDevicesBefore = new List<USBDeviceInfo>();
        int numBeforeUsbDevices = 0;

        public frmUSBDetect()
        {
            InitializeComponent();
            Task.Run(() => CheckDevice());
        }
                                                                                                                                                                                                                
        private void CheckDevice()
        {
            var usbDevices = GetUSBDevices();
            string nowTime = DateTime.Now.ToString("HH:mm:ss.ff ");

            if (usbDevices.Count > numBeforeUsbDevices)
            {
                foreach( var usbDevice in usbDevices)
                {
                    bool bExistDevice = false;

                    foreach (var usbDeviceBefore in usbDevicesBefore)
                    {
                        if (usbDevice.DeviceID == usbDeviceBefore.DeviceID)
                        {
                            bExistDevice = true;
                            break;
                        }
                    }
                    if (!bExistDevice)
                    {
                        string sTemp = string.Format("Add Device ID: {0}", usbDevice.DeviceID);
                        AddMessage(nowTime + sTemp + Environment.NewLine);
                    }
                }
            }
            else if (usbDevices.Count < numBeforeUsbDevices)
            {
                foreach (var usbDeviceBefore in usbDevicesBefore)
                {
                    bool bExistDevice = false;
                    foreach (var usbDevice in usbDevices)
                    {
                        if (usbDeviceBefore.DeviceID == usbDevice.DeviceID)
                        {
                            bExistDevice = true;
                            break;
                        }
                    }
                    if (bExistDevice)
                    {
                        string sTemp = string.Format("Del Device ID: {0}", usbDeviceBefore.DeviceID);
                        AddMessage(nowTime + sTemp + Environment.NewLine);
                    }
                }
            }
            AddMessage(Environment.NewLine);        //可読性のため空行追加
            usbDevicesBefore = usbDevices;          //デバイス一覧を更新
            numBeforeUsbDevices = usbDevices.Count;
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();
            // WMIを通じて取得される管理オブジェクト
            ManagementObjectCollection collection;

            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();
            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                    (string)device.GetPropertyValue("DeviceID"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    (string)device.GetPropertyValue("Description")
                ));
            }
            collection.Dispose();
            return devices;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    Task.Run(() => CheckDevice());
                    break;
            }
        }

        public void AddMessage(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string>(AddMessage), msg);
            else
                this.txtUSBInfo.AppendText(msg);
        }

    }


    class USBDeviceInfo
    {
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }

        public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;

        }
    }
}
