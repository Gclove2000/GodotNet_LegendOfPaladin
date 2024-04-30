using Godot;
using GodotNet_LegendOfPaladin2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class EnemySceneModel : ISceneModel
    {
        private PrintHelper printHelper;

        private CharacterBody2D characterBody2D;

        private CollisionShape2D collisionShape2D;

        private Sprite2D sprite2D;

        private AnimationPlayer animationPlayer;

        public enum DirectionEnum
        {
            Left, Right
        }
        public DirectionEnum Direction { get; set; }


        /// <summary>
        /// 最大速度
        /// </summary>
        public int MaxSpeed { get; set; } 

        /// <summary>
        /// 加速度
        /// </summary>
        public int AccelerationSpeed { get; set; } 


        public EnemySceneModel(PrintHelper printHelper)
        {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(EnemySceneModel));  
        }
        public EnemySceneModel() { }
        public override void Process(double delta)
        {
        }

        public override void Ready()
        {
            characterBody2D = Scene.GetNode<CharacterBody2D>("CharacterBody2D");
            collisionShape2D = characterBody2D.GetNode<CollisionShape2D>("CollisionShape2D");
            sprite2D = characterBody2D.GetNode<Sprite2D>("Sprite2D");
            animationPlayer = characterBody2D.GetNode<AnimationPlayer>("AnimationPlayer");
            printHelper.Debug("加载成功!");
            printHelper.Debug($"当前朝向是:{Direction}");
        }
    }
}
