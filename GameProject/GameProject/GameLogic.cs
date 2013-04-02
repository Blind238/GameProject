using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    static class GameLogic
    {
        private static int _gameState;
        private static Game _game;

        private static float _scale = 5.0f;

        private static ArrayList _enemies;
        private static ArrayList _enemyProjectiles;
        private static ArrayList _players;
        private static ArrayList _playerProjectiles;
        private static int _playerLives;
        
        public static void Start(Game game)
        {
            _game = game;
            _gameState = 1;
            _enemies = new ArrayList();
            _enemyProjectiles = new ArrayList();
            _players = new ArrayList();
            _playerProjectiles = new ArrayList();
            PlayerShip _playerShip = new PlayerShip(_game);
            _game.Components.Add(_playerShip);
            _players.Add(_playerShip);

            Chure chure = new Chure(_game);
            _game.Components.Add(chure);
            _enemies.Add(chure);
        }

        public static void Update(GameTime gametime)
        {
            foreach (PlayerShip playerShip in _players)
            {
                Stack stack = new Stack();
                if (GameHelper.CollisionHappened(playerShip, _enemyProjectiles, stack))
                {
                    while (stack.Count > 0)
                    {
                        ((MovingObject)stack.Pop()).Dispose();
                        GameLogic.PlayerHit();
                    }
                }
            }
        }

        private static void PlayerHit()
        {
            _playerLives--;
        }

        public static Game GetGame()
        {
            return _game;
        }

        public static float GetScale()
        {
            return _scale;
        }

        public static void AddPlayerProjectile(Projectile projectile)
        {
            _playerProjectiles.Add(projectile);
        }
    }
}
