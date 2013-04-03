using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class GuidedMissileDecorator : WeaponUpgrade
    {
        private Vector2 _offset = new Vector2(0, -25);
        private Vector2 _speed = new Vector2(0, -10);

        private static int _pelletNum = 5;

        private static float _totalDamage = 2.0f;
        private static float _pelletDamage = _totalDamage / _pelletNum;

        private static int _lastFired;
        private static int _shootTimer = 2000;

        public GuidedMissileDecorator(IWeapon wpn) {
            _wpn = wpn;
        }

        public override void Shoot(GameTime gameTime, Vector2 position)
        {
            throw new NotImplementedException();
            _wpn.Shoot(gameTime, position);
        }
    }
}
