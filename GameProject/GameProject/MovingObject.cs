using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class MovingObject : DrawableGameComponent
    {
        private Vector2 _position;
        private MovingBehaviour _movingBehaviour;

        public MovingObject(Game game)
            : base(game) {

        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            Move();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }

        private void Move() {
            _movingBehaviour.Move();
        }

        public void SetMovingBehaviour(MovingBehaviour movingBehaviour) {
            _movingBehaviour = movingBehaviour;
        }
    }
}
