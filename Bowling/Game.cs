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
        
        private List<int> scores;

        public Game()
        {
            Frames = new List<Frame>();
            scores = new List<int>(); 
        }

        public void AddFrame(params int[] shots)
        {
            if (shots.Length == 0 || shots.Length > 3)
                throw new ArgumentException();

            if (shots.Length == 3 && (Frames.Count != GameLength - 1 || shots[0] + shots[1] < 10))
                throw new ArgumentException();

            scores.AddRange(shots);

            Frames.Add(new Frame(shots));

            if (Frames.Count == GameLength)
                GetScores();
        }

        public int GetScores()
        {
            int points = 0;
            int roundIndex = 0;

            for (var i = 0; roundIndex < GameLength; i++)
            {
                if (scores[i] == 10)    // Strike
                {
                    points += 10 + scores[i + 1] + scores[i + 2];
                    Frames[roundIndex].Points = points;
                }
                else if (scores[i] + scores[i + 1] == 10)   // Spare
                {
                    points += 10 + scores[i + 2];
                    Frames[roundIndex].Points = points;
                    i++;
                }
                else
                {
                    points += scores[i] + scores[i + 1];
                    Frames[roundIndex].Points = points;
                    i++;
                }
                roundIndex++;
            }

            return points;
        }
    }
}
