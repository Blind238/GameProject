using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class GuidedMissileDecorator : WeaponUpgrade
    {
        public GuidedMissileDecorator(IWeapon wpn) {
            _wpn = wpn;
        }

        public void shoot() {
            throw new NotImplementedException();
        }
    }
}
