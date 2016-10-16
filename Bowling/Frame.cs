using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Frame
    {
        public int FirstShot { get; }
        public int SecondShot { get; }
        public int BonusShot { get; set; }
        public int Points { get; set; }
        public FrameTags Tag { get; } = FrameTags.Default;

        private const int maxScore = 10;
        internal Frame(params int[] shots)
        {
            FirstShot = shots[0];

            if (shots.Length > 1)
                SecondShot = shots[1];
            
            if (shots.Length > 2)
                BonusShot = shots[2];

            if (FirstShot == maxScore)
            {
                Tag = FrameTags.Strike;
            }
            else if(FirstShot + SecondShot == maxScore)
            {
                Tag = FrameTags.Spare;
            }
        }
    }
}
