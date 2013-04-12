using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Movement;

namespace GameProject
{
    class Hud : MovingObject
    {
        private List<PlayerShip> ships = new List<PlayerShip>();

        public Hud(Game game, int maxLives)
            : base(game)
        {
            SetScale(GameLogic.GetInstance().GetScale() * 3f);
            SetTexture(Resources.frame);
            SetMovingBehaviour(new DontMove((MovingObject)this));
            
            SetPosition(new Vector2(game.GraphicsDevice.Viewport.Width * 0.6f, 0f));
            
            for (int i = 0; i < maxLives; i++)
            {
                PlayerShip ship = new PlayerShip(game);
                ship.SetMovingBehaviour(new DontMove(ship));
                ship.SetScale(GameLogic.GetInstance().GetScale()/2);
                ship.SetPosition(new Vector2((GetPosition().X - Bounds().Width) + i * (ship.Bounds().Width * 1f) , ship.Bounds().Height * 0.3f));
                ship.Disable();
                game.Components.Add(ship);
                ships.Add(ship);
            }
        }

        public void UpdateHud(int currentLives)
        {
            PlayerShip ship = ships[ships.Count - 1];
            ships.Remove(ship);
            GameLogic.GetInstance().GetGame().Components.Remove(ship);
        }
    }
}
