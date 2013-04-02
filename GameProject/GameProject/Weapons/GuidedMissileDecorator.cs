using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class GuidedMissileDecorator : WeaponUpgrade
    {
        public GuidedMissileDecorator(IWeapon wpn) {
            _wpn = wpn;
        }

        public override void Shoot(GameTime gametime, Vector2 position)
        {
            throw new NotImplementedException();
            _wpn.Shoot(gametime, position);
        }
    }
}
