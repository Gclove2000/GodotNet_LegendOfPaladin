using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public abstract class ISceneModel
    {

        public Node2D Scene { get; set; }

        public abstract void Ready();
        public abstract void Process(double delta);
    }
}
