using F1H43C_EEJYN9.Entities.Interfaces;
using F1H43C_EEJYN9.Exceptions;

namespace F1H43C_EEJYN9.Entities;

public abstract class Player : IPlayer
{
    public string Name { get; }
    public Board Board { get; }
    protected readonly List<Ship> _ships;
    
    protected Player(string name)
    {
        Name = name;
        Board = new Board();
        _ships = new List<Ship>
        {
            new Ship("Romboló", 1),
            new Ship("Cirkáló", 2),
            new Ship("Csatahajó", 3),
            new Ship("Anya-hajó", 4)
        };
    }
    
    public abstract void PlaceShips();
    public abstract void Turn(IPlayer enemy);
    
    public bool Fire(Coordinate target)
    {
        try
        {
            Board.Hit(target);

            foreach (Ship ship in _ships)
            {
                if (!ship.IsSunk)
                {
                    ship.CheckShipIsSunk(Board);
                }
            }

        } catch (InvalidActionException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
            
        return true;
    }
    
    public bool IsALlShipSunk()
    {
        foreach (Ship ship in _ships)
        {
            if (!ship.IsSunk)
            {
                return false;
            }
        }
        return true;
    }
    
    public bool IsAllShipPlaced()
    {
        foreach (Ship ship in _ships)
        {
            if(ship.Head == null) return false;
        }
        return true;
    }
    
    protected Ship SelectNextShip()
    {
        foreach (Ship ship in _ships)
        {
            if (ship.Head == null) return ship;
        }
        return null;
    }


}