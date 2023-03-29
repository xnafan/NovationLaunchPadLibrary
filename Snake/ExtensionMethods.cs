using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public static class ExtensionMethods
    {
        public static Direction GetOppositeDirection(this Direction direction)
        {
            if (direction == Direction.Up) { return Direction.Down; }
            if (direction == Direction.Down) { return Direction.Up; }
            if (direction == Direction.Left) { return Direction.Right; }
            if (direction == Direction.Right) { return Direction.Left; }
            return Direction.None;
        }
    }
}
