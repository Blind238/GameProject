using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace GameProject
{
    public class GuidedMissileDecorator : WeaponUpgrade
    {
        private Vector2 _offset;
        private float _speed;
        private float _scale;
        private static float _missileScale = 1.5f;
        private enum SideFired { Left, Right }
        private SideFired _sideFiredLast;

        private static ArrayList _enemies;

        private static int _pelletNum = 3;

        private static float _totalDamage = 2.0f;
        private static float _pelletDamage = _totalDamage / _pelletNum;

        private static double _lastFired = 0;
        private static int _shootTimer = 2000;

        public GuidedMissileDecorator(IWeapon wpn) {
            _scale = GameLogic.GetInstance().GetScale();
            _offset = new Vector2(_scale*8f, -(_scale*0.5f));
            _speed = _scale*2f;
            _wpn = wpn;

            if(_enemies == null)
            {
                _enemies = GameLogic.GetInstance().GetEnemies();
            }
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
                Vector2 extraOffset = new Vector2();
                Projectile previousProjectile = null;
                for(int i = 0;i < _pelletNum;i++)
                {
                    Vector2 totalOffset = offset;


                    Projectile projectile = new Projectile(GameLogic.GetInstance().GetGame(), _pelletDamage);
                    if(previousProjectile == null)
                    {
                        projectile.SetPosition(position + totalOffset);
                        projectile.SetMovingBehaviour(new Follow(projectile, _enemies, _speed));
                        Vector2 t = projectile.GetMovingBehaviour().GetSpeed();
                        t.Normalize();
                        extraOffset = new Vector2(0.0f, -_scale * _missileScale * 1.01f);
                    } 
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {
                            totalOffset += extraOffset;
                        }
                        projectile.SetPosition(position + totalOffset);
                        projectile.SetMovingBehaviour(new Follow(projectile, previousProjectile, _enemies, _speed));
                    }
                    previousProjectile = projectile;

                    projectile.SetScale(_scale * _missileScale);
                    GameLogic.GetInstance().GetGame().Components.Add(projectile);
                    GameLogic.GetInstance().AddPlayerProjectile(projectile);
                }
            }
            _wpn.Shoot(gameTime, position);
        }
    }
}
