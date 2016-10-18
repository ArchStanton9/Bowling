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
        BowlingGame _testGame = new BowlingGame();
        BowlingGame _shortGame = new BowlingGame(3);
        BowlingGame _longGame = new BowlingGame(10000);

        [Test]
        public void CalculationTest()
        {
            _testGame.Reset();

            _testGame.AddFrame(1, 4);
            _testGame.AddFrame(4, 5);
            _testGame.AddFrame(6, 4);

            _testGame.AddFrame(5, 5);
            _testGame.AddFrame(10);
            _testGame.AddFrame(0, 1);

            _testGame.AddFrame(7, 3);
            _testGame.AddFrame(6, 4);
            _testGame.AddFrame(10);

            _testGame.AddFrame(2, 8, 6);

            var expectedResult = 133;

            Assert.That(_testGame.Calculate(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void MaxScoreTest()
        {
            _testGame.Reset();

            _testGame.AddFrame(10);
            _testGame.AddFrame(10);
            _testGame.AddFrame(10);

            _testGame.AddFrame(10);
            _testGame.AddFrame(10);
            _testGame.AddFrame(10);

            _testGame.AddFrame(10);
            _testGame.AddFrame(10);
            _testGame.AddFrame(10);

            _testGame.AddFrame(10, 10, 10);

            var expectedResult = 300;

            Assert.That(_testGame.Calculate(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddFrameExceptionTest()
        {
            _testGame.Reset();

            Assert.Throws<ArgumentException>(() => _testGame.AddFrame(13,2));
            Assert.Throws<ArgumentException>(() => _testGame.AddFrame(4, 9));
            Assert.Throws<ArgumentException>(() => _testGame.AddFrame(1, 2, 4));
            Assert.Throws<ArgumentException>(() => _testGame.AddFrame(1, 1, 1, 5));
        }

        [Test]
        public void ShortGameTest()
        {
            _shortGame.Reset();

            _shortGame.AddFrame(4, 5);
            _shortGame.AddFrame(10);
            _shortGame.AddFrame(2, 5);

            Assert.That(_shortGame.Calculate(), Is.EqualTo(33));
        }

        [Test]
        public void LongGameTest()
        {
            _longGame.Reset();

            for (int i = 0; i < 10000; i++)
            {
                _longGame.AddFrame(5, 5);
            }
        }
    }
}
