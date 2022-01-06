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

            InitializeListView();

            Task.Run(() => CheckDevice());
        }

        private void InitializeListView()
        {
            lvwUsbDevice.View = View.Details;

            ColumnHeader chTime = new ColumnHeader();
            chTime.Text = "時刻";
            chTime.Width = 100;

            ColumnHeader chOperate = new ColumnHeader();
            chOperate.Text = "操作";
            chOperate.Width = 100;

            ColumnHeader chDevId = new ColumnHeader();
            chDevId.Text = "Device ID";
            chDevId.Width = 100;

            ColumnHeader chTest = new ColumnHeader();
            chTest.Text = "Test";
            chTest.Width = 100;

            ColumnHeader chTest2 = new ColumnHeader();
            chTest2.Text = "Test";
            chTest2.Width = 100;

            ColumnHeader[] colHeader = {
                chTime, chOperate, chDevId, chTest, chTest2
            };

            lvwUsbDevice.Columns.AddRange(colHeader);
        }
                                                                                                                                                                                                                
        private void CheckDevice()
        {
            var usbDevices = GetUSBDevices();
            string nowTime = DateTime.Now.ToString("HH:mm:ss.ff ");
            Console.WriteLine("{2}:前回件数:{0},今回件数{1}", usbDevicesBefore.Count, usbDevices.Count,nowTime);

            foreach( var usbDevice in usbDevices)
            {
                bool bExistDevice = false;

                // 前回検出したレコードと比較する
                foreach (var usbDeviceBefore in usbDevicesBefore)
                {
                    if (usbDevice.DeviceID == usbDeviceBefore.DeviceID)
                    {
                        // 同一のデバイスIDが見つかれば、検索を中止する
                        bExistDevice = true;
                        break;
                    }
                }

                if (!bExistDevice)
                {
                    // 前回のレコードに今回のデバイスIDが見つからなかったら、追加されたデバイスとみなす
                    string sTemp = string.Format("Add Device ID: {0}", usbDevice.DeviceID);
                    AddMessage(nowTime + sTemp + Environment.NewLine);
                    AddListView(nowTime, "Add", usbDevice);
                }
            }

            foreach (var usbDeviceBefore in usbDevicesBefore)
            {
                bool bExistDevice = false;

                // 今回検出したレコードと比較する
                foreach (var usbDevice in usbDevices)
                {
                    if (usbDeviceBefore.DeviceID == usbDevice.DeviceID)
                    {
                        // 同一のデバイスIDが見つかれば、検索を中止する
                        bExistDevice = true;
                        break;
                    }
                }

                if (bExistDevice)
                {
                    // 今回のレコードに前回のレコードが見つからなければ、削除されたデバイスとみなす
                    string sTemp = string.Format("Del Device ID: {0}", usbDeviceBefore.DeviceID);
                    AddMessage(nowTime + sTemp + Environment.NewLine);
                    AddListView(nowTime, "Del", usbDeviceBefore);
                }
            }

            AddMessage(Environment.NewLine);        //可読性のため空行追加
            usbDevicesBefore = usbDevices;          //デバイス一覧を更新
            numBeforeUsbDevices = usbDevices.Count;
            Console.WriteLine("終了時刻{0}", DateTime.Now.ToString("HH:mm:ss.ff "));
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
                    (string)device.GetPropertyValue("Caption"),
                    (string)device.GetPropertyValue("DeviceID"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    (string)device.GetPropertyValue("Description"),
                    (string)device.GetPropertyValue("Name")
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

        public void AddListView(string tim, string name, USBDeviceInfo info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, string, USBDeviceInfo>(AddListView), tim, name, info);
            }
            else
            {
                ListViewItem lvi;
                lvi = lvwUsbDevice.Items.Add(tim);
                lvi.SubItems.Add(name);
                lvi.SubItems.Add(info.DeviceID);
                lvi.SubItems.Add(info.Caption);
                lvi.SubItems.Add(info.TestProp);
            }
        }

    }


    public class USBDeviceInfo
    {
        public string Caption { get; private set; }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
        public string TestProp { get; private set; }

        public USBDeviceInfo(string caption, string deviceID, string pnpDeviceID, string description, string test)
        {
            this.Caption = caption;
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
            this.TestProp = test;

        }
    }
}
