/*
 *  Copyright (C) 2015 ChestnutHeng. All rights reserved.
 *
 *  Action recognition algorithm which depends on the angle detection.
 */

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
using System.Collections;

namespace KinectToolkit
{

    public partial class MainWindow : Window
    {
        public int angleRec()
        {
            //angle
            SkeletonPoint leg = leftleg;
            leg.Z = spine.Z - 0.5F;

            double head_spine_lenx = head.X - spine.X;
            double head_spine_leny = head.Y - spine.Y;
            double head_spine_lenz = head.Z - spine.Z;

            double head_leg_lenx = head.X - leg.X;
            double head_leg_leny = head.Y - leg.Y;
            double head_leg_lenz = head.Z - leg.Z;

            double spine_leg_lenx = spine.X - leg.X;
            double spine_leg_leny = spine.Y - leg.Y;
            double spine_leg_lenz = spine.Z - leg.Z;


            double body_lengh = System.Math.Sqrt(head_spine_lenx * head_spine_lenx + head_spine_leny * head_spine_leny + head_spine_lenz * head_spine_lenz);
            double head_leg_legth = System.Math.Sqrt(head_leg_lenx * head_leg_lenx + head_leg_leny * head_leg_leny + head_leg_lenz * head_leg_lenz);
            double spine_leg_lenth = System.Math.Sqrt(spine_leg_lenx * spine_leg_lenx + spine_leg_leny * spine_leg_leny + spine_leg_lenz * spine_leg_lenz);

            double cos_main_angle = (body_lengh * body_lengh + spine_leg_lenth * spine_leg_lenth - head_leg_legth * head_leg_legth) / (2 * spine_leg_lenth * body_lengh);

            double angle = (System.Math.Acos(cos_main_angle) * 180F / 3.1415926535F);

            if (angle < 100)
                return 1;
            else if (angle > 148) return -1;
            else return 0;

        }
        
        

    }
}