using System;
using System.Windows;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace Vestuario
{

    public partial class MainWindow : Window
    {
        KinectSensorChooser sKinect;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            /*comprueba si hay sensores*/
            sKinect = new KinectSensorChooser();
            /*comprueba el estado de la kinect*/
            sKinect.KinectChanged += sKinect_KinectChanged;
            /*muestra el estado de la conexion*/
            sensorChooserUI.KinectSensorChooser = sKinect;
            sKinect.Start();
        }

        void sKinect_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            bool error = false;
            /*identifica la conexion de la kinect*/
            if (e.OldSensor == null)
            {
                try
                {
                    e.OldSensor.DepthStream.Disable();
                    e.OldSensor.SkeletonStream.Disable();
                }
                catch (Exception)
                {
                    error = true;
                }
            }

            if (e.NewSensor == null)
                return;

            try
            {/*si esta todo correcto
                habilita los sensores de profundidad y reconocimiento del esqeueleto*/
                e.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                e.NewSensor.SkeletonStream.Enable();
                e.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                e.NewSensor.DepthStream.Range = DepthRange.Near;
                e.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
            }
            catch (InvalidOperationException)
            {
                e.NewSensor.DepthStream.Range = DepthRange.Default;
                e.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;

                error = true;
            }
            SecureZone.KinectSensor = e.NewSensor;

        }

        private void KinectTileButton_Click_0(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCamisetas(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Acceso al vestuario de Camisetas");
        }
        private void btnPantalones(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Acceso al vestuario de Pantalones");
        }
        private void btnVestidos(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Acceso al vestuario de Vestidos");
        }
    }
}

