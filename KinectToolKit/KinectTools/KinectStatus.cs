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
        int label = 0;
        KinectSensor _kinect;
        private void stopKinect()
        {
            if (_kinect != null)
            {
                if (_kinect.Status == KinectStatus.Connected)
                {
                    //关闭Kinect设备
                    _kinect.Stop();
                    textBlock5.Text = "OFF";
                    MessageBox.Show("Kinect已断开");
                    return;
                }
            }
        }
        private void startKinect()
        {
            if (label == 0)
            {
                if (KinectSensor.KinectSensors.Count > 0)
                {
                    _kinect = KinectSensor.KinectSensors[0];
                    textBlock5.Text = "ON";
                    MessageBox.Show("Kinect目前状态为：" + _kinect.Status);
                    //firstLoaded = true;
                    if (_kinect.Status.ToString() == "Not Powered") GUIloaded();
                    //KinectSensor oldKinect = (KinectSensor)_kinect.OldValue;

                    if (label == 1) stopKinect();

                    //KinectSensor kinect = (KinectSensor)_kinect.NewValue;

                    if (_kinect == null)
                    {
                        return;
                    }

                    _kinect.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0:
                            _kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                            break;
                        case 1:
                            _kinect.ColorStream.Enable(ColorImageFormat.RawBayerResolution640x480Fps30);
                            break;
                        case 2:
                            _kinect.ColorStream.Enable(ColorImageFormat.RawBayerResolution1280x960Fps12);
                            break;
                        case 3:
                            _kinect.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                            break;
                        default:
                            break;
                    }

                    var parameters = new TransformSmoothParameters
                    {
                        Smoothing = 0.5f,
                        Correction = 0.5f,
                        Prediction = 0.5f,
                        JitterRadius = 0.05f,
                        MaxDeviationRadius = 0.04f
                    };
                    _kinect.SkeletonStream.Enable(parameters);

                    _kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);

                    try
                    {
                        //显示彩色图像摄像头
                        kinectColorViewer1.Kinect = _kinect;
                        kinectSkeletonViewer1.Kinect = _kinect;
                        //启动
                        _kinect.Start();
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("ColorViewer Did't Start");
                        kinectSensorChooser1.AppConflictOccurred();
                    }

                }
                else
                {
                    MessageBox.Show("没有发现任何Kinect设备");
                }
            }
        }
        //listener
        //void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e){}
        
    }

}