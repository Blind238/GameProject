using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Thege : Enemy
    {
        private static Random _random = new Random();

        public Thege(Game game)
            : base(game)
        {
            SetTexture(Resources.thege);
            SetHealth(2.0f);

            // Spawn at one of the top corners
            Vector2 startingPosition;
            if(_random.Next(2) == 0)
            {
                startingPosition = new Vector2( 0.0f, 0.0f);
            }
            else
            {
                startingPosition = new Vector2( (float)game.GraphicsDevice.Viewport.Width, 0.0f);
            }
            SetPosition(startingPosition);

            // Just using the first playerShip for aiming
            MovingObject playerShip = (MovingObject)GameLogic.GetInstance().GetPlayerShips()[0];
            Vector2 speed = GameHelper.PointToTarget(this, playerShip) * new Vector2(3.0f, 3.0f);
            SetMovingBehaviour(new StraightLine(this, speed));

            // That's it. After spawning, GameLogic and
            // StraightLine take care of the rest
        }
    }
}
