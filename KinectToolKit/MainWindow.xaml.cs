﻿using System;
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
        //bool firstLoaded = false;
        bool firstClick = false;
        public static bool Beginer = false;
        public int fcount = 0;
        public int click = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            GUIloaded();
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            WindowsClosing = true;
            stopKinect();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (click == 0)
                click = 1;
            else click = 0;
        }

        

 

        

        

        
    }
}
