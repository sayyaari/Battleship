# Battleship
Battleship State Tracker

## How to run/build the application
The application was written in .NET 6, so please ensure you have it installed. 

## Functionality
The functionality includes:

* Create a board
* Add a battleship to the board
* Take an "attack" at a given position, and report back whether the attack resulted in a
hit or a miss
* Return whether the player has lost the game yet (i.e. all battleships are sunk)


## Solution structure

The solution consists of the following projects:
1) Battleship 
    * This project includes the logic, interfaces and model 

2) Battleship.Tests
    * This project includes all the unit tests 

2) Battleship.SimpleApp
    * This project is a simple console application to demonstrate how to use Battleship State Tracker for  the requested functionality

## How to use it
To interact with state tracker at highest level only IBoard interface is required. The IBoard instance can be created via a factory called IBoardFactory.
To register the services and interface an extension method on ServiceColletion has been provided which should be called before using factory. 

* Register the services 
```csharp
services.AddBattleshipServices();
```

* Get an instance of the IBoardFactory
```csharp
provider.GetRequiredService<IBoardFactory>();
```

* Create a board with a specific size
```csharp
IBoard singlePlayerBoard = boardFactory.Create(10);
```

* Adding a ship to the board
```csharp
Ship ship = new(3, new Position(5, 5), Direction.Horizaontal);
singlePlayerBoard.AddShip(ship);
```

* Taking an attack at a position specified by x and y coordinate

```csharp
var attackResult = singlePlayerBoard.TakeAttack(5, 5);
```

* Return whether the player has lost the game yet 

```csharp
singlePlayerBoard.HasLost
```
## Design principles
Some basic principles which always have been considered during the implementation:

* Ensuring that any piece of code added to the project can be testable independently and can be mocked when testing other components.
* The classes should be easy to read and understand at first sight as much as possible and without burning them with too many responsibilities.

## Classes & interfaces overview

### Model main classes
*  **IBoard, Board** 
   The public face interface of the project for tracking the player state

*  **IBoardGrid, BoardGrid**
   Managing the board grid. Any access to grid cells and managing them is done through this class
   The grid origin coordinate is x = 0, y = 0.

*  **ICell, Cell**
   Represents any cell on the grid. Any operation cell level like attacking a cell should be done through this class and will affect the state of the cell.

*  **OccupiedArea**
   The area of grid (cells) occupied by a specific ship is represented and handled by this class

*  **Ship**
   Represents a ship of a specific size located at starting position. with a specific direction (Horizontal/Vertical)
   *In vertical direction it is always from starting position toward the top of the grid and in horizontal direction the area will be from starting position toward right of the grid.*

*  **Direction**
   An Enum with two Horizontal and Vertical members.

*  **BoardDimension**
   A struct to represent the dimension (with and height) of the board gird. 

*  **ShipSize**
   A struct to represent the length of the ship


### Factories
*  **IBoardFactory, BoardFactory**
   To create a board

*  **IBoardGridFactory, BoardGridFactory**
   To create and initialize the BoardGrid

*  **ICellFactory, CellFactory**
   To create and initialize a cells of the board grid

### Exceptions
*  **OutOfRangePosition**
   Represents a Position is not inside the board grid

*  **ShipeNotFittedInBoard**
   To be thrown when to inform that a Ship cannot be added to the board

### Validators
*  **IPositionValidator, PositionValidator**
   To validate whether a specific position is inside a board or not

### Services
*  **IPositionGenerator, PositionGenerator**
   Generates set of positions of an area in the board grid which a ship is supposed to sit there

