using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Wave : MovingBehaviour
    {
        private static double _amplitude = 10.0;
        private Vector2 _direction;
        private Vector2 _modDirection;
        private float _speed;
        private Vector2 _basePosition;
        private double _startTime;
        private double _elapsedTime;
        private bool _resetTime = false;

        /// <summary>
        /// Sets forth a sine movement of 'movingObject'
        /// along vector 'direction'
        /// </summary>
        /// <param name="movingObject"></param>
        /// <param name="direction"></param>
        public Wave(MovingObject movingObject, Vector2 direction)
            : base(movingObject) 
        {
            _direction = direction;
            _modDirection = new Vector2(-_direction.X, _direction.Y);
            _speed = _direction.Length();
            _basePosition = GetMovingObject().GetPosition();
        }

        public override void Move()
        {
            // Do nothing.
        }

        public void Move(GameTime gameTime) {
            MovingObject movingObject = GetMovingObject();
            Vector2 position = movingObject.GetPosition();
            Viewport viewport = GameLogic.GetInstance().GetGame().GraphicsDevice.Viewport;

            if (_startTime == 0.0 || _resetTime == true)
            {
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
                _resetTime = false;
            }
            
            _elapsedTime = gameTime.TotalGameTime.TotalMilliseconds - _startTime;

            Vector2 v = _modDirection;
            v.Normalize();
            v = v *
                (float)Math.Sin(
                    _elapsedTime * Math.PI * 0.002
                    ) *
                (float)_amplitude;
            v *= _speed;
            v = v + _basePosition;
            _basePosition += _direction;
            movingObject.SetPosition(v);

            position = movingObject.GetPosition();
            if (position.X > viewport.Width - (movingObject.Bounds().Width / 2))
            {
                movingObject.SetPosition(new Vector2((viewport.Width - (movingObject.Bounds().Width / 2)), position.Y));
                _direction = _direction * new Vector2(-1, 1);
                _modDirection = new Vector2(_direction.X, -_direction.Y);
                _basePosition = movingObject.GetPosition();
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
                //_resetTime = true;
            }
            if (position.X < movingObject.Bounds().Width / 2)
            {
                movingObject.SetPosition(new Vector2((movingObject.Bounds().Width / 2), position.Y));
                _direction = _direction * new Vector2(-1, 1);
                _modDirection = new Vector2(_direction.X, -_direction.Y);
                _basePosition = movingObject.GetPosition();
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
                //_resetTime = true;
            }
            if (position.Y > viewport.Height - (movingObject.Bounds().Height / 2))
            {
                movingObject.SetPosition(new Vector2(position.X, (viewport.Height - (movingObject.Bounds().Height / 2))));
                _direction = _direction * new Vector2(1, -1);
                _modDirection = new Vector2(-_direction.X, _direction.Y);
                _basePosition = movingObject.GetPosition();
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;

                //_resetTime = true;
            }
            if (position.Y < movingObject.Bounds().Height / 2)
            {
                movingObject.SetPosition(new Vector2(position.X, (movingObject.Bounds().Height / 2)));
                _direction = _direction * new Vector2(1, -1);
                _modDirection = new Vector2(-_direction.X, _direction.Y);
                _basePosition = movingObject.GetPosition();
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
                //_resetTime = true;
            }
        }
    }
}
