using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class StraightLine : MovingBehaviour
    {
        public StraightLine(MovingObject movingObject, Vector2 speed)
            :base(movingObject) {
                SetSpeed(speed);
        }
    
        public override void Move() {
            Vector2 currentPosition = GetMovingObject().GetPosition();
            GetMovingObject().SetPosition(new Vector2(currentPosition.X + GetSpeed().X, currentPosition.Y + GetSpeed().Y));
        }
    }
}
