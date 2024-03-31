using Godot;
using GodotNet_LegendOfPaladin.SceneModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.SceneScripts
{
    public partial class BackgroudScene :Node2D
    {
        public BackgroundSceneModel Model { get; set; }

        public BackgroudScene()
        {
            Model = Program.Services.GetService<BackgroundSceneModel>();
            Model.Sence = this;
        }

        public override void _Ready()
        {

            Model.Ready();
            base._Ready();
        }

        public override void _Process(double delta)
        {
            Model?.Process(delta);
            base._Process(delta);
        }

    }
}
