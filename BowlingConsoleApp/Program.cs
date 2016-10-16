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

            game.AddFrame(1, 4);
            game.AddFrame(4, 5);
            game.AddFrame(6, 4);

            game.AddFrame(5, 5);
            game.AddFrame(10);
            game.AddFrame(0, 1);

            game.AddFrame(7, 3);
            game.AddFrame(6, 4);
            game.AddFrame(10);

            game.AddFrame(2, 8, 6);
            var result = game.GetScores();
        }
    }
}
