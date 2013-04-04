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
        private Vector2 _origin;
        private float _scale = GameLogic.GetScale();
        private Texture2D _texture;
        private Color _color = Color.White;
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
            Game1.spriteBatch.Draw(_texture, _position, null, _color, 0, _origin, _scale, SpriteEffects.None, 0);
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
            SetOrigin();
        }

        public void SetPosition(Vector2 position) {
            _position = position;
        }

        public void SetOrigin()
        {
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
        }

        public void SetScale(float scale)
        {
            _scale = scale;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }

        public Vector2 GetPosition() {
            return _position;
        }

        public Vector2 GetOrigin()
        {
            return _origin;
        }

        public Rectangle Bounds()
        {
            Matrix movingObjectTransform =
                Matrix.CreateTranslation(new Vector3(-_origin, 0.0f)) *
                Matrix.CreateScale(GameLogic.GetScale()) *
                // Matrix.CreateRotationZ(movingObjectRotation) *
                Matrix.CreateTranslation(new Vector3(_position, 0.0f));
            return GameHelper.CalculateBoundingRectangle(new Rectangle(0,0, _texture.Width, _texture.Height), movingObjectTransform);
            
        }
    }
}
