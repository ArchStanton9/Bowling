using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bowling.Tests
{
    [TestFixture]
    class BowlingGameTest
    {
        [TestFixture]
        public class BowlingGameClass
        {
            BowlingGame testGame = new BowlingGame();
            BowlingGame shortGame = new BowlingGame(3);
            BowlingGame longGame = new BowlingGame(10000);

            [Test]
            public void CalculationTest()
            {
                testGame.Reset();

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

                Assert.That(testGame.Score, Is.EqualTo(expectedResult));
            }

            [Test]
            public void MaxScoreTest()
            {
                testGame.Reset();

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

                Assert.That(testGame.Score, Is.EqualTo(expectedResult));
            }

            [Test]
            public void AddFrameExceptionTest()
            {
                testGame.Reset();

                Assert.Throws<ArgumentException>(() => testGame.AddFrame(13, 2));
                Assert.Throws<ArgumentException>(() => testGame.AddFrame(4, 9));
                Assert.Throws<ArgumentException>(() => testGame.AddFrame(1, 2, 4));
                Assert.Throws<ArgumentException>(() => testGame.AddFrame(1, 1, 1, 5));
            }

            [Test]
            public void ShortGameTest()
            {
                shortGame.Reset();

                shortGame.AddFrame(4, 5);
                shortGame.AddFrame(10);
                shortGame.AddFrame(2, 5);

                Assert.That(shortGame.Score, Is.EqualTo(33));
            }

            [Test, MaxTime(5000)]
            public void LongGameTest()
            {
                longGame.Reset();

                for (int i = 0; i < 9999; i++)
                {
                    longGame.AddFrame(5, 5);
                }
                longGame.AddFrame(5, 5, 5);

                Assert.That(longGame.Score, Is.EqualTo(150000));
            }
        }
    }
}
