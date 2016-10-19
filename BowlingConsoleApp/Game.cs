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

        static string CommandList()
        {
            var info = new StringBuilder("\nСписок команд:\n");
            foreach (var key in commands.Keys)
            {
                info.Append(key + "\n");
            }
            return info.ToString();
        }

        static Dictionary<string, Action<BowlingGame>> commands = new Dictionary<string, Action<BowlingGame>>()
        {
            { "quit", (game) => Environment.Exit(0) },
            { "remove", (game) => 
                {
                    game.RemoveLastFrame();
                    Console.WriteLine("Раунд №{0} был удален.", game.Frames.Count);
                }
            },
            { "reset", (game) =>
                {
                    game.Reset();
                    Console.WriteLine("Результаты игры сброшены.");
                }
            },
            { "help", (game) =>  Console.WriteLine(CommandList()) },
            { "result", (game) => Console.WriteLine(game.GetResults()) }
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
                    Console.WriteLine("Не удалось считать команду");
                }
            }
        }
    }
}
