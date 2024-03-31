using Godot;
using GodotNet_LegendOfPaladin.SceneModels;
using GodotProgram.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.SceneScripts
{
    public partial class PlayerScene : Node2D
    {

        public PlayerSceneModel Model { get; set; }

        public PlayerScene()
        {
            Model = Program.Services.GetService<PlayerSceneModel>();
            Model.Sence = this;
        }

        public override void _Ready()
        {

            Model.Ready();
            base._Ready();
        }

        public override void _Process(double delta)
        {

            Model.Process(delta);
            base._Process(delta);
        }
    }
}
