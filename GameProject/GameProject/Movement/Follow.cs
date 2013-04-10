using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace GameProject
{
    public class Follow : MovingBehaviour
    {
        // I was considering making the array static
        // but I should be able to use this for enemy objects
        // as well
        private ArrayList _targets = null;
        private MovingObject _target = null;
        private ArrayList _fallbackTargets = null;
        private float _speed;
        private float _turnSpeed = 0.03f;

        public Follow(MovingObject movingObject, ArrayList targets, float speed) 
            : base(movingObject) 
        {
            // Use this for a list of objects that can be followed
            _targets = targets;
            _speed = speed;

            TargetValidation();
            // We want the initial direction to be straight up
            // TODO: Make this dynamic
            SetSpeed(new Vector2(0.0f, -_speed));
        }

        public Follow(MovingObject movingObject, MovingObject target, ArrayList targets, float speed)
            : base(movingObject)
        {
            // Use this for one object that should be followed,
            // the object should always exist (the player's Ship for example)
            _target = target;
            _speed = speed;

            // If the target ceases to exist, we need a fallback to point to
            _fallbackTargets = targets;

            TargetValidation();
            Vector2 v = GameHelper.PointToTarget(GetMovingObject(), _target);
            v = v * _speed;
            v = v * new Vector2(1.0f, -1.0f);
            SetSpeed(v);
            //SetSpeed(new Vector2(0.0f, -_speed));
        }

        private void SpeedSet()
        {
            if (_target != null)
            {
                float speed = _speed;
                Vector2 t = _target.GetPosition() - GetMovingObject().GetPosition();
                if(t.Length() < GameLogic.GetScale() * 1.5f){
                    speed *= 0.2f;
                }
                Vector2 v = GameHelper.PointToTarget(GetMovingObject(), _target);
                Vector2 w = GetSpeed();
                float angle = TurnToFace(w, v, (float)Math.Atan2(w.Y, w.X), _turnSpeed);
                
                Vector2 direction = Vector2.Zero;
                direction.X = (float)Math.Cos(angle) * speed;
                direction.Y = (float)Math.Sin(angle) * speed;
   
                SetSpeed(direction);
            }
        }

        public override void Move() {
            TargetValidation();

            SpeedSet();

            GetMovingObject().SetPosition(GetMovingObject().GetPosition() + GetSpeed());
        }

        private void TargetValidation()
        {
            if (_targets != null)
            {
                // If target wasn't set or no longer exists
                if (_target == null || !_targets.Contains(_target))
                {
                    // Choose a target at random
                    Random random = new Random();
                    if (_targets.Count != 0)
                    {
                        int t = random.Next(0, _targets.Count);
                        _target = (MovingObject)_targets[t];
                    }
                    else
                    {
                        // We've run out of targets, null everything
                        _target = null;
                        _targets = null;
                        _fallbackTargets = null;
                    }
                }
            }
            else
            {
                if (_fallbackTargets != null)
                {
                    _targets = _fallbackTargets;
                }
            }
        }

        // http://xbox.create.msdn.com/en-US/education/catalog/sample/aiming
        private static float TurnToFace(Vector2 position, Vector2 faceThis,
            float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         C 
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // S--------
            //     x
            // 
            // where S is the position of the spot light, C is the position of the cat,
            // and "o" is the angle that the spot light should be facing in order to 
            // point at the cat. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}
