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
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            if (gamepadState.IsButtonDown(Buttons.A))
            {
                _wpn.Shoot(GetPosition());
            }
            
            base.Update(gameTime);
        }
    }
}
