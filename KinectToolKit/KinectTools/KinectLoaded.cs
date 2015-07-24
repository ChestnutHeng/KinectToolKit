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
        //FrameReady
        bool WindowsClosing = false;
        Skeleton[] allSkeletons = new Skeleton[6];
        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (WindowsClosing)
            {
                return;
            }

            //Get a skeleton
            Skeleton s = getClosetSkeleton(e);

            if (s == null)
            {
                return;
            }

            if (s.TrackingState != SkeletonTrackingState.Tracked)
            {
                return;
            }

            //track massage
            if (s.TrackingState == SkeletonTrackingState.Tracked)
            {
                textBlock6.Text = "抓到你的骨头啦！";
                //keyboard reflection, mario or PPT
                //use videobox to choose
                if (status == mode.single)
                    presentPowerPoint(s);
                else if (status == mode.much)
                    playSuperMario(s);
                else if (checkBox1.IsChecked == true && status == mode.mouse)
                    PlayAirMouse(s);
                else if (status == mode.car)
                    PlayCar(s);
                else MessageBox.Show("Error Status");
            }
            else { textBlock6.Text = "你在哪里？"; }
        }

        Skeleton getClosetSkeleton(SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null;
                }

                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //Linq语法，查找离Kinect最近的、被跟踪的骨骼(from internet copy)
                Skeleton closestSkeleton = (from s in allSkeletons
                                            where s.TrackingState == SkeletonTrackingState.Tracked &&
                                                  s.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked
                                            select s).OrderBy(s => s.Joints[JointType.Head].Position.Z)
                                    .FirstOrDefault();

                return closestSkeleton;
            }
        }
        private void kinectSensorChooser1_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void kinectColorViewer1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

}