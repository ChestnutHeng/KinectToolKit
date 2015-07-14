using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using System.Timers;


namespace KinectToolkit
{

    public partial class MainWindow : Window
    {
        private void UP360()
        {
            if (_kinect == null)
                return;

            if (!_kinect.IsRunning)
                return;

            if (_kinect.ElevationAngle <= _kinect.MaxElevationAngle - 5)
            {
                UP.IsEnabled = false;
                _kinect.ElevationAngle -= buttonangleup;
                UP.IsEnabled = true;
            }
        }

        private void Down360()
        {
            if (_kinect == null)
                return;

            if (!_kinect.IsRunning)
                return;

            if (_kinect.ElevationAngle >= _kinect.MinElevationAngle + 5)
            {
                UP.IsEnabled = false;
                _kinect.ElevationAngle -= buttonangledown;
                UP.IsEnabled = true;
            }
        }
    }

}