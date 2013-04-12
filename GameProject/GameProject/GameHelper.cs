using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    // GameHelper is used to store more complicated
    // functions

    static class GameHelper
    {
        private static Color[] _subjectTextureData;
        private static Color[] _movingObjectTextureData;  
        
        /// <summary>
        /// Checks for collisions, stores them in the stack
        /// and returns a boolean
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="movingObjectArray"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static bool CollisionHappened(MovingObject subject, ArrayList movingObjectArray, Stack stack)
        {
            Vector2 subjectOrigin = subject.GetOrigin();
            Vector2 subjectPosition = subject.GetPosition();
            Texture2D subjectTexture = subject.GetTexture();
            float _scale = GameLogic.GetInstance().GetScale();

            _subjectTextureData =
                new Color[subjectTexture.Width * subjectTexture.Height];
            subjectTexture.GetData(_subjectTextureData);

            // Update the subject's transform
            Matrix subjectTransform =
                Matrix.CreateTranslation(new Vector3(-subjectOrigin, 0.0f)) *
                Matrix.CreateScale(_scale) *
                Matrix.CreateTranslation(new Vector3(subjectPosition, 0.0f));

            // Get the bounding rectangle of the subject
            Rectangle subjectRectangle = CalculateBoundingRectangle(
                new Rectangle(0, 0,
                subjectTexture.Width, subjectTexture.Height), subjectTransform);

            bool collision = false;
            for (int i = movingObjectArray.Count - 1; i >= 0; i--)
            {
                MovingObject movingObject = (MovingObject)movingObjectArray[i];
                Vector2 movingObjectOrigin = movingObject.GetOrigin();
                Vector2 movingObjectPosition = movingObject.GetPosition();
                Texture2D movingObjectTexture = movingObject.GetTexture();

                _movingObjectTextureData =
                    new Color[movingObjectTexture.Width * movingObjectTexture.Height];
                movingObjectTexture.GetData(_movingObjectTextureData);

                // Build the movingObject's transform
                Matrix movingObjectTransform =
                    Matrix.CreateTranslation(new Vector3(-movingObjectOrigin, 0.0f)) *
                    Matrix.CreateScale(_scale) *
                    // Matrix.CreateRotationZ(movingObjectRotation) *
                    Matrix.CreateTranslation(new Vector3(movingObjectPosition, 0.0f));

                // Calculate the bounding rectangle of this movingObject in world space
                Rectangle movingObjectRectangle = CalculateBoundingRectangle(
                         new Rectangle(0, 0, movingObjectTexture.Width, movingObjectTexture.Height),
                         movingObjectTransform);

                // The per-pixel check is expensive, so check the bounding rectangles
                // first to prevent testing pixels when collisions are impossible.
                if (subjectRectangle.Intersects(movingObjectRectangle) || subjectRectangle.Contains(movingObjectRectangle))
                {
                    // Check collision with person
                    if (IntersectPixels(subjectTransform, subjectTexture.Width,
                                        subjectTexture.Height, _subjectTextureData,
                                        movingObjectTransform, movingObjectTexture.Width,
                                        movingObjectTexture.Height, _movingObjectTextureData))
                    {
                        collision = true;
                        stack.Push(movingObject);
                    }
                }
            }

            return collision;
        }

        /// <summary>
        /// Check if a weapon is allowed to fire based on gametime
        /// </summary>
        /// <param name="lastFired">Last time(totalMilliseconds) that we fired</param>
        /// <param name="shootTimer">Firing interval(milliseconds)</param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public static bool AllowedToFire(double lastFired, int shootTimer, GameTime gameTime)
        {
            // First check is if the needed time has passed
            // Second check is for when we just had a decorator before us
            if (gameTime.TotalGameTime.TotalMilliseconds >= lastFired + shootTimer ||
                gameTime.TotalGameTime.TotalMilliseconds == lastFired)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a unit vector of the direction from
        /// subject to target
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Vector2 PointToTarget(MovingObject subject, MovingObject target)
        {
            Vector2 v = target.GetPosition() - subject.GetPosition();
            v.Normalize();
            return v;
        }

        //=============================================
        // What follows was taken from 
        // http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed
        // to calculate collision between 
        // transformed elements, in my case
        // the scaling of the pixel art.
        // Why? No need to reinvent the wheel.
        //=============================================

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                           Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(
                            Matrix transformA, int widthA, int heightA, Color[] dataA,
                            Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily
        /// transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}
