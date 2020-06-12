using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
namespace Kinect2
{
    public partial class Form1 : Form
    {
        private KinectSensor kSensor;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        /*enceder la kinect al pulsar el boton*/
        private void btnStream_Click(object sender, EventArgs e)
        {
            if(btnStream.Text == "Play")
            {
              
                if(KinectSensor.KinectSensors.Count>0)
                {
                    this.btnStream.Text = "Stop";
                    kSensor = KinectSensor.KinectSensors[0];
                    KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
                }
                kSensor.Start();
                /* muestra el estado del kinect por uno valores predefinidos*/
                this.lblId.Text = kSensor.DeviceConnectionId;
                kSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                kSensor.ColorFrameReady += KSensor_ColorFrameReady;
            }
            else
            {

                if(kSensor !=null && kSensor.IsRunning)
                {
                    kSensor.Stop();
                    this.btnStream.Text = "Play";
                    this.pictureBox.Image = null;
                }
            }
        }
/*lo elimina de la memoria*/
        private void KSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            
            using (var frame = e.OpenColorImageFrame())
            {
                /*crea imagen*/
            }
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.lblStatus.Text = kSensor.Status.ToString();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
        private Bitmap CreateBitmatFromSensor(ColorImageFrame frame)
        {
            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);
            var stride = frame.Width * frame.BytesPerPixel;

            var bmpFrame = new Bitmap(frame.Width, frame.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bmpData = bmpFrame.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmpFrame.PixelFormat);

            System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bmpData.Scan0, frame.PixelDataLength);

            bmpFrame.UnlockBits(bmpData);
        }
    }
}
