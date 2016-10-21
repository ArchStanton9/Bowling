using System;
using System.Linq;
using Bowling;
using System.Collections.Generic;
using System.Text;

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

        static Dictionary<string, Action<BowlingGame>> commands = new Dictionary<string, Action<BowlingGame>>()
        {
            { "quit", (game) => Environment.Exit(0) },
            { "result", (game) => Console.WriteLine(game.GetResults()) },
            { "help", (game) =>
                {
                    Console.WriteLine("\nСписок команд:");
                    foreach (var key in commands.Keys)
                        Console.WriteLine(key);
                }
            },
            { "remove", (game) => 
                {
                    Console.WriteLine("Фрейм №{0} был удален.", game.Frames.Count);
                    game.Remove();
                }
            },
            { "reset", (game) =>
                {
                    game.Reset();
                    Console.WriteLine("Результаты игры сброшены.");
                }
            }
        };

        public void Start()
        {
            Console.WriteLine("Введите через пробел количестов сбитых кегль в фрейме.");
            while (true)
            {
                try
                {
                    Console.Write("Фрейм №{0}: ", game.Frames.Count + 1);

                    var key = Console.ReadLine().ToLower();

                    if (commands.Keys.Contains(key))
                    {
                        commands[key]?.Invoke(game);
                        Console.WriteLine();
                        continue;
                    }    

                    var shots = key
                        .Trim(' ')
                        .Split(' ')
                        .Where(s => s.Length != 0)
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
                    Console.WriteLine("Неизвестная команда. Используйте help для получения списка команд.");
                }
            }
        }
    }
}
