using MazeCore;
using MazeCore.Cells;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCoreTest.Cells
{
    public class WaterTest
    {
        [Test]
        [TestCase(10)]
        public void TryStep_ReturnAlwysTrue(int heroHP)
        {
            //Preparing
            var mazeMock = new Mock<IMaze>();
            var heroMock = new Mock<IHero>();

            heroMock.Object.HP = heroHP;
            heroMock.SetupProperty(x => x.HP);

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);
            var water = new Water(0, 0, mazeMock.Object, 0);

            //Act
            var answer = water.TryStep();

            //Assert
            Assert.AreEqual(true, answer);
        }
        [Test]
        [TestCase(10, 1, 9)]
        [TestCase(20, 5, 15)]
        [TestCase(100, 99, 1)]
        public void TryStep_lossOfHP(
            int heroHP,
            int damage,
            int heroHPAfter)
        {
            //Preparing
            var mazeMock = new Mock<IMaze>();
            var heroMock = new Mock<IHero>();


            heroMock.SetupProperty(x => x.HP);
            heroMock.Object.HP = heroHP;

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);

            var water = new Water(0, 0, mazeMock.Object, damage);

            //Act
            var answer = water.TryStep();

            //Assert
            Assert.AreEqual(heroHPAfter, heroMock.Object.HP);
        }
    }
}
