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
        private float _bulletScale;

        private static float _pelletDamage = 1.0f;

        private static double _lastFired = 0;
        private static int _shootTimer = 200;

        public Cannon()
        {
            float scale = GameLogic.GetInstance().GetScale();
            _bulletScale = scale * 1.5f;
            _offset = new Vector2(0, -(scale*9f));
            _speed = new Vector2(0, -(scale*3f));
        }

        public void Shoot(GameTime gameTime, Vector2 position) {

            // Check if we're allowed to fire, do so
            if (GameHelper.AllowedToFire(_lastFired, _shootTimer, gameTime))
            {
                _lastFired = gameTime.TotalGameTime.TotalMilliseconds;
                Game game = GameLogic.GetInstance().GetGame();
                Projectile projectile = new Projectile(game, _pelletDamage);
                projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
                projectile.SetPosition(position + _offset);
                projectile.SetScale(_bulletScale);
                game.Components.Add(projectile);
                GameLogic.GetInstance().AddPlayerProjectile(projectile);
            }
        }
    }
}
