using Battleship.Factories;
using Battleship.Model;
using Battleship.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battleship.SimpleApp
{

/*
 * The purpose this simple console application is to demonstrate how to register services and call the operations requested in the test:
 *  - Create Board
 *  - Add Ship 
 *  - Take an Attack
 *  - Report the status of the board whether the player lost or not
 */
    class Program
    {
        private static IBoard singlePlayerBoard;
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Demo");

            Console.WriteLine("Registering Battleship services...");
            ServiceCollection services = new();
            services.AddBattleshipServices();
            var provider = services.BuildServiceProvider();


            Console.WriteLine("Creating a single player board 10x10");
            var boardFactory = provider.GetRequiredService<IBoardFactory>();
            singlePlayerBoard = boardFactory.Create(10)!;



            Ship ship = new(3, new Position(5, 5), Direction.Horizaontal);
            //The ship should be located at (5,5) (6,5) (7,5) positions in the board grid
            AddShip(ship);


            
            Ship secondShip = new(5, new Position(6, 4), Direction.Vertical);
            //The second won't be added since it cannot occupy the position(6,5) occupied already by first ship
            AddShip(secondShip);
            


            AttackAt(5, 5);

            // A missed attack
            AttackAt(3, 5);

            ReportBoardStatus();

            AttackAt(6, 5);

            ReportBoardStatus();

            AttackAt(7, 5);

            ReportBoardStatus();

            Console.ReadKey();
        }

        private static void ReportBoardStatus()
        {
            Console.WriteLine($"Hast Lost: {singlePlayerBoard.HasLost}");
        }

        private static void AddShip(Ship ship)
        {
            Console.WriteLine($"Adding {ship} to the board....");
            bool shipAdded = singlePlayerBoard.AddShip(ship);
            if (shipAdded)
                Console.WriteLine($"{ship} Added to the board");
            else
                Console.WriteLine($"WOOPS. {ship} cannot not be added in the specified position and direction");
        }

        static void AttackAt(int x, int y)
        {
            var attackResult = singlePlayerBoard.TakeAttack(x, y);
            Console.WriteLine($"Attacking Result at x:{x}, y:{y}:{attackResult}");
        }
    }
}


