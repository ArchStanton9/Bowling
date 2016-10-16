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
        public List<Frame> Frames { get; private set; } 
        public int GameLength { get; set; } = 10;
        public List<int> Shots { get; private set; }

        private bool isLastRound() => Frames.Count == GameLength - 1;

        public Game()
        {
            Reset();
        }

        public Game(int length)
        {
            GameLength = length;
            Reset();
        }

        public void AddFrame(params int[] shots)
        {
            ValidateInput(shots);
            Shots.AddRange(shots);
            Frames.Add(new Frame(shots));
            GetScore();
        }

        private bool ValidateInput(int[] shots)
        {
            foreach (var shot in shots)
            {
                if (shot < 0 || shot > 10)
                {
                    var message = string.Format("Количество сбитых кеглей не может быть равно {0}.", shot);
                    throw new ArgumentException(message);
                }     
            }

            switch (shots.Length)
            {
                case 1:
                    if (isLastRound())
                        throw new ArgumentException("Некорректное количество бросков в последнем фрейме.");
                    break;
                case 2:
                    if (shots[0] + shots[1] > 10)
                        throw new ArgumentException
                            ("Сумма очков в этом фрейме не может быть больше 10.");
                    if (isLastRound() && shots[0] + shots[1] == 10)
                        throw new ArgumentException("Некорректное количество бросков в последнем фрейме.");
                    break;
                case 3:
                    if (!isLastRound())
                        throw new ArgumentException("Количество бросков в этом фрейме не может быть больше двух.");
                    break;
                default:
                    var message = string.Format("Количество бросков в одном фрейме не может быть равно {0}.", shots.Length);
                    throw new ArgumentException(message);
            }

            return true;
        }

        public int GetScore()
        {
            int points = 0;
            int round = 0;

            for (var i = 0; i < Shots.Count - 2; i++)
            {
                // Strike! добавить 10 плюс два следующих броска
                if (Shots[i] == 10)    
                {
                    points += 10 + Shots[i + 1] + Shots[i + 2];
                    Frames[round].Points = points;
                }
                // Spare! добавить 10 плюс следующий бросок
                else if (Shots[i] + Shots[i + 1] == 10)  
                {
                    points += 10 + Shots[i + 2];
                    Frames[round].Points = points;
                    i++;
                }
                // обычный фрейм
                else
                {
                    points += Shots[i] + Shots[i + 1];
                    Frames[round].Points = points;
                    i++;
                }
                round++;
            }

            return points;
        }

        public void Reset()
        {
            Frames = new List<Frame>();
            Shots = new List<int>();
        }
    }
}
