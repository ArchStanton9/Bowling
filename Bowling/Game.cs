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
        public event Action GameOver;

        public Game()
        {
            Frames = new List<Frame>();
            Shots = new List<int>();
        }

        public Game(int length)
        {
            GameLength = length;
            Frames = new List<Frame>();
            Shots = new List<int>();
        }

        public void AddFrame(params int[] shots)
        {
            ValidateInput(shots);
            Shots.AddRange(shots);
            Frames.Add(new Frame(shots));
            GetScore();
            if (Frames.Count == GameLength)
                GameOver?.Invoke();
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
                    if (shots[0] != 10)
                        throw new ArgumentException("Если количество сбитых первым броском кегль меньше десяти, нужно произвести еще один бросок");
                    break;
                case 2:
                    if (shots[0] + shots[1] > 10)
                        throw new ArgumentException("Сумма очков в этом фрейме не может быть больше 10.");
                    if (isLastRound() && shots[0] + shots[1] == 10)
                        throw new ArgumentException("Некорректное количество бросков в последнем фрейме.");
                    break;
                case 3:
                    if (!isLastRound())
                        throw new ArgumentException("Количество бросков в этом фрейме не может быть больше двух.");
                    if (shots[0] + shots[1] < 10 && shots[3] != 0)
                        throw new ArgumentException("Нельзя делать третий бросок, если все кегли не были сбиты");
                    if (shots[1] != 10 && shots[1] + shots[2] > 10)
                        throw new ArgumentException("Некорректное количество очков в поледних двух бросках");
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

        public string GetResults()
        {
            var result = new StringBuilder("Результы:\n");
            for (int i = 0; i < Frames.Count; i++)
            {
                result.AppendFormat("Фрейм №{0}: {1}\n", i, Frames[i].ToString());
            }
            return result.ToString();
        }
    }
}
