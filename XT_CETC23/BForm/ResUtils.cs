using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySkins.Entity;

namespace MySkins.Util
{
    /// <summary>
    /// 资源辅助类
    /// </summary>
    class ResUtils
    {
        /// <summary>
        /// 根据资源名称获取图像
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <returns>图像</returns>
        public static Bitmap GetResAsImage(string name)
        {
            if (name == null || name == "")
            {
                return null;
            }
            return (Bitmap)XT_CETC23.Properties.Resources.ResourceManager.GetObject(name);
        }

        /// <summary>
        /// 图片按钮的背景图是4个,根据状态获取其中背景图
        /// </summary>
        /// <param name="name">图片名称</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static Bitmap GetResWithState(String name, ControlState state)
        {
            Bitmap bitmap = (Bitmap)GetResAsImage(name);
            if (bitmap == null)
            {
                return null;
            }
            int block = 0;
            switch (state)
            {
                case ControlState.Normal: block = 0; break;
                case ControlState.MouseOver: block = 1; break;
                case ControlState.MouseDown: block = 2; break;
                case ControlState.Disable: block = 3; break;
            }
            int width = bitmap.Width / 4;
            Rectangle rect = new Rectangle(block * width, 0, width, bitmap.Height);
            return bitmap.Clone(rect, bitmap.PixelFormat);
        }
    }
}
