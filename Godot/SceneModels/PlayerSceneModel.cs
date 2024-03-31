using Godot;
using GodotNet_LegendOfPaladin.Utils;
using GodotProgram.Interfaces;
using GodotProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GodotNet_LegendOfPaladin.Utils.GodotProjectSettingHelper;

namespace GodotNet_LegendOfPaladin.SceneModels
{
    public class PlayerSceneModel : ISceneModel
    {



        public const float RUN_SPEED = 200;
        public const float JUMP_VELOCITY = -300;
        /// <summary>
        /// 最长跳跃等待时间
        /// </summary>
        public const int JUMP_WAIT_TIME = 3000;
        /// <summary>
        /// 初始化的时候让时间往后退一点，防止时间过快
        /// </summary>
        private DateTime jumpLastTime = DateTime.Now.AddDays(-1);

        //枚举类型，防止拼写错误
        public enum AnimationFlame { idel, running, jump }




        #region IOC注入

        private NlogHelper nlogHelper;
        private GodotProjectSettingHelper godotProjectSettingHelper;
        public PlayerSceneModel(NlogHelper nlogHelper, GodotProjectSettingHelper godotProjectSettingHelper)
        {
            this.nlogHelper = nlogHelper;
            this.godotProjectSettingHelper = godotProjectSettingHelper;
        }
        #endregion


        #region 子节点获取

        private CharacterBody2D characterBody2D;

        private AnimationPlayer animationPlayer;

        private Sprite2D sprite2D;

        public override void Ready()
        {

            nlogHelper.Debug($"当前重力值为:{godotProjectSettingHelper.Gravity}");
            //初始化子节点
            characterBody2D = this.Sence.GetNode<CharacterBody2D>("CharacterBody2D");
            animationPlayer = this.Sence.GetNode<AnimationPlayer>("AnimationPlayer");
            sprite2D = this.Sence.GetNode<Sprite2D>("Sprite2D");
            //播放动画
            animationPlayer.Play(AnimationFlame.idel.ToString());


        }
        #endregion
        public override void Process(double delta)
        {
            //初始化速度
            var velocity = new Vector2();
            //初始化动画节点
            var animation = AnimationFlame.idel;
            var direction = Input.GetAxis(InputMapEnum.move_left.ToString(), InputMapEnum.move_right.ToString());
            var y = godotProjectSettingHelper.Gravity * (float)delta;
            var x = direction * RUN_SPEED;
            var isOnFloor = characterBody2D.IsOnFloor();
            //在C# 中，
            velocity = characterBody2D.Velocity;
            //X是最终速度，所以不需要相加
            velocity.X = x;
            //给角色一个速度，因为重力是加速度，所以角色的速度会不断的增加。
            velocity.Y += y;

            if (Input.IsActionJustPressed(InputMapEnum.jump.ToString()))
            {
                jumpLastTime = DateTime.Now;
            }

            if (isOnFloor)
            {
                //如果在地上并且按下跳跃，则直接给一个y轴的速度

                if (jumpLastTime.AddMilliseconds(JUMP_WAIT_TIME) > DateTime.Now)
                {
                    velocity.Y = JUMP_VELOCITY;
                    jumpLastTime = DateTime.Now.AddDays(-1);
                }

                if (Mathf.IsZeroApprox(direction))
                {
                    animation = AnimationFlame.idel;
                }
                else
                {
                    animation = AnimationFlame.running;
                }
            }
            else
            {
                animation = AnimationFlame.jump;
            }

            //方向翻转
            if (!Mathf.IsZeroApprox(direction))
            {
                sprite2D.FlipH = direction < 0;
            }

            characterBody2D.Velocity = velocity;
            //让物体以这个速度进行移动
            characterBody2D.MoveAndSlide();
            //同步场景根节点位置
            var postion = characterBody2D.Position;
            characterBody2D.Position = new Vector2(0, 0);
            this.Sence.Position += postion;

            animationPlayer.Play(animation.ToString());
        }
    }
}
