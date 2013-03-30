using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class LaserDecorator : WeaponUpgrade
    {
        public LaserDecorator(IWeapon wpn) {
            _wpn = wpn;
        }

        public void shoot() {
            throw new NotImplementedException();
            _wpn.shoot();
        }
    }
}
