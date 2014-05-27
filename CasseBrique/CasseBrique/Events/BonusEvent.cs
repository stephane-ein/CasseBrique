﻿using Breakout.Bonus;
using Breakout.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Events
{
    public class BonusEvent : Event
    {
        public AbstractBonus Bonus { get; set; }

        public BonusEvent() : this(null, null)
        {
        }

        public BonusEvent(AbstractModel model, AbstractBonus bonus) : base(model)
        {
            this.Bonus = bonus;
        }
    }
}
