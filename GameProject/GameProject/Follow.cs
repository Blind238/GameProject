using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Follow : MovingBehaviour
    {
        public Follow(MovingObject movingObject) 
            : base(movingObject) {
        }

        public override void Move() {
            throw new NotImplementedException();
        }
    }
}
