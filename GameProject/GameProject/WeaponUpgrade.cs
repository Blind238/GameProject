using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public abstract class WeaponUpgrade : IWeapon
    {
        public IWeapon _wpn { get; set; }
        abstract void shoot();
    }
}
