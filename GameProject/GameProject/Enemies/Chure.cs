using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Chure : Enemy
    {
        public Chure(Game game)
            : base(game)
        {
            SetTexture(Resources.chure);
            SetMovingBehaviour(new RowByRow(this));
            SetHealth(3.0f);
        }
    }
}
