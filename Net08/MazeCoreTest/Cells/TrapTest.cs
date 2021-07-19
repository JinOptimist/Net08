using MazeCore;
using MazeCore.Cells;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCoreTest.Cells
{
    public class TrapTest
    {
        [Test]
        [TestCase(100,60,40)]
        [TestCase(55, 20, 35)]
        [TestCase(40, 60, 0)]
        [TestCase(90, 60, 30)]
        public void TryStepToTheTrap(int HeroInitHp , int HeroHpLoseCount, int HeroResultHp)
        {
            var mazeMock = new Mock<IMaze>();
            var heroMock = new Mock<IHero>();

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);
            heroMock.SetupProperty(x => x.HP);
            heroMock.Object.HP = HeroInitHp;
            var trap = new Trap(0, 0, mazeMock.Object,3, HeroHpLoseCount);

            var answer = trap.TryStep();

            Assert.AreEqual(true,answer);
            Assert.AreEqual(2, trap._trapCharges);
            mazeMock.Verify(x => x.ReplaceCell(It.IsAny<Ground>()), Times.Never);
            //mazeMock.Verify(x => x.ReplaceCell(It.IsAny<Wall>()), Times.Never);
            Assert.AreEqual(HeroResultHp, heroMock.Object.HP);

        }
        [Test]
        [TestCase(3,9)]
        [TestCase(2,40)]
        public void Costructor_DoesnotThrowAfterpositiveNumbs(int trapCharge,int hpLose)
        {
            new Trap(0, 0, null, trapCharge, hpLose);

            Assert.Pass();
        }
    }
}
