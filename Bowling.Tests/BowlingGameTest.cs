using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bowling.Tests
{
    [TestFixture]
    public class BowlingGameTest
    {
        BowlingGame testGame = new BowlingGame();
        BowlingGame shortGame = new BowlingGame(3);
        BowlingGame longGame = new BowlingGame(10000);

        [SetUp]
        public void ResetGames()
        {
            testGame.Reset();
            shortGame.Reset();
            longGame.Reset();
        }

        [Test]
        public void CalculationTest()
        {
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
            for (int i = 0; i < 9; i++)
            {
                testGame.AddFrame(10);
            }

            testGame.AddFrame(10, 10, 10);

            Assert.That(testGame.Score, Is.EqualTo(300));
        }

        [Test]
        public void AddFrameWrongArgumentTest()
        {
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(13, 2));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(4, 9));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(1, 2, 4));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(10, 1, 1, 5));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(0, 0, 8, 5));
        }

        [Test]
        public void AddFrameNegativeArgumentTest()
        {
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(-1, -6));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(-4, 4));
            Assert.Throws<ArgumentException>(() => testGame.AddFrame(0, -9));
        }

        [Test]
        public void AddFrameLimitTest()
        {
            shortGame.AddFrame(4, 3);
            shortGame.AddFrame(5, 1);
            shortGame.AddFrame(4, 2);

            Assert.Throws<IndexOutOfRangeException>(() => shortGame.AddFrame(7, 2));
        }

        [Test]
        public void NullScoreTest()
        {
            Assert.That(testGame.Score, Is.Not.Null);
            Assert.That(testGame.Score, Is.EqualTo(0)); 
        }

        [Test]
        public void GameLengthCtorTest()
        {
            Assert.Throws<ArgumentException>(() => new BowlingGame(-1));
        }

        [Test]
        public void ShortGameTest()
        {
            shortGame.AddFrame(4, 5);
            shortGame.AddFrame(10);
            shortGame.AddFrame(2, 5);

            Assert.That(shortGame.Score, Is.EqualTo(33));
        }

        [Test, MaxTime(2000)]
        public void LongGameTest()
        {
            for (int i = 0; i < 9999; i++)
            {
                longGame.AddFrame(5, 5);
            }
            longGame.AddFrame(5, 5, 5);

            Assert.That(longGame.Score, Is.EqualTo(150000));
        }

        [Test]
        public void PinsCountTest()
        {
            BowlingGame.PinsCount = 25;
            shortGame.AddFrame(14, 2);
            shortGame.AddFrame(25);
            shortGame.AddFrame(15, 10, 21);
            BowlingGame.PinsCount = 10;

            Assert.That(shortGame.Score, Is.EqualTo(112));
        }

        [Test]
        public void RemoveTest()
        {
            testGame.AddFrame(10);  // 24
            testGame.AddFrame(10);  // 43
            testGame.AddFrame(4, 5); // 52
            testGame.Remove();

            Assert.That(testGame.Score, Is.EqualTo(30));
        }
    }
}
