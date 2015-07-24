/*
 *  Copyright (C) 2015 ChestnutHeng. All rights reserved.
 *
 *  Action recognition algorithm which depends on the model detection.
 */

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


        private SkeletonPoint transfer(SkeletonPoint now, SkeletonPoint spine)
        {
            SkeletonPoint relative = new SkeletonPoint();
            float realnum = 1.85F / now.Z;
            relative.X = (now.X - spine.X) * realnum;
            relative.Y = (now.Y - spine.Y) * realnum;
            relative.Z = now.Z - spine.Z;
            return relative;
        }
        public int forstate = 0;
        public int state_status = 0;
        public double differ = 0;
        public double last_differ = 0;
        public bool status_move = false;
        public float permit = 0;
        public double last_status = 0;

        private const int QueueLimit = 60;
        private actionarray[] skeletonqueue = new actionarray[QueueLimit];


        private void PushQueue(actionarray new_vec)
        {
            if (Current_pt == QueueLimit)
            {
                //                SkeletonPoint tmp;
                for (int i = 0; i < Vec_FlipLimit - 1; ++i)
                {
                    skeletonqueue[i] = skeletonqueue[i + 1];
                }
                skeletonqueue[Vec_FlipLimit - 1] = new_vec;
            }
            else
            {
                skeletonqueue[Current_pt] = new_vec;
                Current_pt++;
            }
        }
        public int modeRec()
        {

            //define now array
            actionarray nowarray;
            ArrayList temparray = new ArrayList();
            nowarray.head = head;
            nowarray.neck = neck;
            //fit zb
            SkeletonPoint relativehead = transfer(head, spine);
            SkeletonPoint relativeneck = transfer(neck, spine);

            //into file
            if (fcount <= 60 &&　false)
            {
                FileStream data_file = new FileStream("C:\\stand_data.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(data_file);
                sw.WriteLine(relativehead.X.ToString());
                sw.WriteLine(relativehead.Y.ToString());
                sw.WriteLine(relativehead.Z.ToString());
                sw.WriteLine("------------");
                sw.WriteLine(relativeneck.X.ToString());
                sw.WriteLine(relativeneck.Y.ToString());
                sw.WriteLine(relativeneck.Z.ToString());
                sw.WriteLine("============");
                sw.Close();
                data_file.Close();

            }
            else
            {
                //Console.WriteLine("Over");
            }

            //push
            PushQueue(nowarray);
            if (fcount > 60)
            {
                for (int i = 0; i < 60; ++i)
                {
                    double differx = skeletonqueue[i].head.X - stand_data[i].head.X;
                    double differy = skeletonqueue[i].head.Y - stand_data[i].head.Y;
                    double differz = skeletonqueue[i].head.Z - stand_data[i].head.Z;
                    differ += System.Math.Sqrt(differx * differx * 10 + differy * differy * 10 + differz * differz * 10);
                    double ndifferx = skeletonqueue[i].neck.X - stand_data[i].neck.X;
                    double ndiffery = skeletonqueue[i].neck.Y - stand_data[i].neck.Y;
                    double ndifferz = skeletonqueue[i].neck.Z - stand_data[i].neck.Z;
                    differ += System.Math.Sqrt(ndifferx * ndifferx * 10 + ndiffery * ndiffery * 10 + ndifferz * ndifferz * 10);

                    double bdifferx = skeletonqueue[i].head.X - bow_data[i].head.X;
                    double bdiffery = skeletonqueue[i].head.Y - bow_data[i].head.Y;
                    double bdifferz = skeletonqueue[i].head.Z - bow_data[i].head.Z;
                    bdiffer += System.Math.Sqrt(bdifferx * bdifferx * 10 + bdiffery * bdiffery * 10 + bdifferz * bdifferz * 10);
                    double bndifferx = skeletonqueue[i].neck.X - bow_data[i].neck.X;
                    double bndiffery = skeletonqueue[i].neck.Y - bow_data[i].neck.Y;
                    double bndifferz = skeletonqueue[i].neck.Z - bow_data[i].neck.Z;
                    bdiffer += System.Math.Sqrt(bndifferx * bndifferx * 10 + bndiffery * bndiffery * 10 + bndifferz * bndifferz * 10);

                }
                if (fcount % 60 == 0)
                {
                    last_status = bdiffer;
                }
                System.Console.WriteLine((last_status - bdiffer).ToString());
                double ddiffer = last_status - bdiffer;
                bool bow_state = (17 > ddiffer && ddiffer > 8);
                bool back_state = (-17 < ddiffer && ddiffer < -8);
                bool big_back = (ddiffer <= -20);
                bool big_bow = (ddiffer > 17);

                bool last_bow_state = (17 > last_differ && last_differ > 8);
                bool last_back_state = (-17 < last_differ && last_differ < -8);
                bool last_big_back = (last_differ <= -20);
                bool last_big_bow = (last_differ > 17);

                last_differ = ddiffer;

                if (last_back_state == back_state) return -2;
                if (last_bow_state == bow_state) return -2;
                if (big_back == last_big_back) return -2;
                if (big_bow == last_big_bow) return -2;

                if (bow_state) state_status -= 1;
                if (back_state) state_status += 1;
                if (big_bow) state_status -= 2;
                if (big_back) state_status += 2;

                if (state_status == 0)
                    return 0;// "Stand";
                else if (state_status == 1)
                    return 1; // "Bow";
                else if (state_status == 2)
                    return -1; //"Back";


                
            }
            return -2;
        }
        

    }
}