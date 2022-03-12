namespace Battleship.Model
{
    public class BoardGrid
    {
        public BoardGrid(ICell[,] cells)
        {
            Cells = cells;
            Dimension = new(cells.GetLength(0), cells.GetLength(1));
        }

        public BoardDimension Dimension { get; init; }
        
        public ICell[,] Cells { get; init; }

    }
}
