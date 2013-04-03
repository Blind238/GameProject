using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Projectile : MovingObject
    {
        private float _damage;
        public Projectile(Game game, float damage)
            : base(game)
        {
            _damage = damage;
            SetTexture(Resources.projectile);
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}
