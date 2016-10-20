using System.Linq;
using System.Text;

namespace Bowling
{
    public class Frame
    {
        public int FirstShot { get; }
        public int? SecondShot { get; }
        public int? BonusShot { get; }
        public int Points { get; set; }
        public Frame PreFrame { get; set; }
        public FrameTags Tag { get; private set; }

        internal Frame(Frame preFrame, params int[] shots)
        {
            FirstShot = shots[0];
            PreFrame = preFrame;
            Points = shots.Sum();

            if (shots.Length > 1)
                SecondShot = shots[1];
                
            if (shots.Length > 2)
                BonusShot = shots[2];

            Tag = GetTag();
            if (preFrame != null)
                CalculatePoints();
        }

        void CalculatePoints()
        {
            if (PreFrame.Tag == FrameTags.Strike && PreFrame.PreFrame?.Tag == FrameTags.Strike)
            {
                PreFrame.PreFrame.Points += FirstShot;
                PreFrame.Points += FirstShot;
            }
            if (PreFrame.Tag == FrameTags.Strike)
            {
                PreFrame.Points += SecondShot == null ? FirstShot : FirstShot + (int)SecondShot;
            }
            if (PreFrame.Tag == FrameTags.Spare)
            {
                PreFrame.Points += FirstShot;
            }

            Points += PreFrame.Points;
        }

        private FrameTags GetTag()
        {
            if (FirstShot == BowlingGame.PinsCount)
            {
                return FrameTags.Strike;
            }
            else if (FirstShot + SecondShot == BowlingGame.PinsCount)
            {
                return FrameTags.Spare;
            }

            return FrameTags.Default;
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
