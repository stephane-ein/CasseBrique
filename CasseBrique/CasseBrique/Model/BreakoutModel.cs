﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Breakout.Bonus;
using Breakout.Events;

namespace Breakout.Model
{
    public class BreakoutModel : GameModel
    {
        public BrickZone BrickZone { get; set; }

        public Ball Ball { get; set; }

        public Bar Bar { get; set; }

        public List<AbstractBonus> Bonuses { get; set; }

        public BreakoutModel(int nbBrickCol, int nbBrickRow, float startBlockBrickX, float startBlockBrickY)
        {
            this.BrickZone = new BrickZone(nbBrickCol, nbBrickRow, startBlockBrickX, startBlockBrickY);
            this.Ball = new Ball();
            this.Bar = new Bar();
            this.Bonuses = new List<AbstractBonus>();
        }

        public void AddBrick(Brick brick, int x, int y)
        {
            BrickZone.AddBrick(brick, x, y);
            this.RefreshViews(new AddedBrickEvent(this, brick));
        }

        public void RemoveBrick(Brick brick)
        {
           BrickZone.RemoveBrick(brick);
           this.RefreshViews(new RemovedBrickEvent(this, brick));
        }

        public void UpdateBrickLife(Brick brick, int life)
        {
            brick.Life = life;
            if (brick.Life < 0)
            {
                RemoveBrick(brick);
            }
            this.RefreshViews(new BrickLifeUpdatedEvent(this, brick, life));
        }

        public void AddBonus(AbstractBonus bonus)
        {
            this.Bonuses.Add(bonus);
            this.RefreshViews(new AddedBonusEvent(this, bonus));
        }

        public void RemoveBonus(AbstractBonus bonus)
        {
            this.Bonuses.Remove(bonus);
            this.RefreshViews(new RemovedBonusEvent(this, bonus));
        }
    }
}
