using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class PowerUp : MovingObject
    {
        private static Vector2 _fallSpeed = new Vector2(0, 3);

        public PowerUp(Game game, Vector2 position)
            : base(game)
        {
            SetPosition(position);
            SetMovingBehaviour(new StraightLine(this, _fallSpeed));
        }
    }
}
