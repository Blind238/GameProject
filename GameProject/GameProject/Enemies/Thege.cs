using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Thege : Enemy
    {
        Vector2 placeholder = new Vector2(10, 10);
        public Thege(Game game)
            : base(game){
            SetMovingBehaviour(new StraightLine(this, placeholder));
        }
    }
}
