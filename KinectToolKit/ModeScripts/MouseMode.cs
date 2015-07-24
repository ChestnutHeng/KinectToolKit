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

        private void PlayAirMouse(Skeleton s)
        {
            var joints = s.Joints;

            Joint rightHand = joints[JointType.HandRight];
            Joint leftHand = joints[JointType.HandLeft];
            Joint head = joints[JointType.Head];
            SkeletonPoint rightshoulder = s.Joints[JointType.ShoulderRight].Position;
            SkeletonPoint rightHand1 = s.Joints[JointType.HandRight].Position;
            //根据与Kinect的距离，来判定是左手还是右手来操作鼠标，兼容左右手习惯
            /*var hand = (rightHand.Position.Z < leftHand.Position.Z)
                            ? rightHand
                            : leftHand;*/
            var hand = rightHand;

            installtextblock();

            //如果手没有伸出，则不作跟踪。以头部的Z坐标为参照点
            if (head.Position.Z - hand.Position.Z <= ArmZStretchedThreshold)
            {
                return;
            }
            //模拟鼠标移动
            TrackHand2SimulateMouseMove(hand);

            // Righthand has been cancelled,this code is used to get track
            // bool isLeftHandStretched = ((head.Position.X - leftHand.Position.X) > ArmXStretchedThreshold) &&
            //   ((head.Position.Z - leftHand.Position.Z) < ArmZStretchedThreshold);
            bool isLeftHandStretched = (head.Position.Z - leftHand.Position.Z) > ArmXStretchedThreshold;
            /* bool isRightHandStreched = ((rightHand.Position.X - head.Position.X) > ArmXStretchedThreshold) &&
                 ((head.Position.Z - rightHand.Position.Z) < ArmZStretchedThreshold);*/
            //bool isRightHandStreched = (rightHand1.X - rightshoulder.X) > ArmStretchedThreadhold;

            //left mouse down if right hand rised
            //if (isLeftHandStretched || isRightHandStreched)
            if (isLeftHandStretched)
            {
                MouseToolkit.mouse_event(MouseToolkit.MouseEventFlag.LeftDown, 0, 0, 0, 0);
                isMouseLeftButtonDown = true;
            }
            else if (isMouseLeftButtonDown)
            {
                isMouseLeftButtonDown = false;
                MouseToolkit.mouse_event(MouseToolkit.MouseEventFlag.LeftUp, 0, 0, 0, 0);
            }
        }


        private const double ArmXStretchedThreshold = 0.3; //手臂X轴方向伸展的阀值，单位米
        private const double ArmZStretchedThreshold = 0.0; //手臂Z轴方向伸展的阀值，单位米
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
            float posX = hand.ScaleTo(screenWidth, screenHeight, 0.15f, 0.15f).Position.X;
            float posY = hand.ScaleTo(screenWidth, screenHeight, 0.15f, 0.15f).Position.Y;
            

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