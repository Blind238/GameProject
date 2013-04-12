using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    class GameLogic
    {
        private static GameLogic _gameLogic;
        private int _currentLevel;
        private Game _game;

        private static float _scale = 3.0f;

        private ArrayList _enemies;
        private ArrayList _enemyProjectiles;
        private ArrayList _players;
        private ArrayList _playerProjectiles;
        private ArrayList _powerUps;
        private int _playerLives;
        
        private void Start(Game game)
        {
            _game = game;
            _enemies = new ArrayList();
            _enemyProjectiles = new ArrayList();
            _players = new ArrayList();
            _playerProjectiles = new ArrayList();
            _powerUps = new ArrayList();
            PlayerShip _playerShip = new PlayerShip(_game);
            _game.Components.Add(_playerShip);
            _players.Add(_playerShip);

            Chure chure = new Chure(_game);
            _game.Components.Add(chure);
            _enemies.Add(chure);

            Thege thege = new Thege(_game);
            _game.Components.Add(thege);
            _enemies.Add(thege);
        }

        public void Update(GameTime gametime)
        {
            // Go though ArrayLists and check for collisions
            // If true, take appropriate action

            foreach (PlayerShip playerShip in _players)
            {
                Stack stack = new Stack();
                if (GameHelper.CollisionHappened(playerShip, _enemyProjectiles, stack))
                {
                    while (stack.Count > 0)
                    {
                        _enemyProjectiles.Remove((MovingObject)stack.Peek());
                        _game.Components.Remove((MovingObject)stack.Pop());
                        PlayerHit();
                    }
                }

                if (GameHelper.CollisionHappened(playerShip, _powerUps, stack))
                {
                    while (stack.Count > 0)
                    {
                        playerShip.SetPowerUp((PowerUp)stack.Peek());
                        _powerUps.Remove((MovingObject)stack.Peek());
                        _game.Components.Remove((MovingObject)stack.Pop());
                    }
                }
            }

            Stack killedStack = new Stack();
            Stack collideStack = new Stack();

            foreach (Enemy enemy in _enemies)
            {
                // Check enemy collisions with playerProjectiles
                Stack stack = new Stack();
                if (GameHelper.CollisionHappened(enemy, _playerProjectiles, stack))
                {
                    // Apply damage to enemy for each projectile
                    while (stack.Count > 0)
                    {
                        enemy.Hit(((Projectile)stack.Peek()).GetDamage());

                        _playerProjectiles.Remove((MovingObject)stack.Peek());
                        _game.Components.Remove((MovingObject)stack.Pop());
                    }

                    // Check if enemy should be dead, kill it
                    if(enemy.IsDestroyed())
                    {
                        killedStack.Push(enemy);
                    }
                }

                // Check enemy collisions with players
                if (GameHelper.CollisionHappened(enemy, _players, stack))
                {
                     collideStack.Push(enemy);
                }
            }

            while(killedStack.Count > 0)
            {
                _enemies.Remove((MovingObject)killedStack.Peek());
                _game.Components.Remove((MovingObject)killedStack.Pop());
            }

            while (collideStack.Count > 0)
            {
                _enemies.Remove((MovingObject)collideStack.Peek());
                _game.Components.Remove((MovingObject)collideStack.Pop());
                PlayerHit();
            }

            CleanUp();
        }

        private void PlayerHit()
        {
            _playerLives--;
        }

        public Game GetGame()
        {
            return _game;
        }

        public float GetScale()
        {
            return _scale;
        }

        public ArrayList GetEnemies()
        {
            return _enemies;
        }

        public ArrayList GetPlayerShips()
        {
            return _players;
        }

        public void AddPlayerProjectile(Projectile projectile)
        {
            _playerProjectiles.Add(projectile);
        }

        public void AddEnemyProjectile(Projectile projectile)
        {
            _enemyProjectiles.Add(projectile);
        }

        public void AddPowerUp(PowerUp powerUp)
        {
            _powerUps.Add(powerUp);
        }

        private void CleanUp()
        {
            Stack stack = new Stack();
            Rectangle viewport = _game.GraphicsDevice.Viewport.Bounds;
            foreach (Projectile projectile in _playerProjectiles)
            {
                Rectangle projectileBounds = projectile.Bounds();
                if (!viewport.Contains(projectileBounds) && !viewport.Intersects(projectileBounds))
                {
                    stack.Push(projectile);
                }
            }
            while (stack.Count > 0)
            {
                _playerProjectiles.Remove((MovingObject)stack.Peek());
                _game.Components.Remove((MovingObject)stack.Pop());
            }

            foreach (Projectile projectile in _enemyProjectiles)
            {
                Rectangle projectileBounds = projectile.Bounds();
                if (!viewport.Contains(projectileBounds) && !viewport.Intersects(projectileBounds))
                {
                    stack.Push(projectile);
                }
            }
            while (stack.Count > 0)
            {
                _enemyProjectiles.Remove((MovingObject)stack.Peek());
                _game.Components.Remove((MovingObject)stack.Pop());
            }

            foreach (PowerUp powerUp in _powerUps)
            {
                Rectangle projectileBounds = powerUp.Bounds();
                if (!viewport.Contains(projectileBounds) && !viewport.Intersects(projectileBounds))
                {
                    stack.Push(powerUp);
                }
            }
            while (stack.Count > 0)
            {
                _powerUps.Remove((MovingObject)stack.Peek());
                _game.Components.Remove((MovingObject)stack.Pop());
            }

            foreach (Enemy enemy in _enemies)
            {
                Rectangle enemyBounds = enemy.Bounds();
                if (!viewport.Contains(enemyBounds) && !viewport.Intersects(enemyBounds))
                {
                    stack.Push(enemy);
                }
            }
            while (stack.Count > 0)
            {
                if (stack.Peek().GetType() == typeof(Thege))
                {
                    ResetThege((Thege)stack.Pop());
                }
                else
                {
                    _enemies.Remove((MovingObject)stack.Peek());
                    _game.Components.Remove((MovingObject)stack.Pop());
                }
            }
        }

        public static GameLogic GetInstance()
        {
            if (_gameLogic == null)
            {
                _gameLogic = new GameLogic();
                _gameLogic.Start(Game1.GetGame());
            }
            return _gameLogic;
        }

        private void ResetThege(Thege thege)
        {
            _enemies.Remove(thege);
            _game.Components.Remove(thege);

            Thege newThege = new Thege(_game);
            _game.Components.Add(newThege);
            _enemies.Add(newThege);
        }
    }
}
