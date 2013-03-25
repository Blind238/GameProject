using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class MovingBehaviour
    {
        private MovingObject _movingObject;
        private Vector2 _direction;
        private Vector2 _speed;

        public MovingBehaviour(MovingObject movingObject) {
            _movingObject = movingObject;
        }

        public abstract void Move();
    }
}
