namespace Battleship.Model.Interfaces
{
    public interface IBoard
    {
        bool HasLost { get; }
        bool AddShip(Ship ship);
        AttackResult TakeAttack(int x, int y);
    }
}