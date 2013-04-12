using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Chure : Enemy
    {
        private static Random _random = new Random();
        private static double _shootChance = 0.01;
        private static Vector2 _shootVector = new Vector2(0.0f, 2.0f);
        private static Vector2 _offset;

        public Chure(Game game)
            : base(game)
        {
            SetTexture(Resources.chure);
            SetMovingBehaviour(new RowByRow(this));
            SetHealth(3.0f);
            if (_offset == Vector2.Zero)
            {
                _offset = new Vector2(0.0f, (float)Bounds().Height / 2f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_random.NextDouble() <= _shootChance)
            {
                GameLogic gameLogic = GameLogic.GetInstance();
                Game game = gameLogic.GetGame();

                Projectile projectile = new Projectile(game, 1.0f);
                projectile.SetMovingBehaviour(new StraightLine(projectile, _shootVector));

                // Tweak the vertical position with one scaled pixel,
                // It looks better
                projectile.SetPosition(GetPosition() + _offset - new Vector2(0.0f,gameLogic.GetScale()));

                projectile.SetScale(gameLogic.GetScale() * 2.0f);
                game.Components.Add(projectile);
                gameLogic.AddEnemyProjectile(projectile);
            }
            base.Update(gameTime);
        }
    }
}
