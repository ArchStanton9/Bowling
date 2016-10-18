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
    public class BowlingGame
    {
        static public int PinsCount { get; set; } = 10;
        public List<Frame> Frames { get; private set; } 
        public int GameLength { get; set; } = 10;
        private List<int> _shots;

        private bool isLastRound() => Frames.Count == GameLength - 1;
        public event Action GameOver;

        public BowlingGame() : this(10) { }

        public BowlingGame(int length)
        {
            GameLength = length;
            Reset();
        }

        public void Reset()
        {
            Frames = new List<Frame>();
            _shots = new List<int>();
        }

        public void AddFrame(params int[] shots)
        {
            if (shots.Length == 0)  // Если пришел пустой массив превращаем его в Фрейм {0, 0}
                shots = new[] { 0, 0 };

            if (shots.Length == 2 && shots[0] == 10)    // фрейм вида {10, 0} превращаем в {10}
                shots = new[] { 10 };

            ValidateShots(shots);
            _shots.AddRange(shots);
            Frames.Add(new Frame(shots));

            Calculate();

            if (Frames.Count == GameLength)
                GameOver?.Invoke();
        }

        private void ValidateShots(int[] shots)
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
                    if (shots[0] != PinsCount)
                        throw new ArgumentException("Если количество сбитых первым броском кегль меньше десяти, нужно произвести еще один бросок");
                    if (isLastRound())
                        throw new ArgumentException("Некорректное количество бросков в последнем фрейме. Если первым броском выбит страйк то можно сделать еще два бонусных броска.");
                    break;

                case 2:
                    if (shots[0] + shots[1] > PinsCount)
                        throw new ArgumentException("Сумма очков в этом фрейме не может быть больше 10.");
                    if (isLastRound() && shots[0] + shots[1] == PinsCount)
                        throw new ArgumentException("Некорректное количество бросков в последнем фрейме.");
                    break;

                case 3:
                    if (!isLastRound())
                        throw new ArgumentException("Количество бросков в этом фрейме не может быть больше двух.");
                    if (shots[0] + shots[1] < PinsCount && shots[3] != 0)
                        throw new ArgumentException("Нельзя делать третий бросок, если все кегли не были сбиты");
                    break;

                default:
                    var message = string.Format("Количество бросков в одном фрейме не может быть равно {0}.", shots.Length);
                    throw new ArgumentException(message);
            }
        }

        public int Calculate()
        {
            var points = 0;
            var round = 0;

            for (var i = 0; i < _shots.Count - 1 && round < Frames.Count; i += 2)
            {
                points += _shots[i] + _shots[i + 1];
                Frames[round].Points = points;

                if (Frames[round].Tag == FrameTags.Strike && i + 2 < _shots.Count)
                {
                    points += _shots[i + 2];
                    Frames[round].Points = points;
                    i--;
                }

                if (Frames[round].Tag == FrameTags.Spare && i + 2 < _shots.Count)
                {
                    points += _shots[i + 2];
                    Frames[round].Points = points;
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
                result.AppendFormat("Фрейм №{0}: {1}\n", i + 1, Frames[i].ToString());
            }
            return result.ToString();
        }
    }
}
