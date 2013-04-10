using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    class Levels
    {
        public static void LoadLevel(int lvl)
        {
            GameLogic gameLogic = GameLogic.GetInstance();
            Game game = gameLogic.GetGame();

            switch (lvl)
            {
                case 1:
                    LevelOne(game, gameLogic);
                    break;
                case 2:
                    LevelTwo(game, gameLogic);
                    break;
                case 3:
                    LevelThree(game, gameLogic);
                    break;
                default:
                    break;
            }
        }

        private static void LevelOne(Game game, GameLogic gameLogic)
        {

        }

        private static void LevelTwo(Game game, GameLogic gameLogic)
        {
            
        }

        private static void LevelThree(Game game, GameLogic gameLogic)
        {
            
        }
    }
}
