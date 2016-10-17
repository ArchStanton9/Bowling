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
        Game testGame;
        [Test]
        public void GetScoresTest()
        {
            testGame = new Game();

            testGame.AddFrame(1, 4);
            testGame.AddFrame(4, 5);
            testGame.AddFrame(6, 4);

            testGame.AddFrame(5, 5);
            testGame.AddFrame(10);
            testGame.AddFrame(0, 1);

            testGame.AddFrame(7, 3);
            testGame.AddFrame(6, 4);
            testGame.AddFrame(10);

            testGame.AddFrame(2, 8, 6);

            var expectedResult = 133;

            Assert.That(testGame.GetScore(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void MaxScoreTest()
        {
            testGame = new Game();

            testGame.AddFrame(10);
            testGame.AddFrame(10);
            testGame.AddFrame(10);

            testGame.AddFrame(10);
            testGame.AddFrame(10);
            testGame.AddFrame(10);

            testGame.AddFrame(10);
            testGame.AddFrame(10);
            testGame.AddFrame(10);

            testGame.AddFrame(10, 10, 10);

            var expectedResult = 300;

            Assert.That(testGame.GetScore(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddFrameExceptionTest()
        {
            var game = new Game();

            Assert.Throws<ArgumentException>(() => game.AddFrame(13,2));
            Assert.Throws<ArgumentException>(() => game.AddFrame(4, 9));
            Assert.Throws<ArgumentException>(() => game.AddFrame());
            Assert.Throws<ArgumentException>(() => game.AddFrame(1, 2, 4));
            Assert.Throws<ArgumentException>(() => game.AddFrame(1, 1, 1, 5));
        }
    }
}
