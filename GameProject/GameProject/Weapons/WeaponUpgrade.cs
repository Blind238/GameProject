using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class WeaponUpgrade : IWeapon
    {
        public IWeapon _wpn { get; set; }
        public abstract void Shoot(GameTime gametime, Vector2 position);
    }
}
