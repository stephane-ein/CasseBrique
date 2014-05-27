﻿using Breakout.Model;
using Breakout.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Model
{
    public class Ball : Shape
    {
        public Ball() : base(Vector2.Zero, Vector2.Normalize(new Vector2(-1)), 0.1f, new Size(0, 0))
        {
        }

        public Ball(Vector2 position, Vector2 deplacement, float speed) : base(position, deplacement, speed, new Size(0, 0))
        {

        }

        public override void HandleTrajectory(BreakoutModel model, GameTime gameTime, int heightFrame, int widthFrame)
        {
            Bar bar = model.CurrentPlayer.Bar;
            BrickZone bricks = model.BrickZone;

            //balle sort du jeu
            if (Position.Y > heightFrame)
            {
                //handle when the player loose the game
            }

            HandleTrajectoryBallReboundBar(bar, gameTime, heightFrame, widthFrame);
            HandleTrajectoryBallReboundFrame(bar, gameTime, heightFrame, widthFrame);
            HandleBallReboundBrick(model);

            Position += Deplacement * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void HandleTrajectoryBallReboundBar(Bar bar, GameTime gameTime, int heightFrame, int widthFrame)
        {
            if (bar.getRectangle().Contains((int)(Position.X), (int)(Position.Y + bar.Size.Height)))
            {
                RuleBall.HandleReboundUpDown(this);
            }

        }

        public void HandleTrajectoryBallReboundFrame(Bar bar, GameTime gameTime, int heightFrame, int widthFrame)
        {
            //rebond à gaiche ou à droite
            if ((Position.X < 0 || Position.X > widthFrame))
            {
                RuleBall.BallReboundLeftRight(this);
            }
            //rebond en haut
            else if (Position.Y < 0)
            {
                RuleBall.HandleReboundUpDown(this);
            }
        }

        public void HandleBallReboundBrick(BreakoutModel model)
        {
            BrickZone bricks = model.BrickZone;
            Brick brick = RuleBall.GetBrickHit(this, bricks);

            if(brick != null)
            {
                RuleBall.HandleDeplacementHitBrick(model, brick);
            }
        }
    }
}
