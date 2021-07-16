using MazeCore;
using MazeCore.Cells;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCoreTest.Cells
{
    public class FoodTest
    {
        [Test]
        [TestCase(-2)]
        [TestCase(101)]
        public void Constructor_ThrowExeptionIfIvalidValue(int foodinit)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<Exception>(() => new Food(0, 0, null, foodinit));
        }

        [Test]
        [TestCase(0,10,10)]
        [TestCase(20, 20, 40)]
        public void TryStep_CountFood(int foodHero,int foodinit, int foodCount)
        {
            //Arrange
            var heroMock = new Mock<IHero>();
            var mazeMock = new Mock<IMaze>();

         
            heroMock.SetupProperty(x => x.Stamina);
            heroMock.Object.Stamina = foodHero;

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);

            var food = new Food(0, 0, mazeMock.Object, foodinit);
            //Act
            var answer = food.TryStep();
            //Assert
            mazeMock.Verify(x => x.ReplaceCell(It.IsAny<Ground>()), Times.Once);

            Assert.AreEqual(foodCount, heroMock.Object.Stamina);

            Assert.AreEqual(true, answer);
        }
        [Test]
        [TestCase(5)]
        public void TryStep_TrueAfterValidValue(int foodinit)
        {
            //Arrange
            var heroMock = new Mock<IHero>();
            var mazeMock = new Mock<IMaze>();

            heroMock.SetupProperty(x => x.Stamina);
            heroMock.Object.Stamina = 0;

            mazeMock.Setup(x => x.Hero).Returns(heroMock.Object);

            var food = new Food(0, 0, mazeMock.Object, foodinit);
            //Act

            var answer = food.TryStep();

            //Assert

            Assert.AreEqual(true, answer);
        }
    }
}
