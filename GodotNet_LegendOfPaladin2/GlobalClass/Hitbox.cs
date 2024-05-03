using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.GlobalClass
{
    [GlobalClass]
    public partial class Hitbox:Area2D
    {

        [Export]
        public int Damage = 1;

        /// <summary>
        /// 在实例化事件中添加委托
        /// </summary>
        public Hitbox() {
            AreaEntered += Hitbox_AreaEntered;
        }


        /// <summary>
        /// 当有Area2D进入时
        /// </summary>
        /// <param name="area"></param>
        private void Hitbox_AreaEntered(Area2D area)
        {

            //当进入的节点是继承Area2D的HurtBox的时候
            if (area is Hurtbox)
            {

                OnAreaEnterd((Hurtbox)area);
            
            }
        }

        /// <summary>
        /// 攻击判断
        /// </summary>
        /// <param name="area"></param>
        public void OnAreaEnterd(Hurtbox area)
        {
            //GD.Print($" {Owner.Name} [Hit] {area.Owner.Name}");
            area.Hurt(this);
        }
    }


}
