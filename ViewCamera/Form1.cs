using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewCamera
{
    public partial class Form1 : Form
    {

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private FilterInfo videoDevice1;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities videoCapabilitie1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                int width=0,height=0;
                videoDevice1 = videoDevices[0];
                videoDevice = new VideoCaptureDevice(videoDevice1.MonikerString);
                videoCapabilities = videoDevice.VideoCapabilities;
                foreach (VideoCapabilities capabilty in videoCapabilities)
                {
                    if (capabilty.FrameSize.Width > width)
                    {
                        videoCapabilitie1 = capabilty;
                        width = capabilty.FrameSize.Width;
                        height = capabilty.FrameSize.Height;

                        Rectangle ScreenArea = Screen.GetBounds(this);
                        int width1 = ScreenArea.Width; //屏幕宽度
                        int height1 = ScreenArea.Height; //屏幕高度

                        width = Math.Min(width1, width);
                        height = Math.Min(width1, height);

                        this.WindowState = FormWindowState.Maximized;
                        videoSourcePlayer1.Width = width;
                        videoSourcePlayer1.Height = height;
                    }
                }
                if (videoDevice != null)
                {
                    if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                    {
                        videoDevice.VideoResolution = videoCapabilitie1;
                        videoSourcePlayer1.VideoSource = videoDevice;
                        videoSourcePlayer1.Start();
                    }
                }
            }
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    closeProc("LiveWallpaperEngine.Samples.NetCore.Test.exe");
        //}

        //private bool closeProc(string ProcName)
        //{
        //    bool result = false;
        //    System.Collections.ArrayList procList = new System.Collections.ArrayList();
        //    string tempName = "";

        //    foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
        //    {
        //        tempName = thisProc.ProcessName;
        //        procList.Add(tempName);
        //        if (tempName == ProcName)
        //        {
        //            if (!thisProc.CloseMainWindow())
        //                thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程     
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
    }
}
