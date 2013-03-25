using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Projectile : MovingObject
    {
        public Projectile() {
            SetMovingBehaviour(new StraightLine(this));
        }
    }
}
