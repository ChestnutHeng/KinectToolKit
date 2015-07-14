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
        enum mode { single, much, mouse, car };
        mode status = mode.single;

        //Keyboard

        private const double ArmRaisedThreshhold = 0.2; //arms chuizhi /m
        private const double ArmStretchedThreadhold = 0.5; //arms shuiping /m
        private const double JumpDiffThreadhold = 0.05; //jump /m
        private double headPreviousPosition = 2.0; // firstvalue /m

        void playSuperMario(Skeleton s)
        {
            SkeletonPoint head = s.Joints[JointType.Head].Position;
            SkeletonPoint leftshoulder = s.Joints[JointType.ShoulderLeft].Position;
            SkeletonPoint rightshoulder = s.Joints[JointType.ShoulderRight].Position;

            SkeletonPoint leftHand = s.Joints[JointType.HandLeft].Position;
            SkeletonPoint rightHand = s.Joints[JointType.HandRight].Position;

            bool isRightHandRaised = (rightHand.Y - rightshoulder.Y) > ArmRaisedThreshhold;
            bool isLeftHandRaised = (leftHand.Y - leftshoulder.Y) > ArmRaisedThreshhold;

            bool isRightHandStretched = (rightHand.X - rightshoulder.X) > ArmStretchedThreadhold;
            bool isLeftHandStretched = (leftshoulder.X - leftHand.X) > ArmStretchedThreadhold;


            //判断头部的位置是否变高
            if ((head.Y - headPreviousPosition) > JumpDiffThreadhold)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Up);
                
                textBlock14.Text = "Up";
            }
            headPreviousPosition = head.Y;

            //玩超级玛丽
            //右手水平伸展开
            if (isRightHandStretched)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Right);
                textBlock14.Text = "Right";
            }

            //左手水平伸展开
            if (isLeftHandStretched)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Left);
                textBlock14.Text = "Left";

            }
            //双手同时举起
            if (isLeftHandRaised && isRightHandRaised)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.B);
                textBlock14.Text = "Down";
            }
        }


        private bool isForwardGestureActive = false;
        private bool isBackGestureActive = false;
        private bool isBlackScreenActive = false;
        private const double ArmStretchedThreadhold4PPT = 0.4; //arm weigh in ppt


        private void presentPowerPoint(Skeleton s)
        {
            SkeletonPoint head = s.Joints[JointType.Head].Position;
            SkeletonPoint leftshoulder = s.Joints[JointType.ShoulderLeft].Position;
            SkeletonPoint rightshoulder = s.Joints[JointType.ShoulderRight].Position;

            SkeletonPoint leftHand = s.Joints[JointType.HandLeft].Position;
            SkeletonPoint rightHand = s.Joints[JointType.HandRight].Position;

            bool isRightHandRaised = (rightHand.Y - rightshoulder.Y) > ArmRaisedThreshhold;
            bool isLeftHandRaised = (leftHand.Y - leftshoulder.Y) > ArmRaisedThreshhold;

            bool isRightHandStretched = (rightHand.X - rightshoulder.X) > ArmStretchedThreadhold4PPT;
            bool isLeftHandStretched = (leftshoulder.X - leftHand.X) > ArmStretchedThreadhold4PPT;

            //使用状态变量，避免多次重复发送键盘事件
            if (isRightHandStretched)
            {
                if (!isBackGestureActive && !isForwardGestureActive)
                {
                    isForwardGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    textBlock13.Text = "Right";
                }
            }
            else
            {
                isForwardGestureActive = false;
            }

            if (isLeftHandStretched)
            {
                if (!isBackGestureActive && !isForwardGestureActive)
                {
                    isBackGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Left}");
                    textBlock13.Text = "Left";
                }
            }
            else
            {
                isBackGestureActive = false;
            }

            ////双手同时举起，控制PPT播放
            if (isLeftHandRaised && isRightHandRaised)
            {
                if (!isBlackScreenActive)
                {
                    isBlackScreenActive = true;
                    System.Windows.Forms.SendKeys.SendWait("+{F5}");
                    textBlock13.Text = "Shift+F5";
                }
            }
            else
            {
                isBlackScreenActive = false;
            }

        }

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
                    //如果手没有伸出，则不作跟踪。以头部的Z坐标为参照点
                    if (head.Position.Z - hand.Position.Z <= ArmZStretchedThreshold)
                    {
                        return;
                    }
                    //模拟鼠标移动
                    TrackHand2SimulateMouseMove(hand);

                    // Righthand has been cancelled,this code is used to get track
                    bool isLeftHandStretched = ((head.Position.X - leftHand.Position.X) > ArmXStretchedThreshold) &&
                        ((head.Position.Z - leftHand.Position.Z) < ArmZStretchedThreshold);

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
                
    }

}