﻿using Godot;
using GodotNet_LegendOfPaladin2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.TextServer;

namespace GodotNet_LegendOfPaladin2.SceneModels
{
    public class PlayerSceneModel : ISceneModel
    {
        private PrintHelper printHelper;
        #region 常量
        /// <summary>
        /// 速度
        /// </summary>
        public const float RUN_SPEED = 200;

        /// <summary>
        /// 加速度，为了显示明显，20秒内到达RUN_SPEED的速度
        /// </summary>
        public const float ACCELERATION = (float)(RUN_SPEED / 20);

        /// <summary>
        /// 跳跃速度
        /// </summary>
        public const float JUMP_SPEED = -350;

        /// <summary>
        /// 蹬墙跳的速度
        /// </summary>
        public readonly Vector2 WALL_JUMP_VELOCITY = new Vector2(400, -320);

        #endregion

        private Sprite2D sprite2D;

        private CharacterBody2D characterBody2D;

        private AnimationPlayer animationPlayer;

        private Camera2D camera2D;

        public enum AnimationEnum { REST, Idel, Running, Jump, Fall, Land, WallSliding }

        public AnimationEnum AnimationState { get; private set; }

        public bool IsLand { get; private set; } = true;

        public float Direction { get; private set; } = 0;
        
        /// <summary>
        /// 跳跃重置时间
        /// </summary>
        public const float JudgeIsJumpTime = 0.5f;
        private float isJumpTime = 0;

        public PlayerSceneModel(PrintHelper printHelper)
        {

            this.printHelper = printHelper;
            this.printHelper.SetTitle(nameof(PlayerSceneModel));
        }


        public override void Process(double delta)
        {
            PlayerMove(delta);

            SetAnimation();
        }

        /// <summary>
        /// 角色移动
        /// </summary>
        /// <param name="delta"></param>
        private void PlayerMove(double delta)
        {
            var velocity = characterBody2D.Velocity;
            velocity.Y += ProjectSettingHelper.Gravity * (float)delta;
            Direction = Input.GetAxis(ProjectSettingHelper.InputMapEnum.move_left.ToString(),
                ProjectSettingHelper.InputMapEnum.move_right.ToString());
            //原本直接赋值
            //velocity.X = direction*RUN_SPEED;
            //现在使用加速度
            velocity.X = Mathf.MoveToward(velocity.X, Direction * RUN_SPEED, ACCELERATION);
            //按下跳跃键，就将跳跃时间设置为判断区间
            if (Input.IsActionJustPressed(ProjectSettingHelper.InputMapEnum.jump.ToString()))
            {
                isJumpTime = JudgeIsJumpTime;
            }
            //慢慢变成0
            isJumpTime = (float)Mathf.MoveToward(isJumpTime,0,delta);

            //如果在跳跃时间的判断内
            if (isJumpTime != 0)
            {
                
                if (characterBody2D.IsOnFloor())
                {
                    //进行跳跃之后，跳跃时间结束
                    isJumpTime = 0;
                    velocity.Y = JUMP_SPEED;
                    AnimationState = AnimationEnum.Jump;
                }
                else if (AnimationState == AnimationEnum.WallSliding)
                {
                    //进行跳跃之后，跳跃时间结束
                    isJumpTime = 0;
                    velocity = WALL_JUMP_VELOCITY;
                    //获取墙面的法线的方向
                    velocity.X *= characterBody2D.GetWallNormal().X;
                    AnimationState = AnimationEnum.Jump;

                }
            }

            characterBody2D.Velocity = velocity;
            characterBody2D.MoveAndSlide();

        }

        private void SetAnimation()
        {
            switch (AnimationState)
            {
                case AnimationEnum.Idel:
                    if (!Mathf.IsZeroApprox(Direction))
                    {
                        AnimationState = AnimationEnum.Running;
                    }
                    break;
                case AnimationEnum.Jump:
                    if (characterBody2D.Velocity.Y < 0)
                    {
                        AnimationState = AnimationEnum.Fall;

                    }
                    else if (characterBody2D.IsOnWall())
                    {
                        AnimationState = AnimationEnum.WallSliding;

                    }

                    break;
                case AnimationEnum.Running:
                    if (Mathf.IsZeroApprox(Direction))
                    {
                        AnimationState = AnimationEnum.Idel;
                    }
                    break;
                case AnimationEnum.Fall:

                    if (Mathf.IsZeroApprox(characterBody2D.Velocity.Y))
                    {
                        AnimationState = AnimationEnum.Land;
                        //开启异步任务，如果过了400毫秒，仍然是Land，则转为Idel
                        Task.Run(async () =>
                        {
                            await Task.Delay(400);
                            if (AnimationState == AnimationEnum.Land)
                            {
                                AnimationState = AnimationEnum.Idel;

                            }
                        });
                    }
                    else if (characterBody2D.IsOnWall())
                    {
                        AnimationState = AnimationEnum.WallSliding;

                    }
                    break;
                case AnimationEnum.Land:

                    break;

                case AnimationEnum.WallSliding:
                    if (!characterBody2D.IsOnWall())
                    {
                        AnimationState = AnimationEnum.Fall;
                    }
                    break;
            }

            if (!Mathf.IsZeroApprox(Direction))
            {
                sprite2D.FlipH = Direction < 0;
            }
            PlayAnimation();
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        private void PlayAnimation()
        {
            //printHelper.Debug(AnimationState.ToString());

            animationPlayer.Play(AnimationState.ToString());
        }

        /// <summary>
        /// 是否准备好了
        /// </summary>
        public override void Ready()
        {
            characterBody2D = Scene.GetNode<CharacterBody2D>("CharacterBody2D");
            camera2D = characterBody2D.GetNode<Camera2D>("Camera2D");
            sprite2D = characterBody2D.GetNode<Sprite2D>("Sprite2D");
            animationPlayer = characterBody2D.GetNode<AnimationPlayer>("AnimationPlayer");
            printHelper.Debug("加载完成");
            AnimationState = AnimationEnum.Idel;
            PlayAnimation();
        }

        /// <summary>
        /// 设置相机
        /// </summary>
        /// <param name="rect2"></param>
        public void SetCameraLimit(Rect2 rect2)
        {
            camera2D.LimitLeft = (int)rect2.Position.X;
            //camera2D.LimitTop = (int)rect2.Position.Y;
            camera2D.LimitRight = (int)rect2.End.X;
            camera2D.LimitBottom = (int)rect2.End.Y;
            //printHelper.Debug(JsonConvert.SerializeObject(rect2));
        }
    }
}
