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
        private enum PowerUps {
            Laser,
            GuidedMissile,
            Rocket
        };
        private PowerUps _powerUp;

        public PowerUp(Game game, Vector2 position)
            : base(game)
        {
            SetPosition(position);
            SetMovingBehaviour(new StraightLine(this, _fallSpeed));
            SetPowerUp();
        }

        public IWeapon GetPowerUp(IWeapon wpn)
        {
            WeaponUpgrade wpnUpgrade;
            switch(_powerUp)
            {
                case PowerUps.Laser:
                    wpnUpgrade = new LaserDecorator(wpn);
                    break;
                case PowerUps.GuidedMissile:
                    wpnUpgrade = new GuidedMissileDecorator(wpn);
                    break;
                case PowerUps.Rocket:
                    wpnUpgrade = new RocketDecorator(wpn);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return wpnUpgrade;
        }

        private void SetPowerUp()
        {
            Random random = new Random();
            // We pass the first value of the PowerUps enum as the min,
            // and the last as the max.
            _powerUp =(PowerUps)random.Next((int)PowerUps.Laser,(int)PowerUps.Rocket);

            switch(_powerUp)
            {
                case PowerUps.Laser:
                    SetTexture(Resources.laser);
                    break;
                case PowerUps.GuidedMissile:
                    SetTexture(Resources.guidedmissile);
                    break;
                case PowerUps.Rocket:
                    SetTexture(Resources.rocket);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
