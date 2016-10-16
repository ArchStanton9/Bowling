using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public enum FrameTags
    {
        Strike,
        Spare,
        Default
    }
    public class Game
    {
        public List<Frame> Frames { get; } 
        public int GameLength { get; set; } = 10;
        private List<int> Scores { get; set; }

        public Game()
        {
            Frames = new List<Frame>();
            Scores = new List<int>(); 
        }

        public void AddFrame(params int[] shots)
        {
            if (shots.Length == 0 || shots.Length > 3)
                throw new ArgumentException();

            if (shots.Length == 3 && (Frames.Count != GameLength - 1 || shots[0] + shots[1] < 10))
                throw new ArgumentException();

            Scores.AddRange(shots);

            Frames.Add(new Frame(shots));
        }

        public int GetScores()
        {
            int points = 0;
            int roundIndex = 0;

            for (var i = 0; roundIndex < GameLength; i++)
            {
                if (Scores[i] == 10)    // Strike
                {
                    points += 10 + Scores[i + 1] + Scores[i + 2];
                    Frames[roundIndex].Points = points;
                }
                else if (Scores[i] + Scores[i + 1] == 10)   // Spare
                {
                    points += 10 + Scores[i + 2];
                    Frames[roundIndex].Points = points;
                    i++;
                }
                else
                {
                    points += Scores[i] + Scores[i + 1];
                    Frames[roundIndex].Points = points;
                    i++;
                }
                roundIndex++;
            }

            return points;
        }
    }
}
