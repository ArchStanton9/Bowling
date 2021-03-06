﻿using System;
using System.Linq;
using Bowling;
using System.Collections.Generic;

namespace BowlingConsoleApp
{
    class App
    {
        private BowlingGame game;

        public App()
        {
            game = new BowlingGame();
            game.GameOver += GameOver;
        }

        private void GameOver()
        {
            Console.WriteLine("\n" + "Конец игры.");
            Console.WriteLine(game.GetResults());
            game.Reset();
        }

        static Dictionary<string, Action<BowlingGame>> commands = new Dictionary<string, Action<BowlingGame>>()
        {
            { "quit", (game) => Environment.Exit(0) },
            { "result", (game) => Console.WriteLine("\n" + game.GetResults()) },
            { "help", (game) =>
                {
                    Console.WriteLine("\n" + "Список команд:");
                    foreach (var key in commands.Keys)
                        Console.WriteLine(key);
                }
            },
            { "remove", (game) =>
                {
                    if (game.Frames.Count != 0)
                    {
                        Console.WriteLine("\n" + "Фрейм №{0} был удален.", game.Frames.Count);
                        game.Remove();
                    }
                }
            },
            { "reset", (game) =>
                {
                    game.Reset();
                    Console.WriteLine("\n" + "Результаты игры сброшены.");
                }
            },
            { "score", (game) => Console.WriteLine("\n" + "Текущий счет: {0}", game.Score) }
        };

        public void Run()
        {
            Console.WriteLine("Введите через пробел количестов сбитых кегль в фрейме.");
            Console.WriteLine("Введите help для получения списка команд.");

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
