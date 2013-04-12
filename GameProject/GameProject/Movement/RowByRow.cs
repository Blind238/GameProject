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
        private static int _descendAmount = 60;
        private int _reachedEdge = 0;
        private Vector2 _speed = new Vector2(-2.0f, 2.0f);

        public RowByRow(MovingObject movingObject)
            : base(movingObject) {
        }
    
        public override void Move() {
            MovingObject movingObject = GetMovingObject();
            Vector2 position = movingObject.GetPosition();
            Viewport viewport = GameLogic.GetInstance().GetGame().GraphicsDevice.Viewport;

            // If we haven't descended enough, keep descending.
            // Else we move horizontally again.
            if (position.Y < (_reachedEdge + _descendAmount))
            {
                movingObject.SetPosition(position + new Vector2 (0.0f, _speed.Y));
            }
            else
            {
                movingObject.SetPosition(position + new Vector2 (_speed.X, 0.0f));
            }

            // We need to get position again because it was updated.
            position = movingObject.GetPosition();

            // Whenever we reach the right or left edge,
            // set reachedEdge and flip horizontal direction
            if (position.X > viewport.Width - (movingObject.Bounds().Width/2))
            {
                movingObject.SetPosition(new Vector2((viewport.Width - (movingObject.Bounds().Width / 2)), position.Y));
                _reachedEdge = (int)position.Y;
                _speed = _speed * new Vector2(-1, 1);
            }
            if (position.X < movingObject.Bounds().Width / 2)
            {
                movingObject.SetPosition(new Vector2((movingObject.Bounds().Width / 2), position.Y));
                _reachedEdge = (int)position.Y;
                _speed = _speed * new Vector2(-1, 1);
            }

            
        }
    }
}
