﻿using Breakout.Model;
using Microsoft.Xna.Framework;
using System;

namespace Breakout.Model
{
    public static class RuleBall
    {
        public static Brick GetBrickHit(Ball ball, BrickZone bricks)
        {
            Brick result = null;
            Vector2 positionBall = ball.Position;

            //la balle et dans la zone de brique
            if (CheckBallEnterBlockBrick(ball.Position, bricks))
            {
                int brickX = (int)((positionBall.X - bricks.StartBlockBrickX) / bricks.WidthBrick);
                int brickY = (int)((positionBall.Y - bricks.StartBlockBrickY) / bricks.HeightBrick);

                Console.WriteLine("Enter in blockBrick      " + brickX+" , "+brickY );
                result = bricks.AllBricks[brickX, brickY];
            }

            return result;
        }

        public static bool CheckBallEnterBlockBrick(Vector2 positionBall, BrickZone bricks)
        {
            return (positionBall.X > bricks.StartBlockBrickX && positionBall.X < bricks.EndBlockBrickX
                && positionBall.Y > bricks.StartBlockBrickY && positionBall.Y < bricks.EndBlockBrickY);
        }

        public static void HandleDeplacementHitBrick(Ball ball, Brick brick)
        {
            int widthBrick = brick.Size.Width;
            int heightBrick = brick.Size.Height;

            Vector2 positionBall = ball.Position;
            Vector2 centerBrick = new Vector2(brick.Position.X + widthBrick, brick.Position.Y + heightBrick);
            Vector2 newDeplacement = Vector2.Zero;

            //la balle a touché la brique à droite
            if (positionBall.X > centerBrick.X)
            {
                Console.WriteLine("In RulleBall ball touché à droite");
                //la balle a touché la brique dans la partie haute
                if (positionBall.Y < centerBrick.Y)
                {
                    Console.WriteLine("             In RulleBall ball touché en haut");
                    float diffX = centerBrick.X + widthBrick - positionBall.X;
                    float diffY = positionBall.Y - centerBrick.Y - heightBrick;

                    HandleVarianceXAndY(ball, diffX, diffY);
                }
                else
                {
                    //la balle a touché la brique dans la partie basse
                    float diffX = centerBrick.X + widthBrick - positionBall.X;
                    float diffY = centerBrick.Y + heightBrick - positionBall.Y;

                    HandleVarianceXAndY(ball, diffX, diffY);
                }

            }
            else
            {
                Console.WriteLine("In RulleBall ball touché à gauche au au milieu");
                //la balle a touché la brique à gauche

                if (positionBall.Y < centerBrick.Y)
                {

                    Console.WriteLine("             In RulleBall ball touché en haut");
                    //la balle a touché la brique dans la partie haute
                    float diffX = positionBall.X - centerBrick.X - widthBrick;
                    float diffY = positionBall.Y - centerBrick.Y - heightBrick;

                    HandleVarianceXAndY(ball, diffX, diffY);
                }
                else
                {
                    //la balle a touché la brique dans la partie basse
                    float diffX = positionBall.X - centerBrick.X - widthBrick;
                    float diffY = centerBrick.Y + heightBrick - positionBall.Y;

                    HandleVarianceXAndY(ball, diffX, diffY);
                }
            }
        }

        public static void BallReboundLeftRight(Ball ball)
        {
            if ((ball.Deplacement.X > 0 || ball.Deplacement.X < 0) && (ball.Deplacement.Y < 0 || ball.Deplacement.Y > 0))
            {
                ball.Deplacement = Vector2.Normalize(new Vector2(-ball.Deplacement.X, ball.Deplacement.Y));
            }
        }

        public static void BallReboundDown(Ball ball)
        {
            if ((ball.Deplacement.X > 0 || ball.Deplacement.X < 0) && ball.Deplacement.Y > 0)
            {
                ball.Deplacement = Vector2.Normalize(new Vector2(ball.Deplacement.X, -ball.Deplacement.Y));
            }
        }

        public static void BallReboundTop(Ball ball)
        {
            if ((ball.Deplacement.X > 0 || ball.Deplacement.X < 0) && ball.Deplacement.Y < 0)
            {
                ball.Deplacement = Vector2.Normalize(new Vector2(ball.Deplacement.X, -ball.Deplacement.Y));
            }
        }

        public static void HandleVarianceXAndY(Ball ball, float diffX, float diffY)
        {
            if (diffX < diffY)
            {
                //la balle a touché la brique à droite
                BallReboundLeftRight(ball);
            }
            else
            {
                //la balle a touché la brique en haut
                BallReboundDown(ball);
            }
        }
    }
}
