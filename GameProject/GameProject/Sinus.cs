using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Wave : MovingBehaviour
    {
        public Wave(MovingObject movingObject)
            : base(movingObject) {
            
        }
    
        public void Move() {
            throw new NotImplementedException();
        }
    }
}
