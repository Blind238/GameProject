using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class RowByRow : MovingBehaviour
    {
        private static Vector2 _startPosition = new Vector2(50, 0);
        private static int _fallingAmount = 60;
        private int _reachedEdge = 0;
        private Vector2 _speed = new Vector2(-2, 2);

        public RowByRow(MovingObject movingObject)
            : base(movingObject) {
                movingObject.SetPosition(_startPosition);
        }
    
        public override void Move() {
            MovingObject movingObject = GetMovingObject();
            Vector2 position = movingObject.GetPosition();
            Viewport viewport = GameLogic.GetGame().GraphicsDevice.Viewport;

            if (position.Y < (_reachedEdge + _fallingAmount))
            {
                movingObject.SetPosition(position + new Vector2 (0,_speed.Y));
            }
            else
            {
                movingObject.SetPosition(position + new Vector2 (_speed.X,0));
            }

            position = movingObject.GetPosition() + movingObject.GetOrigin();

            if (position.X > viewport.Width - (movingObject.Bounds().Width/2))
            {
                movingObject.SetPosition(new Vector2((viewport.Width - (movingObject.Bounds().Width / 2)) - movingObject.GetOrigin().X, position.Y));
                _reachedEdge = (int)position.Y;
                _speed = _speed * new Vector2(-1, 1);
            }
            if (position.X < movingObject.Bounds().Width / 2)
            {
                movingObject.SetPosition(new Vector2((movingObject.Bounds().Width / 2) - movingObject.GetOrigin().X, position.Y));
                _reachedEdge = (int)position.Y;
                _speed = _speed * new Vector2(-1, 1);
            }

            
        }
    }
}
