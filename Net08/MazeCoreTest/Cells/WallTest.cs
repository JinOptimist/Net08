using MazeCore.Cells;
using NUnit.Framework;
using System;

namespace MazeCoreTest.Cells
{
    public class WallTest
    {
        [Test]
        public void TryStep_ReturnAlwaysFalse()
        {
            // Preparing
            var wall = new Wall(0, 0, null);

            // Act
            var answer = wall.TryStep();

            // Assert
            Assert.AreEqual(false, answer);
        }
    }
}
