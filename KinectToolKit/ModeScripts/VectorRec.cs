/*
 *  Copyright (C) 2015 XiaoJSoft & ChestnutHeng. All rights reserved.
 *
 *  Action recognition algorithm which depends on the cross multiplination of vectors.
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

        private struct MyVector
        {
            public float x, y, z;
        };

        private const int Vec_FlipLimit = 10;
        private MyVector[] Vec_queue = new MyVector[Vec_FlipLimit];
        private int Current_pt = 0;
        private MyVector CrossMultiply(MyVector v1, MyVector v2)
        {
            MyVector ret;
            ret.x = v1.y * v2.z - v1.z * v2.y;
            ret.y = v1.z * v2.x - v1.x * v2.z;
            ret.z = v1.x * v2.y - v1.y * v2.x;
            return ret;
        }

        private MyVector CalculateAverageVector()
        {
            MyVector average;
            average.x = 0;
            average.y = 0;
            average.z = 0;
            if (Current_pt == Vec_FlipLimit)
            {
                for (int i = 0; i < Vec_FlipLimit - 1; ++i)
                {
                    MyVector vx;
                    vx = CrossMultiply(Vec_queue[i], Vec_queue[i + 1]);
                    average.x += vx.x;
                    average.y += vx.y;
                    average.z += vx.z;
                }
                average.x /= Vec_FlipLimit;
                average.y /= Vec_FlipLimit;
                average.z /= Vec_FlipLimit;
            }
            return average;
        }

        private void PushData(MyVector new_vec)
        {
            if (Current_pt == Vec_FlipLimit)
            {
                //                SkeletonPoint tmp;
                for (int i = 0; i < Vec_FlipLimit - 1; ++i)
                {
                    Vec_queue[i] = Vec_queue[i + 1];
                }
                Vec_queue[Vec_FlipLimit - 1] = new_vec;
            }
            else
            {
                Vec_queue[Current_pt] = new_vec;
                Current_pt++;
            }
        }

        public int vectorRec()
        {

            MyVector _vec;
            _vec.x = spine.X - head.X;
            _vec.y = spine.Y - head.Y;
            _vec.z = spine.Z - head.Z;
            PushData(_vec);
            MyVector ave = CalculateAverageVector();
            

            if (ave.x < 0 && System.Math.Abs(ave.x) > 0.001 && _vec.z > 0.06)
            {
                forstate = 1;

            }
            else if (ave.x > 0 && System.Math.Abs(ave.x) > 0.001 && _vec.z < 0.06)
            {
                forstate = -1;
            }
            else if (ave.x > 0 && System.Math.Abs(ave.x) > 0.001 && _vec.z > 0.06)
            {
                forstate = 0;
            }
            else if (ave.x < 0 && System.Math.Abs(ave.x) > 0.001 && _vec.z < 0.06)
            {
                forstate = 0;
            }

            if (System.Math.Abs(_vec.z) < 0.02)
            {
                forstate = 0;
            }
            switch (forstate)
            {
                case 1:
                    return 1;
                case -1:
                    return -1;
            }
            return 0;
        }
        

    }
}