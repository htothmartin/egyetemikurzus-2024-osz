namespace F1H43C_EEJYN9.Entities;

public class Board
{
    private const int GridSize = 8;
    public readonly Cell[,] Grid ;
    private Coordinate _direction;
    public Coordinate SelectedCell { get; private set; }
    
    public Board()
    {
        Grid = new Cell[GridSize, GridSize];
        InitializeGrid();
        _direction = new Coordinate(1, 0);
        SelectedCell = new Coordinate(0, 0);
    }
    
    private void InitializeGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Grid[i, j] = new Cell('~');
            }
        }
    }


    public void RenderGrid(Ship ship, bool shipPreview = false, bool cursor = true)
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<Coordinate> possibleCells =  shipPreview ? GetPossibleCells(ship) : new List<Coordinate>();
        
        Console.WriteLine("╔" + new string('═', GridSize * 2) + "╗");

        for (int i = 0; i < GridSize; i++)
        {
            Console.Write("║");
            for (int j = 0; j < GridSize; j++)
            {
                if (Grid[i, j].HasShip && !Grid[i,j].IsHit)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (shipPreview && possibleCells.Contains(new Coordinate(i, j)))
                {
                    Console.ForegroundColor = CheckShipPositionIsValid(ship) ? ConsoleColor.Green : ConsoleColor.Red;
                } else if(SelectedCell.Equals(new Coordinate(i, j)) && cursor)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else if (Grid[i, j].HasShip && Grid[i, j].IsHit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ResetColor();
                }
                
                char symbol = Grid[i, j].Symbol;
                if (Grid[i, j].HasShip)
                {
                    symbol = '■';
                } else if (Grid[i, j].IsHit)
                {
                    symbol = 'O';
                }
                

                Console.Write(symbol + " ");
                Console.ResetColor();
            }
            Console.WriteLine("║");
        }

        Console.WriteLine("╚" + new string('═', GridSize * 2) + "╝");
    }

    public void RenderGridWithEnemyPos(Coordinate pos)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("╔" + new string('═', GridSize * 2) + "╗");

        for (int i = 0; i < GridSize; i++)
        {
            Console.Write("║");
            for (int j = 0; j < GridSize; j++)
            {
                if (pos.X == i && pos.Y == j)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (Grid[i, j].HasShip && Grid[i,j].IsHit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ResetColor();
                }
                
                char symbol = Grid[i, j].Symbol;
                if (Grid[i, j].HasShip && Grid[i, j].IsHit)
                {
                    symbol = 'X';
                } else if (Grid[i, j].IsHit)
                {
                    symbol = 'O';
                }

                Console.Write(symbol + " ");
                Console.ResetColor();
            }
            Console.WriteLine("║");
        }

        Console.WriteLine("╚" + new string('═', GridSize * 2) + "╝");
        
        
    }
    
    public bool CheckShipPositionIsValid(Ship ship)
    {
        Coordinate possibleCell = SelectedCell;
        for (int i = 0; i < ship.Length; i++)
        {
            if (possibleCell.X >= GridSize || possibleCell.X < 0)
            {
                return false;
            }
            else if (possibleCell.Y >= GridSize || possibleCell.Y < 0)
            {
                return false;
            }

            if (Grid[possibleCell.X, possibleCell.Y].HasShip)
            {
                return false;
            }

            possibleCell = new Coordinate(possibleCell.X + _direction.X, possibleCell.Y + _direction.Y);
        }

        return true;
    }

    public bool PlaceShip(Ship ship)
    {
        if (!CheckShipPositionIsValid(ship)) return false;
        
        ship.Head = SelectedCell;
        ship.Direction = _direction;
        
        List<Coordinate> possibleCells = GetPossibleCells(ship);
        
        foreach (Coordinate cell in possibleCells)
        {
  
            Grid[cell.X, cell.Y].HasShip = true;
                
        }
        return true;
    }
    
    public void Rotate90Degrees()
    {
        int temp = _direction.X;
        _direction.X = _direction.Y;
        _direction.Y = -temp;
    }
    
    private List<Coordinate> GetPossibleCells(Ship ship)
    {
        var possibleCells = new List<Coordinate> { SelectedCell };

        for (int i = 0; i < ship.Length - 1; i++)
        {
            possibleCells.Add(new Coordinate(possibleCells[i].X + _direction.X, possibleCells[i].Y + _direction.Y));
        }

        return possibleCells;
    }
    
    public bool Hit(Coordinate target)
    {
        if (!Grid[target.X, target.Y].IsHit)
        {
            Grid[target.X, target.Y].IsHit = true;
            return true;
        }

        return false;
    }

    public void GenerateRandomAiPos(bool rotate = false)
    {
        Random random = new Random();
        if (rotate && random.Next(0, 2) == 0)
        {
            Rotate90Degrees();
        }
        SelectedCell = new Coordinate(random.Next(0, GridSize), random.Next(0, GridSize));
    }
    
    public void MoveSelection(ConsoleKey key)
    {
        Coordinate newSelection = SelectedCell;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (SelectedCell.X > 0)
                    newSelection.X--;
                break;
            case ConsoleKey.DownArrow:
                if (SelectedCell.X < GridSize - 1)
                    newSelection.X++;
                break;
            case ConsoleKey.LeftArrow:
                if (SelectedCell.Y > 0)
                    newSelection.Y--;
                break;
            case ConsoleKey.RightArrow:
                if (SelectedCell.Y < GridSize - 1)
                    newSelection.Y++;
                break;
        }
        SelectedCell = newSelection;
    }
}