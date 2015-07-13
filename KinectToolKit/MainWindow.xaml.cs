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
        public MainWindow()
        {
            InitializeComponent();
        }
        int label = 0;
        bool firstLoaded = false;
        bool firstClick = false;
        enum mode { single, much ,mouse};
        mode status = mode.single;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            GUIloaded();
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            WindowsClosing = true;
            stopKinect();
        }

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
                firstLoaded = true;
                if (_kinect.Status.ToString() == "Not Powered") GUIloaded();
             //   KinectSensor oldKinect = (KinectSensor)_kinect.OldValue;

                if (label == 1) stopKinect();

              //  KinectSensor kinect = (KinectSensor)_kinect.NewValue;

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

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }



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
                else 



                //mmmmmmmmmmmmm


                    if (checkBox1.IsChecked == true && status == mode.mouse)
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
                

            } else { textBlock6.Text = "你在哪里？"; }
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
                KinectSuperMario.KeyboardToolkit.Keyboard.Type(Key.Down);
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


        //button area
        int buttonangleup = 5;
        int buttonangledown = 5;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;//父窗体隐藏
          // Window Window1 = new Window();
           // Window1.Owner = this;//指定子窗体的父窗体是自己
          //  Window1.ShowDialog();
          //  NavigationWindow window = new NavigationWindow();
          //  window.Source = new Uri("Page1.xaml", UriKind.Relative);
           //   Window1.Visibility  = 
        }
        private void UP_Click(object sender, RoutedEventArgs e)
        {
            UP360();
        }
        private void down_Click(object sender, RoutedEventArgs e)
        {
            Down360();
        }
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

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (label == 0)
            {
                startKinect();
                label = 1;
            }
            else
            {
                stopKinect();
                label = 0;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) UP360();
            if (e.Key == Key.Down) Down360();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
  //Video box
        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            status = mode.much;
            MessageBox.Show("double mode");
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            status = mode.single;
            if(firstClick == true) MessageBox.Show("single mode");
            firstClick = true;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonangleup = (5 - comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonangledown = (5 - comboBox2.SelectedIndex);
        }
    //Gui area
        private void GUIloaded()
        {
            //cmbox
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            checkBox1.IsChecked = false;
        }
        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            Timer    timer1 = new Timer();
            timer1.Interval = 5;
            timer1.Start() ;        
        }
          
        private void kinectSensorChooser1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //solving
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            status = mode.mouse;
        }


    }
}
