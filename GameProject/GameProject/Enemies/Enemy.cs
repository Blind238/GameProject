using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class Enemy : MovingObject
    {
        private static double _powerUpChance = 0.20;
        private float _health;
        private bool _destroyed = false;

        public Enemy(Game game)
            : base(game){
        }

        public void Destruct() {
            if(!_destroyed)
            {
                Random random = new Random();
                if(random.NextDouble() <= _powerUpChance)
                {
                    GameLogic gameLogic = GameLogic.GetInstance();
                    Game game = gameLogic.GetGame();
                    PowerUp powerUp = new PowerUp(game, GetPosition());
                    gameLogic.AddPowerUp(powerUp);
                    game.Components.Add(powerUp);
                }
                _destroyed = true; 
            }
        }

        public void SetHealth(float health)
        {
            _health = health;
        }

        public void Hit(float damage)
        {
            _health -= damage;

            if(_health <= 0)
            {
                Destruct();
            }
        }

        public bool IsDestroyed()
        {
            return _destroyed;
        }
    }
}
