using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Cannon : IWeapon
    {
        private Vector2 _offset = new Vector2(0, -25);
        private Vector2 _speed = new Vector2(0, -10);

        private static int _lastBullet;

        public void Shoot(GameTime gametime, Vector2 position) {
            Projectile projectile = new Projectile(GameLogic.GetGame());
            projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
            projectile.SetTexture(Resources.projectile);
            projectile.SetPosition(position + _offset);
            GameLogic.GetGame().Components.Add(projectile);
            GameLogic.AddPlayerProjectile(projectile);
        }
    }
}
