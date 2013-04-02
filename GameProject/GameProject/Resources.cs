﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public static class Resources
    {
        public static Texture2D playerShip { get; set; }
        public static Texture2D thege { get; set; }
        public static Texture2D chure { get; set; }
        public static Texture2D sinode { get; set; }

        public static void LoadResources(Game game) {
            playerShip = game.Content.Load<Texture2D>("playerShip");
            thege = game.Content.Load<Texture2D>("thege");
            chure = game.Content.Load<Texture2D>("chure");
            sinode = game.Content.Load<Texture2D>("sinode");
        }
    }
}