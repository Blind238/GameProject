using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class RocketDecorator : WeaponUpgrade
    {
        public RocketDecorator(IWeapon wpn) {
            _wpn = wpn;
        }

        public void shoot() {
            throw new NotImplementedException();
        }
    }
}
