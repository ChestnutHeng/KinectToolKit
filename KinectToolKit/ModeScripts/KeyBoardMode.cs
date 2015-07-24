using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using System.Timers;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace KinectToolkit
{

    public partial class MainWindow : Window
    {
        enum mode { single, much, mouse, car };
        mode status = mode.single;
        public double bdiffer;
        public int get_status;
        public SkeletonPoint HipCenter;
        public SkeletonPoint handRight;
        public SkeletonPoint handLeft;
        public SkeletonPoint spine;
        public SkeletonPoint head;
        public SkeletonPoint neck;
        public SkeletonPoint leftleg;

        public SkeletonPoint leftshoulder;
        public SkeletonPoint rightshoulder;

        //Keyboard

        private const double ArmRaisedThreshhold = 0.2; //arms chuizhi /m
        private const double ArmStretchedThreadhold = 0.5; //arms shuiping /m
        private const double JumpDiffThreadhold = 0.05; //jump /m
        private double headPreviousPosition = 2.0; // firstvalue /m

        private void playSuperMario(Skeleton s)
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


            if ((head.Y - headPreviousPosition) > JumpDiffThreadhold)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Up);
                
                textBlock14.Text = "Up";
            }
            headPreviousPosition = head.Y;

            
            if (isRightHandStretched)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Right);
                textBlock14.Text = "Right";
            }

            if (isLeftHandStretched)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Left);
                textBlock14.Text = "Left";

            }

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

 

        private void PlayCar(Skeleton s)
        {

            HipCenter = s.Joints[JointType.HipCenter].Position;
            handRight = s.Joints[JointType.HandRight].Position;
            handLeft = s.Joints[JointType.HandLeft].Position;
            spine = s.Joints[JointType.Spine].Position;
            head = s.Joints[JointType.Head].Position;
            neck = s.Joints[JointType.ShoulderCenter].Position;
            leftleg = s.Joints[JointType.KneeLeft].Position;

            SkeletonPoint leftshoulder = s.Joints[JointType.ShoulderLeft].Position;
            SkeletonPoint rightshoulder = s.Joints[JointType.ShoulderRight].Position;

            //flip count
            fcount++;
            differ = 0;
            bdiffer = 0;

            //mode data
            Set_stand_data();
            Set_bow_data();


           // modeRec();

            if(comboBox4.SelectedIndex == 0) 
                get_status = angleRec();
            else 
                get_status = vectorRec();

            textBlock16.Text = "";
            if (get_status == 1)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Up);
                textBlock17.Text = "↑";
            }
            else if (get_status == -1)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Down);
                textBlock17.Text = "↓";
            }
            else textBlock17.Text = "";

            bool isRightHandPut = head.Z - handRight.Z  > 0.25;
            bool isLeftHandPut = head.Z - handLeft.Z > 0.25;


            if (isRightHandPut)
            {

                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Right);
                textBlock18.Text = "→";
            }else if (isLeftHandPut)
            {
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Left);
                textBlock18.Text = "←";
            }
            else textBlock18.Text = " ";
            
            


        }
        private void installtextblock()
        {

           textBlock16.Text = "动";
           textBlock17.Text = "无";
           textBlock18.Text = "作";
           textBlock14.Text = "无动作";
           textBlock13.Text = "无动作";
        }

                
    }

}
