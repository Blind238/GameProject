using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace GameProject
{
    class Levels
    {
        public enum ELevel
        {
            One = 1,
            Two,
            Three
        }
        private static ELevel currentLevel;

        public static void LoadNext()
        {
            currentLevel++;
            LoadLevel((int)currentLevel);
        }
        public static void LoadLevel(int lvl)
        {
            currentLevel = (ELevel)lvl;
            GameLogic gameLogic = GameLogic.GetInstance();
            Game game = gameLogic.GetGame();
            ArrayList enemies = gameLogic.GetEnemies();

            switch (lvl)
            {
                case 1:
                    LevelOne(game, gameLogic, enemies);
                    break;
                case 2:
                    LevelTwo(game, gameLogic, enemies);
                    break;
                case 3:
                    LevelThree(game, gameLogic, enemies);
                    break;
                default:
                    break;
            }
        }

        private static void LevelOne(Game game, GameLogic gameLogic, ArrayList enemies)
        {
            for (int i = 0; i < 3; i++)
            {
                Chure chure = new Chure(game);
                chure.SetPosition(new Vector2((game.GraphicsDevice.Viewport.Width/3) * i + chure.Bounds().Width, -20));
                game.Components.Add(chure);
                enemies.Add(chure);
            }
            
            Thege thege = new Thege(game);
            game.Components.Add(thege);
            enemies.Add(thege);

            Sinode sinode = new Sinode(game);
            game.Components.Add(sinode);
            enemies.Add(sinode);
        }

        private static void LevelTwo(Game game, GameLogic gameLogic, ArrayList enemies)
        {
            for (int i = 0; i < 4; i++)
            {
                Chure chure = new Chure(game);
                chure.SetPosition(new Vector2((game.GraphicsDevice.Viewport.Width / 4) * i + chure.Bounds().Width, -20));
                game.Components.Add(chure);
                enemies.Add(chure);
            }

            for (int i = 0; i < 2; i++)
            {
                Thege thege = new Thege(game);
                game.Components.Add(thege);
                enemies.Add(thege); 
            }

            Sinode sinode = new Sinode(game);
            game.Components.Add(sinode);
            enemies.Add(sinode);
        }

        private static void LevelThree(Game game, GameLogic gameLogic, ArrayList enemies)
        {
            for (int i = 0; i < 5; i++)
            {
                Chure chure = new Chure(game);
                chure.SetPosition(new Vector2((game.GraphicsDevice.Viewport.Width / 5) * i + chure.Bounds().Width, -20));
                game.Components.Add(chure);
                enemies.Add(chure);
            }

            for (int i = 0; i < 2; i++)
            {
                Thege thege = new Thege(game);
                game.Components.Add(thege);
                enemies.Add(thege);
            }

            for (int i = 0; i < 2; i++)
            {
                Sinode sinode = new Sinode(game);
                game.Components.Add(sinode);
                enemies.Add(sinode); 
            }
        }
    }
}
