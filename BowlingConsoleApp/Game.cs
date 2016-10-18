using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling;

namespace BowlingConsoleApp
{
    class Game
    {
        private BowlingGame _game;
        public int Length{ get; set; }

        public Game()
        {
            _game = new BowlingGame();
            _game.GameOver += GameOver;
        }

        private void GameOver()
        {
            Console.WriteLine("\nКонец игры.");
            Console.WriteLine(_game.GetResults());
            _game.Reset();
        }

        public void Start()
        {
            Console.WriteLine("Введите через пробел количестов сбитых кегль в фрейме.");
            while (true)
            {
                try
                {
                    Console.Write("Фрейм №{0}: ", _game.Frames.Count + 1);

                    var shots = Console.ReadLine()
                        .Trim(' ')
                        .Split(' ')
                        .Select(s => int.Parse(s))
                        .ToArray();

                    _game.AddFrame(shots);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Не удалось считать количество сбитых кегль. Используйте целые числа");
                }
            }
        }
    }
}
