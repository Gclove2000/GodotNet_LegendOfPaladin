﻿using Godot;
using GodotNet_LegendOfPaladin2.SceneModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneScripts
{
    public partial class PlayerScene : Node2D
    {

        public PlayerSceneModel Model { get; set; }

        [Export]
        public bool CanCombo
        {
            get => Model.CanCombo;
            set { Model.CanCombo = value; }
        }




        public PlayerScene()
        {
            Model = Program.Services.GetService<PlayerSceneModel>();
            Model.Scene = this;
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
