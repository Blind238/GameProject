using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject.Movement
{
    public class DontMove : MovingBehaviour
    {
        public DontMove(MovingObject movingObject)
            : base(movingObject)
        {
        }

        public override void Move()
        {
        }
    }
}
