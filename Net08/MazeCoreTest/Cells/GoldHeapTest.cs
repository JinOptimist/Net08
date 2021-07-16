using MazeCore;
using MazeCore.Cells;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCoreTest.Cells
{
    public class GoldHeapTest
    {
        [Test]
        [TestCase(10, 50, 60)]
        [TestCase(20, 10, 30)]
        [TestCase(0, 50, 50)]
        public void TryStep_ReturnAlwysFalse(
            int heroGoldInit, 
            int goldHeapCount,
            int heroGoldAfter)
        {
            //Preparing
            var mazeMock = new Mock<IMaze>();
            var heroMock = new Mock<IHero>();

            heroMock.SetupProperty(x => x.Gold);
            heroMock.Object.Gold = heroGoldInit;

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);

            var goldHeap =
                new GoldHeap(0, 0, mazeMock.Object, goldHeapCount);

            //Act
            var answer = goldHeap.TryStep();

            //Assert
            Assert.AreEqual(true, answer);

            mazeMock.Verify(x =>
                x.ReplaceCell(It.IsAny<Ground>()), Times.Once);

            Assert.AreEqual(
                heroGoldAfter, 
                heroMock.Object.Gold);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-99)]

        public void Constructor_ThrowAfterNegativeGold(int goldCount)
        {
            //Preparing

            //Act
            //Assert
            Assert.Throws<Exception>(() => new GoldHeap(0, 0, null, goldCount));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(99)]
        public void Constructor_DoesntThrowAfterPositiveGold(int goldCount)
        {
            //Preparing

            //Act
            new GoldHeap(0, 0, null, goldCount);

            //Assert
            Assert.Pass();
        }
    }
}
