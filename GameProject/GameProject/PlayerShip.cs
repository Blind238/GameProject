using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public class PlayerShip : MovingObject
    {
        private IWeapon _wpn;

        public PlayerShip(Game game)
            : base(game){
            SetTexture(Resources.playerShip);
            SetMovingBehaviour(new PlayerMovement(this));
            _wpn = new Cannon();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Space) || gamepadState.IsButtonDown(Buttons.A))
            {
                // Shoot, giving our position so 
                // the weapon knows where to spawn projectiles
                _wpn.Shoot(gameTime, GetPosition());
            }
            
            base.Update(gameTime);
        }

        public void SetPowerUp(PowerUp powerUp)
        {
            _wpn = powerUp.GetPowerUp(_wpn);
        }
    }
}
