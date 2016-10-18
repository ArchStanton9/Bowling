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
        public int? BonusShot { get; }
        public int Points { get; set; }
        public FrameTags Tag { get; } = FrameTags.Default;

        internal Frame(params int[] shots)
        {
            FirstShot = shots[0];

            if (shots.Length > 1)
                SecondShot = shots[1];
            
            if (shots.Length > 2)
                BonusShot = shots[2];

            if (FirstShot == BowlingGame.PinsCount)
            {
                Tag = FrameTags.Strike;
            }
            else if(FirstShot + SecondShot == BowlingGame.PinsCount)
            {
                Tag = FrameTags.Spare;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (BonusShot != null)
            {
                result.AppendFormat("Сбито {0} + {1} + {2} кегль.\t", FirstShot, SecondShot, BonusShot);
            }
            else if (Tag == FrameTags.Strike)
            {
                result.Append("Сбито 10 кегль. Strike! \t");
            }
            else if (Tag == FrameTags.Spare)
            {
                result.AppendFormat("Сбито {0} + {1} кегль. Spare!\t", FirstShot, SecondShot);
            }
            else
            {
                result.AppendFormat("Сбито {0} + {1} кегль.\t\t", FirstShot, SecondShot);
            }

            result.AppendFormat(" Количество очков: {0}", Points);

            return result.ToString();
        }
    }
}
