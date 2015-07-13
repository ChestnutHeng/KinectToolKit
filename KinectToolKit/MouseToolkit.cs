using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace KinectToolkit
{
    class MouseToolkit
    {
        [Flags]
        public enum MouseEventFlag : uint
        {
            /// <summary>
            /// //移动鼠标
            /// </summary>
            Move = 0x0001,
            /// <summary>
            /// //模拟鼠标左键按下参数
            /// </summary>
            LeftDown = 0x0002,
            /// <summary>
            /// //模拟鼠标左键抬起参数
            /// </summary>
            LeftUp = 0x0004,
            /// <summary>
            /// //模拟鼠标右键按下
            /// </summary>
            RightDown = 0x0008,
            /// <summary>
            /// //模拟鼠标右键抬起
            /// </summary>
            RightUp = 0x0010,
            /// <summary>
            /// //模拟鼠标中键按下
            /// </summary>
            MiddleDown = 0x0020,
            /// <summary>
            /// // 模拟鼠标中键抬起
            /// </summary>
            MiddleUp = 0x0040,
            /// <summary>
            /// X button扩展键按下
            /// </summary>
            XDown = 0x0080,
            /// <summary>
            /// X button扩展键抬起
            /// </summary>
            XUp = 0x0100,
            /// <summary>
            /// 模拟鼠标滚轴
            /// </summary>
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            /// <summary>
            ///  //标示是否采用绝对坐标，范围：0-65535
            /// </summary>
            Absolute = 0x8000
        }

        /// <summary>
        /// 模拟鼠标操作
        /// </summary>
        /// <param name="dwFlags">模拟的鼠标消息</param>
        /// <param name="dx">x</param>
        /// <param name="dy">y</param>
        /// <param name="dwData">0</param>
        /// <param name="dwExtraInfo">0</param>
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(MouseEventFlag dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">y</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

    }
}
