using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public class PlayerMovement : MovingBehaviour
    {
        private Game _game;

        public PlayerMovement(MovingObject movingObject)
            : base(movingObject)
        {
            _game = GameLogic.GetInstance().GetGame();
            GetMovingObject().SetPosition(new Vector2( 40,_game.GraphicsDevice.Viewport.Height * 0.9f));
            SetSpeed(new Vector2(4, 0));
        }

        public override void Move()
        {
            MovingObject movingObject = GetMovingObject();
            Vector2 position = movingObject.GetPosition();
            Vector2 origin = movingObject.GetOrigin();
            Vector2 speed = GetSpeed();

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            if (keyboardState.IsKeyDown(Keys.Right)|| gamepadState.IsButtonDown(Buttons.LeftThumbstickRight)){
                position += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || gamepadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                position -= speed;
            }
                        
            movingObject.SetPosition(new Vector2(MathHelper.Clamp(position.X,
               movingObject.Bounds().Width/2,
                (_game.GraphicsDevice.Viewport.Width - movingObject.Bounds().Width/2)), position.Y));

        }
    }
}
