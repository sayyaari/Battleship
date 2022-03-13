using Battleship.Model;
using System;

namespace Battleship.Tests.Utils
{
    internal class Generator
    {
        public static Position GeneratePosition(int xMin = 0, int xMax = int.MaxValue, int yMin = 0, int yMax = int.MaxValue)
        {
            return new Position(GenerateNumber(xMin, xMax), GenerateNumber(yMin, yMax));
        }
        public static int GenerateNumber(int min = 0, int max = int.MaxValue)
        {
            Random random = new();
            return random.Next(min, max);
        }
    }
}
