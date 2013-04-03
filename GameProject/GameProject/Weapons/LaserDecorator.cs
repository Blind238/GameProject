using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class LaserDecorator : WeaponUpgrade
    {
        private Vector2 _offset;
        private Vector2 _speed;
        private float _scale;

        private static int _pelletNum = 10;

        private static float _totalDamage = 2.0f;
        private static float _pelletDamage = _totalDamage / _pelletNum;

        private static double _lastFired = 0;
        private static int _shootTimer = 500;

        public LaserDecorator(IWeapon wpn) {
            _scale = GameLogic.GetScale();
            _offset = new Vector2(_scale, -(_scale*5));
            _speed = new Vector2(0, -(_scale*2));
            _wpn = wpn;
        }

        public override void Shoot(GameTime gameTime, Vector2 position)
        {
            if (GameHelper.AllowedToFire(_lastFired, _shootTimer, gameTime))
            {
                _lastFired = gameTime.TotalGameTime.TotalMilliseconds;
                for (int i = 0; i < _pelletNum; i++)
                {
                    Vector2 totalOffset = _offset;
                    Vector2 verticalOffset = new Vector2(0, _scale);

                    Projectile projectile = new Projectile(GameLogic.GetGame(), _pelletDamage);
                    projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
                    for (int j = 0; j < i; j++)
                    {
                        totalOffset += verticalOffset;
                    }
                    projectile.SetPosition(position + totalOffset);
                    GameLogic.GetGame().Components.Add(projectile);
                    GameLogic.AddPlayerProjectile(projectile);
                } 
            }
            _wpn.Shoot(gameTime, position);
        }
    }
}
