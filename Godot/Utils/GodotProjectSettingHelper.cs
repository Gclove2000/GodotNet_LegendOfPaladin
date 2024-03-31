using Godot;
using GodotProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.Utils
{
    public class GodotProjectSettingHelper
    {
        private NlogHelper nlogHelper;

        public readonly float Gravity = 0;

        /// <summary>
        /// 输入映射
        /// </summary>
        public enum InputMapEnum { move_left, move_right, move_up, move_down, jump }
        public GodotProjectSettingHelper(NlogHelper nlogHelper)
        {
            this.nlogHelper = nlogHelper;
            Gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
        }
    }
}
