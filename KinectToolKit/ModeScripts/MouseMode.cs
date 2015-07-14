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
        //mouse area
        private const double ArmXStretchedThreshold = 0.3; //手臂X轴方向伸展的阀值，单位米
        private const double ArmZStretchedThreshold = 0.2; //手臂Z轴方向伸展的阀值，单位米
        private bool isMouseLeftButtonDown = false;

        private void TrackHand2SimulateMouseMove(Joint hand)
        {
            if (hand.TrackingState != JointTrackingState.Tracked)
                return;
            if (hand.TrackingState != JointTrackingState.Tracked)
                MessageBox.Show("not tracked your hand");
            //获得屏幕的宽度和高度
            int screenWidth = (int)SystemParameters.PrimaryScreenWidth;
            int screenHeight = (int)SystemParameters.PrimaryScreenHeight;


            //将部位“手”的骨骼坐标映射为屏幕坐标；手只需要在有限范围内移动即可覆盖整个屏幕区域
            float posX = hand.ScaleTo(screenWidth, screenHeight, 0.2f, 0.2f).Position.X;
            float posY = hand.ScaleTo(screenWidth, screenHeight, 0.2f, 0.2f).Position.Y;

            //test
            //image1 
            Joint scaledCursorJoint = new Joint
            {
                TrackingState = JointTrackingState.Tracked,
                Position = new SkeletonPoint
                {
                    X = posX,
                    Y = posY,
                    Z = hand.Position.Z
                }
            };

            int x = Convert.ToInt32(scaledCursorJoint.Position.X);
            int y = Convert.ToInt32(scaledCursorJoint.Position.Y);

            //MouseToolkit.SetCursorPos(Convert.ToInt32(scaledCursorJoint.Position.X), 
            //    Convert.ToInt32(scaledCursorJoint.Position.Y));

            int mouseX = Convert.ToInt32(x * 65536 / screenWidth);
            int mouseY = Convert.ToInt32(y * 65536 / screenHeight);

            MouseToolkit.mouse_event(MouseToolkit.MouseEventFlag.Absolute | MouseToolkit.MouseEventFlag.Move,
                mouseX, mouseY, 0, 0);
            // image1.Margin = new Thickness(x, y, 0, 0);
            textBlock12.Text = "(" + x.ToString() + "," + y.ToString() + ")";

        }

    }

}