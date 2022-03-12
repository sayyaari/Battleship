namespace Battleship.Model
{
    public struct BoardDimension
    {
        public BoardDimension(int size):this(size, size)
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
    }
}