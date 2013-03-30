using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Projectile : MovingObject
    {
        public Projectile(Game game) 
            : base(game){
            SetMovingBehaviour(new StraightLine(this));
        }
    }
}
