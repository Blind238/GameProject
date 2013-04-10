using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class RocketDecorator : WeaponUpgrade
    {
        private Vector2 _offset;
        private Vector2 _speed;
        private float _scale;
        private static float _rocketScale = 2.0f;
        private enum SideFired { Left, Right }
        private SideFired _sideFiredLast;

        private static int _pelletNum = 2;

        private static float _totalDamage = 3.0f;
        private static float _pelletDamage = _totalDamage / (float)_pelletNum;

        private static double _lastFired = 0;
        private static int _shootTimer = 1000;

        public RocketDecorator(IWeapon wpn) {
            _scale = GameLogic.GetInstance().GetScale();
            _offset = new Vector2(_scale*5f, -(_scale*0.5f));
            _speed = new Vector2(0, -(_scale*2f));
            _wpn = wpn;
        }

        public override void Shoot(GameTime gameTime, Vector2 position)
        {
            if (GameHelper.AllowedToFire(_lastFired, _shootTimer, gameTime))
            {
                _lastFired = gameTime.TotalGameTime.TotalMilliseconds;

                Vector2 offset;
                if (_sideFiredLast == SideFired.Left)
                {
                    offset = new Vector2(_offset.X, _offset.Y);
                    _sideFiredLast = SideFired.Right;
                }
                else
                {
                    offset = new Vector2(-_offset.X, _offset.Y);
                    _sideFiredLast = SideFired.Left;
                }

                for(int i = 0;i < _pelletNum;i++)
                {
                    Vector2 totalOffset = offset;
                    Vector2 verticalOffset = new Vector2(0, _scale * _rocketScale);

                    Projectile projectile = new Projectile(GameLogic.GetInstance().GetGame(), _pelletDamage);
                    projectile.SetMovingBehaviour(new StraightLine(projectile, _speed));
                    for(int k = 0;k < i;k++)
                    {
                        totalOffset += verticalOffset;
                    }
                    projectile.SetPosition(position + totalOffset);
                    projectile.SetScale(_scale * _rocketScale);
                    GameLogic.GetInstance().GetGame().Components.Add(projectile);
                    GameLogic.GetInstance().AddPlayerProjectile(projectile);
                }
            }
            _wpn.Shoot(gameTime, position);
        }
    }
}
