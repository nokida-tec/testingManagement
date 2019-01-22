using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySkins.Entity
{
    /// <summary>
    /// 控件状态
    /// </summary>
    public enum ControlState
    {
        Normal = 1,//控件默认时
        MouseOver = 2,//鼠标移上控件时
        MouseDown = 3,//鼠标按下控件时
        Disable = 4 //当控件不可用时
    }
}
