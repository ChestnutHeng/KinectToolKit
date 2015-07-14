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
        //button area
        int buttonangleup = 5;
        int buttonangledown = 5;
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
            Timer timer1 = new Timer();
            timer1.Interval = 5;
            timer1.Start();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;//父窗体隐藏
            // Window Window1 = new Window();
            // Window1.Owner = this;//指定子窗体的父窗体是自己
            //  Window1.ShowDialog();
            //  NavigationWindow window = new NavigationWindow();
            //  window.Source = new Uri("Page1.xaml", UriKind.Relative);
        }
        private void UP_Click(object sender, RoutedEventArgs e)
        {
            UP360();
        }
        private void down_Click(object sender, RoutedEventArgs e)
        {
            Down360();
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
            if (firstClick == true) MessageBox.Show("single mode");
            firstClick = true;
        }
        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            status = mode.car;
            MessageBox.Show("Now you can control your car.");
        }
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Now you can control your mouse.");
            status = mode.mouse;
        }
        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonangleup = (5 - comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonangledown = (5 - comboBox2.SelectedIndex);
        }
    }

}