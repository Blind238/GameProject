using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Sinode : Enemy
    {
        private static Random _random = new Random();

        public Sinode(Game game)
            : base(game)
        {
            SetTexture(Resources.sinode);
            SetHealth(2.5f);

            // Spawn at one of the top corners
            Vector2 startingPosition;
            if (_random.Next(2) == 0)
            {
                startingPosition = new Vector2((float)game.GraphicsDevice.Viewport.Width * 0.2f, Bounds().Height);
            }
            else
            {
                startingPosition = new Vector2((float)game.GraphicsDevice.Viewport.Width * 0.8f, Bounds().Height);
            }
            SetPosition(startingPosition);

            // Just using the first playerShip for aiming
            MovingObject playerShip = (MovingObject)GameLogic.GetInstance().GetPlayerShips()[0];
            Vector2 speed = GameHelper.PointToTarget(this, playerShip) * new Vector2(3.0f, 3.0f);
            SetMovingBehaviour(new Wave(this, speed));
        }

        public override void Update(GameTime gameTime)
        {
            Wave wave = (Wave)GetMovingBehaviour();
            wave.Move(gameTime);
            base.Update(gameTime);
        }
    }
}
