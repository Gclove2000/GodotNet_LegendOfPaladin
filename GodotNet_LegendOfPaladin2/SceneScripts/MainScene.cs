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
    public partial class MainScene : Node2D
    {

        public MainSceneModel Model { get; set; }

        public MainScene()
        {
            Model = Program.Services.GetService<MainSceneModel>();
            Model.Scene = this;
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
