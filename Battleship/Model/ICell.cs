namespace Battleship.Model
{
    public interface ICell
    {
        bool HasHit { get; }
        bool IsOccupied { get; }
        Ship? OccupiedBy { get; }
        Position Position { get; init; }

        AttackResult Attack();
        bool TryOccupy(Ship ship);
    }
}