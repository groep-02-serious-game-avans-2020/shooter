using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Events
{
    public class ScoreEventArgs : EventArgs
    {
        public ScoreEventArgs(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; private set; }
    }
}
