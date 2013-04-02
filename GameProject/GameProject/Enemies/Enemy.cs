using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public abstract class Enemy : MovingObject
    {
        public Enemy(Game game)
            : base(game){
        }
    }
}
