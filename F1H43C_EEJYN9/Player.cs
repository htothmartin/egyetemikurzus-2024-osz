using System.Reflection.Metadata.Ecma335;

namespace F1H43C_EEJYN9;

public class Player
{
    public string Name { get; }
    public Board Board { get; }
    public List<Ship> Ships { get; }
    


    public Player(string name)
    {
        Name = name;
        Board = new Board();
        Ships = new List<Ship>();
        

        Ships.Add(new Ship("Rombolo", 1));
        Ships.Add(new Ship("Cirkalo", 2));
        Ships.Add(new Ship("Csatahajo", 3));
        Ships.Add(new Ship("Anya-hajo", 4));
    }

    public void PlaceShips()
    {
        Console.WriteLine("Hajok lehelyezese...");
        Thread.Sleep(1000);
        Console.Clear();
        Ship selectedShip = Ships[0];
        
        while (!IsAlShipPlaced())
        {
            Board.RenderGrid(selectedShip, true);
            Console.WriteLine($"Kerlek helyezd el a {selectedShip.ShipName}-t");
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Spacebar:
                    if (Board.PlaceShip(selectedShip))
                    {
                        selectedShip = SelectNextShip();
                    }
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    Board.MoveSelection(key);
                    break;
                case ConsoleKey.R:
                    Board.Rotate90Degrees();
                    break;
                
                    
            }
            Console.Clear();
        }
        Board.RenderGrid(null, false);
        Console.WriteLine("Sikeresen elhelyeztel minden hajot!");
        Console.WriteLine("Nyomd egy gombot a folytatashoz.");
        Console.ReadKey(true);
    }

    public bool Fire(Coordinate target)
    {
        if (!Board.IsCellHit(target))
        {
            Board.Grid[target.X, target.Y].IsHit = true;
            foreach (Ship ship in Ships)
            {
                if (!ship.IsSunk)
                {
                    Board.CheckShipIsSunk(ship);
                }
            }
            
            return true;
        }
        return false;
    }

    public bool MoveCrosshair()
    {
        ConsoleKey key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.Spacebar:
                return true;
            case ConsoleKey.UpArrow:
            case ConsoleKey.DownArrow:
            case ConsoleKey.LeftArrow:
            case ConsoleKey.RightArrow:
                Board.MoveSelection(key);
                break;
        }

        return false;
    }

    public void PlaceAIShips()
    {
        Console.Clear();
        Ship selectedShip = Ships[0];
        while (!IsAlShipPlaced())
        {
            Board.GenerateRandomAIPos();
            if (Board.PlaceShip(selectedShip))
            {
                selectedShip = SelectNextShip();
            }
        }
        //Board.RenderGrid(null, false);
        //Console.ReadLine();
    }

    public void Turn()
    {
        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Spacebar:
                    if (Board.Fire())
                    {
                        return;
                    }
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    Board.MoveSelection(key);
                    break;
                case ConsoleKey.R:
                    Board.Rotate90Degrees();
                    break;
                
                    
            }
        }
    }


    private bool IsAlShipPlaced()
    {
        foreach (Ship ship in Ships)
        {
            if(ship.Head == null) return false;
        }
        return true;
    }

    private Ship SelectNextShip()
    {
        foreach (Ship ship in Ships)
        {
            if (ship.Head == null) return ship;
        }
        return null;
    }

    public bool IsALlShipIsSunk()
    {
        foreach (Ship ship in Ships)
        {
            if (!ship.IsSunk)
            {
                return false;
            }
        }

        return true;
    }
    





}