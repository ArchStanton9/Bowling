using System;
using System.Linq;
using Bowling;

namespace BowlingConsoleApp
{
    class Game
    {
        private BowlingGame game;
        public int Length{ get; set; }

        public Game()
        {
            game = new BowlingGame();
            game.GameOver += GameOver;
        }

        private void GameOver()
        {
            Console.WriteLine("\nКонец игры.");
            Console.WriteLine(game.GetResults());
            game.Reset();
        }

        public void Start()
        {
            Console.WriteLine("Введите через пробел количестов сбитых кегль в фрейме.");
            while (true)
            {
                try
                {
                    Console.Write("Фрейм №{0}: ", game.Frames.Count + 1);

                    var shots = Console.ReadLine()
                        .Trim(' ')
                        .Split(' ')
                        .Select(s => int.Parse(s))
                        .ToArray();

                    game.AddFrame(shots);
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
