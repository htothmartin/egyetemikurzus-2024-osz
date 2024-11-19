namespace F1H43C_EEJYN9;

public class Board
{
    public const int GridSize = 8;
    public readonly Cell[,] Grid;
    private Coordinate direction;
    public Coordinate selectedCell { get; set; }
    
    public Board()
    {
        Grid = new Cell[GridSize, GridSize];
        InitializeGrid();
        direction = new Coordinate(1, 0);
        selectedCell = new Coordinate(0, 0);
    }
    
    private void InitializeGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Grid[i, j] = new Cell(i, j, '~');
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
                } else if(selectedCell != null && selectedCell.Equals(new Coordinate(i, j)) && cursor)
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
        Coordinate possibleCell = selectedCell;
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

            possibleCell = new Coordinate(possibleCell.X + direction.X, possibleCell.Y + direction.Y);
        }

        return true;
    }

    public bool IsCellHit(Coordinate pos)
    {
        return Grid[pos.X, pos.Y].IsHit;
    }

    public bool PlaceShip(Ship ship)
    {
        if (!CheckShipPositionIsValid(ship)) return false;
        
        ship.Head = selectedCell;
        ship.Direction = direction;
        
        List<Coordinate> possibleCells = GetPossibleCells(ship);
        
        foreach (Coordinate cell in possibleCells)
        {
  
            Grid[cell.X, cell.Y].HasShip = true;
                
        }
        return true;
    }
    
    public void Rotate90Degrees()
    {
        int temp = direction.X;
        direction.X = direction.Y;
        direction.Y = -temp;
    }
    
    private List<Coordinate> GetPossibleCells(Ship ship)
    {
        var possibleCells = new List<Coordinate>();

        possibleCells.Add(selectedCell);

        for (int i = 0; i < ship.Length - 1; i++)
        {
            possibleCells.Add(new Coordinate(possibleCells[i].X + direction.X, possibleCells[i].Y + direction.Y));
        }

        return possibleCells;
    }
    
    public bool Fire()
    {
        if (Grid[selectedCell.X, selectedCell.Y].IsHit)
        {
            return false;
        }
        else
        {
            Grid[selectedCell.X, selectedCell.Y].IsHit = true;
            return true;
        }
    }

    public void GenerateRandomAIPos()
    {
        Random random = new Random();
        selectedCell = new Coordinate(random.Next(0, GridSize), random.Next(0, GridSize));
    }

    public void CheckShipIsSunk(Ship ship)
    {
        Coordinate temp = ship.Head ?? new Coordinate(0, 0);
        for (int i = 0; i < ship.Length ; i++)
        {
            if (!Grid[temp.X + i * ship.Direction.X, temp.Y + i * ship.Direction.Y].IsHit)
            {
                return;
            }
         
        }
        ship.IsSunk = true;
        
    }
    
    public void MoveSelection(ConsoleKey key)
    {
        Coordinate newSelection = selectedCell;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (selectedCell.X > 0)
                    newSelection.X--;
                break;
            case ConsoleKey.DownArrow:
                if (selectedCell.X < GridSize - 1)
                    newSelection.X++;
                break;
            case ConsoleKey.LeftArrow:
                if (selectedCell.Y > 0)
                    newSelection.Y--;
                break;
            case ConsoleKey.RightArrow:
                if (selectedCell.Y < GridSize - 1)
                    newSelection.Y++;
                break;
        }
        selectedCell = newSelection;
    }
}