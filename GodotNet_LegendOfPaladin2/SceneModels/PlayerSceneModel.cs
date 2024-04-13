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
    public class PlayerSceneModel : ISceneModel
    {
        private PrintHelper printHelper;

        public const float RUN_SPEED = 200;

        public const float JUMP_SPEED = -350;

        private Sprite2D sprite2D;

        private CharacterBody2D characterBody2D;

        private AnimationPlayer animationPlayer;

        private Camera2D camera2D;

        public enum AnimationEnum {  REST,Idel,Running,Jump}


        public PlayerSceneModel(PrintHelper printHelper)
        {

            this.printHelper = printHelper;
            this.printHelper.SetTitle(nameof(PlayerSceneModel));
        }


        public override void Process(double delta)
        {
            PlayerMove(delta);
        }

        private void PlayerMove(double delta)
        {
            var velocity = characterBody2D.Velocity;
            velocity.Y += ProjectSettingHelper.Gravity * (float)delta;
            var direction = Input.GetAxis(ProjectSettingHelper.InputMapEnum.move_left.ToString(),
                ProjectSettingHelper.InputMapEnum.move_right.ToString());
            velocity.X = direction*RUN_SPEED;
            if (characterBody2D.IsOnFloor() && Input.IsActionJustPressed(ProjectSettingHelper.InputMapEnum.jump.ToString()))
            {
                velocity.Y = JUMP_SPEED;
                PlayAnimation(AnimationEnum.Jump);

            }

            if (characterBody2D.IsOnFloor())
            {
                if (Mathf.IsZeroApprox(direction))
                {
                    PlayAnimation(AnimationEnum.Idel);
                }
                else
                {
                    PlayAnimation(AnimationEnum.Running);
                }
            }
            else
            {
                PlayAnimation(AnimationEnum.Jump);
            }

            if (!Mathf.IsZeroApprox(direction))
            {
                sprite2D.FlipH = direction < 0;
            }



            characterBody2D.Velocity = velocity;
            characterBody2D.MoveAndSlide();

        }
        private void PlayAnimation(AnimationEnum animationEnum)
        {
            animationPlayer.Play(animationEnum.ToString());
        }

        public override void Ready()
        {
            characterBody2D = Scene.GetNode<CharacterBody2D>("CharacterBody2D");
            camera2D = characterBody2D.GetNode<Camera2D>("Camera2D");
            sprite2D = characterBody2D.GetNode<Sprite2D>("Sprite2D");
            animationPlayer = characterBody2D.GetNode<AnimationPlayer>("AnimationPlayer");
            printHelper.Debug("加载完成");
            PlayAnimation(AnimationEnum.Idel);
        }

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
