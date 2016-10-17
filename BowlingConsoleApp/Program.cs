using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling;

namespace BowlingConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            Console.WriteLine("Введите количество сбитых кегль каждым из бросков через пробел.");

            game.GameOver += GameOver;

            while (true)
            {
                try
                {
                    Console.Write("Фрейм №{0}: ", game.Frames.Count + 1);
                    var input = Console.ReadLine();

                    var shots = input
                        .Trim(' ')
                        .Split(' ')
                        .Select(s => int.Parse(s))
                        .ToArray();

                    game.AddFrame(shots);
                }
                catch (ArgumentException e )
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Не удалось считать количество сбитых кегль. Используйте целые числа");
                }
                
                if (game.Frames.Count == 10)
                {
                    Console.WriteLine(game.GetResults());
                    game = new Game();
                    Console.WriteLine("Введите количество сбитых кегль.");
                }

            }
        }

        private static void GameOver()
        {
            Console.WriteLine("\nКонец игры.");
        }
    }
}
