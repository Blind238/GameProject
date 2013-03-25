using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Thege : Enemy
    {
        public Thege() {
            SetMovingBehaviour(new StraightLine(this));
        }
    }
}
