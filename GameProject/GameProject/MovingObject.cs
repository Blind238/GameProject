using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public abstract class MovingObject : DrawableGameComponent
    {
        private Vector2 _position;
        private Texture2D _texture;
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

        public void SetTexture(Texture2D texture) {
            _texture = texture;
        }

        public void SetPosition(Vector2 position) {
            _position = position;
        }

        public Vector2 GetPosition() {
            return _position;
        }
    }
}
