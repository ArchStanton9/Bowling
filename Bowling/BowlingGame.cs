using System;
using System.Collections.Generic;
using System.Text;

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

        public int GameLength { get; } = 10;
        public int Score => lastFrame != null  ? lastFrame.Points : 0;
        public event Action GameOver;
        public Stack<Frame> Frames { get; private set; }

        private Frame lastFrame;
        private bool isLastRound() => Frames.Count == GameLength - 1;
        
        public BowlingGame() : this(10) { }

        public BowlingGame(int length) 
        {
            if (length < 1)
            {
                var message = string.Format("Количество фреймов в одной ирге не может быть равно {0}", length);
                throw new ArgumentException(message);
            }
            GameLength = length;
            Reset();
        }

        public void Reset()
        {
            Frames = new Stack<Frame>();
            lastFrame = null;
        }

        public void AddFrame(params int[] shots)
        {
            if (Frames.Count == GameLength)
            {
                var message = string.Format("Количество фреймов в этой игре не может быть больше {0}", GameLength);
                throw new IndexOutOfRangeException(message);
            }

            if (shots.Length == 0)  // Если пришел пустой массив превращаем его в Фрейм {0, 0}
                shots = new[] { 0, 0 };

            if (shots.Length == 2 && shots[0] == PinsCount)    // фрейм вида {10, 0} превращаем в {10}
                shots = new[] { 10 };

            ValidateFrame(shots);
            Frames.Push(new Frame(lastFrame, shots));
            lastFrame = Frames.Peek();

            if (Frames.Count == GameLength)
                GameOver?.Invoke();
        }

        public void Remove()
        {
            if (Frames.Count != 0)
            {
                /* -------------------------------------------sophistic----------------------------------------------
                 * Мы не можем просто выкинуть из стека последний фрейм и поменять ссылку на голову.
                 * Нужно откатить измения в предыдущих раундах, которые были сделаны при создании удаляемого фрейма.
                 * Поскольку эти изменения происходят в конструкторе класса,
                 * создадим фрейм с отрицательным количеством сбитых кегль,
                 * который вернет значение очков в предыдущих раундах в исходное состояние.
                */
                var shots = new List<int>();
                shots.Add(-lastFrame.FirstShot);
                if (lastFrame.SecondShot != null)
                    shots.Add(-(int)lastFrame.SecondShot);
                if (lastFrame.BonusShot != null)
                    shots.Add(-(int)lastFrame.BonusShot);
   
                lastFrame = Frames.Pop().PreFrame;
                new Frame(lastFrame, shots.ToArray());
            }
        }

        private void ValidateFrame(int[] shots)
        {
            foreach (var shot in shots)
            {
                if (shot < 0 || shot > PinsCount)
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
                        throw new ArgumentException("В последнем раунде доступен еще один бросок после спэа");
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


        public string GetResults()
        {
            var result = new StringBuilder("Результы:\n");
            var frames = Frames.ToArray();
            var index = 1;

            for (int i = frames.Length - 1; i >= 0; i--)
            {
                result.AppendFormat("Фрейм №{0}: {1}\n", index, frames[i].ToString());
                index++;
            }

            result.AppendFormat("Финальный счет: {0}.", Score);

            return result.ToString();
        }
    }
}
