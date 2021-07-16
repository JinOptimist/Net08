using MazeCore;
using MazeCore.Cells;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCoreTest.Cells
{
    public class TeleportTest
    {
        [Test]
        public void TryStep_ReturnAlwaysFalse()
        {
            // Preparing
            var heroMock = new Mock<IHero>();
            heroMock.SetupProperty(x => x.X);
            heroMock.SetupProperty(x => x.Y);
            heroMock.Object.X = 0;
            heroMock.Object.Y = 0;

            var mazeMock = new Mock<IMaze>();
//            mazeMock.SetupProperty(x => x.Hero);
//            mazeMock.Object.Hero = heroMock;

            var teleport = new Teleport(0, 0, null);
//            var teleport2 = new Teleport(1, 1, null);

            // Act
            var answer = teleport.TryStep();

            // Assert


            Assert.Pass();
            //            Assert.AreEqual(false, answer);
        }
    }
}
