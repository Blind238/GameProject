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
        public static Texture2D projectile { get; set; }
        public static Texture2D guidedmissile { get; set; }
        public static Texture2D rocket { get; set; }
        public static Texture2D laser { get; set; }
        public static Texture2D frame { get; set; }

        public static void LoadResources(Game game) {
            playerShip = game.Content.Load<Texture2D>("playerShip");
            thege = game.Content.Load<Texture2D>("Enemies/thege");
            chure = game.Content.Load<Texture2D>("Enemies/chure");
            sinode = game.Content.Load<Texture2D>("Enemies/sinode");
            projectile = game.Content.Load<Texture2D>("projectile");
            guidedmissile = game.Content.Load<Texture2D>("PowerUps/guidedmissile");
            laser = game.Content.Load<Texture2D>("PowerUps/laser");
            rocket = game.Content.Load<Texture2D>("PowerUps/rocket");
            frame = game.Content.Load<Texture2D>("Hud/frame");
        }
    }
}
