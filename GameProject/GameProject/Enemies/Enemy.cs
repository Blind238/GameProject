using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class Enemy : MovingObject
    {
        private static double _powerUpChance = 0.25;

        public Enemy(Game game)
            : base(game){
        }

        public void Destruct() {
            Random random = new Random();
            if(random.NextDouble() <= _powerUpChance)
            {
                Game game = GameLogic.GetGame();
                PowerUp powerUp = new PowerUp(game, GetPosition());
                GameLogic.AddPowerUp(powerUp);
                GameLogic.GetGame().Components.Add(powerUp);
            }
        }
    }
}
