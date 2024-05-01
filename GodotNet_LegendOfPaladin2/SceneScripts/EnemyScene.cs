using Godot;
using GodotNet_LegendOfPaladin2.SceneModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneScripts
{
    public partial class EnemyScene : Node2D
    {



        [Export]
        public EnemySceneModel.DirectionEnum Direction
        {
            get => Model.Direction;
            set => Model.Direction = value;
        }

        [Export]
        public int MaxSpeed = 180;
        [Export]
        public int AccelerationSpeed = 2000;

        [Export(PropertyHint.Range,"1,3,")]
        public int AnimationType = 1;

        public EnemySceneModel Model { get; private set; }

        public EnemyScene()
        {
            
            
            Model = Program.Services.GetService<EnemySceneModel>();
            Model.Scene = this;
        }


        public override void _Ready()
        {
            Model.MaxSpeed = MaxSpeed;
            Model.AccelerationSpeed = AccelerationSpeed;
            Model.AnimationType = AnimationType;
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
