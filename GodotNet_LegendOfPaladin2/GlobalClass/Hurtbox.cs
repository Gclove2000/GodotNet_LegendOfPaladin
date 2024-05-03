using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin2.GlobalClass
{
    [GlobalClass]
    public partial class Hurtbox : Area2D
    {
        /// <summary>
        /// 注册伤害和死亡的委托事件
        /// </summary>
        public event Action<Hitbox> HurtCallback;

        public event Action DieCallback;

        [Export]
        public int Health = 100;

        public Hurtbox()
        {

        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="num"></param>
        /// <param name="owner"></param>
        public void Hurt(Hitbox hitbox)
        {
            Health -= hitbox.Damage;
            GD.Print($"{hitbox.Owner.Name} [Hit] {Owner.Name} in {hitbox.Damage} damage, Health = {Health}");
            HurtCallback?.Invoke(hitbox);
            if (Health <= 0)
            {
                DieCallback?.Invoke();
            }
        }


    }
}
