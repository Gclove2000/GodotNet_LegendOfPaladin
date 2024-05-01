using Bogus;
using Godot;
using GodotNet_LegendOfPaladin2.Utils;
using Newtonsoft.Json;
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

        public RayCast2D WallCheck { get; private set; }

        public RayCast2D FloorCheck { get; private set; }

        public RayCast2D PlayerCheck { get; private set; }

        public enum DirectionEnum
        {
            Left = -1, Right = 1
        }

        //设置正向的方向
        private DirectionEnum direction = DirectionEnum.Right;
        public DirectionEnum Direction
        {
            get => direction;
            //这个是一个生命周期的问题，属性的设置比树节点的加载更早
            //，所以我们会在Ready里面使用Direction = Direction来触发get函数
            set
            {
                if (characterBody2D != null && direction != value)
                {
                    printHelper.Debug($"设置朝向,{value}");
                    var scale = characterBody2D.Scale;
                    //注意反转是X=-1。比如你左反转到右是X=-1，你右又反转到左也是X=-1。不是X=-1就是左，X=1就是右。
                    scale.X = -1;
                    characterBody2D.Scale = scale;
                    direction = value;
                }

            }
        }



        public enum AnimationEnum
        {
            Hit, Idle, Run, Walk

        }

        public AnimationEnum Animation = AnimationEnum.Idle;

        /// <summary>
        /// 动画持续时间
        /// </summary>
        private float animationDuration = 0;

        /// <summary>
        /// 最大速度
        /// </summary>
        public int MaxSpeed { get; set; }

        /// <summary>
        /// 加速度
        /// </summary>
        public int AccelerationSpeed { get; set; }


        /// <summary>
        /// Animation类型
        /// </summary>
        public int AnimationType { get; set; }




        public EnemySceneModel(PrintHelper printHelper)
        {
            this.printHelper = printHelper;
            printHelper.SetTitle(nameof(EnemySceneModel));
        }
        public EnemySceneModel() { }
        public override void Process(double delta)
        {
            animationDuration = (float)Mathf.MoveToward(animationDuration, 99, delta);
            SetAnimation();
            Move(delta);
            Direction = Direction;
        }

        public override void Ready()
        {
            characterBody2D = Scene.GetNode<CharacterBody2D>("CharacterBody2D");
            collisionShape2D = characterBody2D.GetNode<CollisionShape2D>("CollisionShape2D");
            sprite2D = characterBody2D.GetNode<Sprite2D>("Sprite2D");
            animationPlayer = characterBody2D.GetNode<AnimationPlayer>("AnimationPlayer");
            WallCheck = Scene.GetNode<RayCast2D>("CharacterBody2D/RayCast/WallCheck");
            FloorCheck = Scene.GetNode<RayCast2D>("CharacterBody2D/RayCast/FloorCheck");
            PlayerCheck = Scene.GetNode<RayCast2D>("CharacterBody2D/RayCast/PlayerCheck");
            PlayAnimation();
            printHelper.Debug("加载成功!");
            printHelper.Debug($"当前朝向是:{Direction}");
            Direction = Direction;
        }

        #region 动画状态机

        public void PlayAnimation()
        {
            var animationStr = string.Format("{0}_{1}", AnimationType, Animation);
            //printHelper.Debug($"播放动画，{animationStr}");
            animationPlayer.Play(animationStr);
        }
        public void SetAnimation()
        {

            //如果检测到玩家，就直接跑起来
            if (PlayerCheck.IsColliding())
            {
                //printHelper.Debug("检测到玩家，开始奔跑");
                Animation = AnimationEnum.Run;
                animationDuration = 0;
            }

            switch (Animation)
            {
                //如果站立时间大于2秒，则开始散步
                case AnimationEnum.Idle:
                    if (animationDuration > 2)
                    {
                        printHelper.Debug("站立时间过长，开始移动");

                        Animation = AnimationEnum.Walk;
                        animationDuration = 0;
                        //如果撞墙，则反转
                        if (WallCheck.IsColliding() || !FloorCheck.IsColliding())
                        {
                            if (Direction == DirectionEnum.Left)
                            {
                                Direction = DirectionEnum.Right;
                            }
                            else
                            {
                                Direction = DirectionEnum.Left;
                            }
                        }
                        //Direction = Direction;
                    }
                    break;
                //如果检测到墙或者没检测到地面或者动画时间超过4秒，则开始walk
                case AnimationEnum.Walk:
                    if ((WallCheck.IsColliding() || !FloorCheck.IsColliding()) || animationDuration > 4)
                    {
                        Animation = AnimationEnum.Idle;
                        animationDuration = 0;
                        printHelper.Debug("开始闲置");
                    }
                    break;
                //跑动不会立刻停下，当持续时间大于2秒后站立发呆
                case AnimationEnum.Run:
                    if (animationDuration > 2)
                    {
                        printHelper.Debug("追逐时间到达上限，停止");

                        Animation = AnimationEnum.Idle;
                        animationDuration = 0;
                    }
                    break;
            }

            PlayAnimation();

        }
        #endregion

        #region 物体移动
        public void Move(double delta)
        {
            var velocity = characterBody2D.Velocity;
            velocity.Y += ProjectSettingHelper.Gravity * (float)delta;

            switch (Animation)
            {
                case AnimationEnum.Idle:
                    velocity.X = 0;
                    break;
                case AnimationEnum.Walk:
                    velocity.X = MaxSpeed / 3;
                    break;
                case AnimationEnum.Run:
                    velocity.X = MaxSpeed;

                    break;
            }
            velocity.X = velocity.X * (int)Direction;
            characterBody2D.Velocity = velocity;
            //printHelper.Debug(JsonConvert.SerializeObject(characterBody2D.Velocity));
            characterBody2D.MoveAndSlide();

        }
        #endregion

    }
}
