using Battleship.Helpers;

namespace Battleship.Model
{
    public struct BoardDimension
    {

        public BoardDimension(int size) : this(size, size)
        {
        }

        public BoardDimension(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }

        public static implicit operator BoardDimension(int size)
        {
            return new BoardDimension(size);
        }
        public override string ToString()
        {
            return $"width={Width} height={Height}";
        }

        public static BoardDimension CreateFrom<T>(T[,] array)
        {
            array.ThrowIfNull();
            return new BoardDimension(array.GetLength(0), array.GetLength(1));
        }

    }
}