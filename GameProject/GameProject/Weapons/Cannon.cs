using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Cannon : IWeapon
    {
        Vector2 _offset = new Vector2(0, -50f);
        Vector2 _speed = new Vector2(0, -10);

        public void Shoot(Vector2 position) {
            Projectile projectile = new Projectile(GameLogic.GetGame());
            projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
            projectile.SetTexture(Resources.thege);
            projectile.SetPosition(position + _offset);
            GameLogic.GetGame().Components.Add(projectile);
            GameLogic.AddPlayerProjectile(projectile);
        }
    }
}
