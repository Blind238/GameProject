using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Cannon : IWeapon
    {
        private Vector2 _offset;
        private Vector2 _speed;
        private float _scale;

        private static float _pelletDamage = 1.0f;

        private static double _lastFired = 0;
        private static int _shootTimer = 200;

        public Cannon()
        {
            _scale = GameLogic.GetScale();
            _offset = new Vector2(0, -(_scale*5));
            _speed = new Vector2(0, -(_scale*3));
        }

        public void Shoot(GameTime gameTime, Vector2 position) {
            if (GameHelper.AllowedToFire(_lastFired, _shootTimer, gameTime))
            {
                _lastFired = gameTime.TotalGameTime.TotalMilliseconds;
                Projectile projectile = new Projectile(GameLogic.GetGame(), _pelletDamage);
                projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
                projectile.SetPosition(position + _offset);
                GameLogic.GetGame().Components.Add(projectile);
                GameLogic.AddPlayerProjectile(projectile); 
            }
        }
    }
}
