using System.Runtime.CompilerServices;
using Battleship.Helpers;

[assembly:InternalsVisibleTo("Battleship.Tests")]
namespace Battleship.Model
{
    public class OccupiedArea : IOccupiedArea
    {
        public OccupiedArea(Ship ship, IEnumerable<ICell> cells)
        {
            Ship = ship.ThrowIfNull();
            Cells = cells.ThrowIfNull();
            Cells.ToList().ForEach(cell => cell.TryOccupy(ship));
        }

        public Ship Ship { get; init; }
        public IEnumerable<ICell> Cells { get; init; }
        public bool HasSunkShip => Cells.All(cell => cell.HasHit);
    }
}
