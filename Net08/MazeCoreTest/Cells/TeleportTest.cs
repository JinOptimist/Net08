using MazeCore.Cells;
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
            var wall = new Teleport(0, 0, null);

            // Act
//            var answer = wall.TryStep();

            // Assert
//            Assert.AreEqual(false, answer);
        }
    }
}
