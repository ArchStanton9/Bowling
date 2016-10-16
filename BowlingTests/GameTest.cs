using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Bowling;

namespace BowlingTests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void GetScoresTest()
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

            var expectedResult = 133;

            Assert.That(game.GetScore(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void MaxScoreTest()
        {
            var game = new Game();

            game.AddFrame(10);
            game.AddFrame(10);
            game.AddFrame(10);

            game.AddFrame(10);
            game.AddFrame(10);
            game.AddFrame(10);

            game.AddFrame(10);
            game.AddFrame(10);
            game.AddFrame(10);

            game.AddFrame(10, 10, 10);

            var expectedResult = 300;

            Assert.That(game.GetScore(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddFrameExceptionTest()
        {
            var game = new Game();

            Assert.Throws<ArgumentException>(() => game.AddFrame(13,2));
            Assert.Throws<ArgumentException>(() => game.AddFrame(4, 9));
        }
    }
}
