using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public interface IWeapon
    {
        void Shoot(Vector2 position);
    }
}
