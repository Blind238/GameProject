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
        private static float _pelletDamage = _totalDamage / (float)_pelletNum;

        private static double _lastFired = 0;
        private static int _shootTimer = 500;

        public LaserDecorator(IWeapon wpn) {
            _scale = GameLogic.GetInstance().GetScale();
            _offset = new Vector2(_scale*2.5f, -(_scale*5f));
            _speed = new Vector2(0, -(_scale*4f));
            _wpn = wpn;
        }

        public override void Shoot(GameTime gameTime, Vector2 position)
        {
            if (GameHelper.AllowedToFire(_lastFired, _shootTimer, gameTime))
            {
                _lastFired = gameTime.TotalGameTime.TotalMilliseconds;
                for(int i = 0;i < 2;i++)
                {
                    for(int j = 0;j < _pelletNum;j++)
                    {
                        Vector2 totalOffset = _offset;
                        Vector2 verticalOffset = new Vector2(0, _scale);

                        Projectile projectile = new Projectile(GameLogic.GetInstance().GetGame(), _pelletDamage);
                        projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
                        for(int k = 0;k < j;k++)
                        {
                            totalOffset += verticalOffset;
                        }
                        projectile.SetPosition(position + totalOffset);
                        GameLogic.GetInstance().GetGame().Components.Add(projectile);
                        GameLogic.GetInstance().AddPlayerProjectile(projectile);
                    }

                    _offset = new Vector2(-_offset.X, _offset.Y);
                }
            }
            _wpn.Shoot(gameTime, position);
        }
    }
}
