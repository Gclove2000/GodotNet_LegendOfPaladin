using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.Utils
{
    public class GodotProjectSettingHelper
    {
        private NlogHelper nlogHelper;

        public readonly float Gravity = 0;


        public GodotProjectSettingHelper(NlogHelper nlogHelper)
        {
            this.nlogHelper = nlogHelper;
            Gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
        }
    }
}
