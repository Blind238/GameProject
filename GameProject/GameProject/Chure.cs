using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Chure : Enemy
    {
        public Chure() {
            SetMovingBehaviour(new RowByRow(this));
        }
    }
}
