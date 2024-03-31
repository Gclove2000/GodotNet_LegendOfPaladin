using Godot;
using GodotNet_LegendOfPaladin.Utils;
using GodotProgram.Interfaces;
using GodotProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin.SceneModels
{
    public class PlayerSceneModel : ISceneModel
    {

        private CharacterBody2D characterBody2D;

        private AnimationPlayer animationPlayer;

        public const float PLAYER_SPEED = 200;

        public enum AnimationFlame { REST, idel }




        #region IOC注入

        private NlogHelper nlogHelper;
        private GodotProjectSettingHelper godotProjectSettingHelper;

        public PlayerSceneModel(NlogHelper nlogHelper, GodotProjectSettingHelper godotProjectSettingHelper)
        {
            this.nlogHelper = nlogHelper;
            this.godotProjectSettingHelper = godotProjectSettingHelper;
        }
        #endregion



        public override void Process(double delta)
        {
            //给角色一个速度，因为重力是加速度，所以角色的速度会不断的增加。
            characterBody2D.Velocity += new Vector2(0, godotProjectSettingHelper.Gravity * (float)delta);

            //让物体以这个速度进行移动
            characterBody2D.MoveAndSlide();

            var postion = characterBody2D.Position;
            characterBody2D.Position = new Vector2(0, 0);
            this.Sence.Position += postion;

        }

        public override void Ready()
        {

            nlogHelper.Debug($"当前重力值为:{godotProjectSettingHelper.Gravity}");

            characterBody2D = this.Sence.GetNode<CharacterBody2D>("CharacterBody2D");
            animationPlayer = this.Sence.GetNode<AnimationPlayer>("AnimationPlayer");

            animationPlayer.Play(AnimationFlame.idel.ToString());
        }
    }
}
