using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1H43C_EEJYN9
{
    public class GameLogic
    {
        private const int GridSize = 8;
        private readonly Cell[,] grid = new Cell[GridSize, GridSize];
        private Coordinate selectedCell = new Coordinate(0,0);
        //private bool[,] lockedCells = new bool[GridSize, GridSize];
        
        private Coordinate Direction = new Coordinate(1, 0);
        private List<int> ShipSizes;
        private int SelectedShipLength;
        private List<Ship> Ships = new List<Ship>();



        public GameLogic()
        {
            ShipSizes = new List<int> { 1, 2, 3, 4 };
            SelectedShipLength = ShipSizes[0];
            InitializeGrid();
        }



        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    grid[i, j] = new Cell(i, j, '~');
                }
            }
        }

        public void RenderGrid()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Coordinate> possibleCells = GetPossibleCells();

            Console.WriteLine("╔" + new string('═', GridSize * 2) + "╗");

            for (int i = 0; i < GridSize; i++)
            {
                Console.Write("║");
                for (int j = 0; j < GridSize; j++)
                {
                    if (grid[i, j].HasShip)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (possibleCells.Contains(new Coordinate(i, j)))
                    {
                        if (checkShipPositionIsValid())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.Write(grid[i, j].Symbol + " ");
                    Console.ResetColor();
                }
                Console.WriteLine("║");
            }

            Console.WriteLine("╚" + new string('═', GridSize * 2) + "╝");

            if (ShipSizes.Count == 0)
            {
                Console.WriteLine("Minden hajó el lett helyezve.");
                
            } else
            {
                Console.WriteLine($"Jelenlegi hajó hossza: {SelectedShipLength}");
                Console.WriteLine("Nyomd meg az 1-4 gombokat a hajó hosszának kiválasztásához.");            
            }
        }

        public void Rotate90Degrees()
        {
            int temp = Direction.X;
            Direction.X = Direction.Y;
            Direction.Y = -temp;
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

        public bool checkShipPositionIsValid()
        {
            Coordinate possibleCell = selectedCell;
            for (int i = 0; i < SelectedShipLength - 1; i++)
            {
                possibleCell = new Coordinate(possibleCell.X + Direction.X, possibleCell.Y + Direction.Y);

                if (possibleCell.X >= GridSize || possibleCell.X < 0)
                {
                    return false;
                }
                else if (possibleCell.Y >= GridSize || possibleCell.Y < 0)
                {
                    return false;
                }
                if (grid[possibleCell.X, possibleCell.Y].HasShip)
                {
                    return false;
                }
            }
          
            return true;
        }

        public List<Coordinate> GetPossibleCells()
        {
            var possibleCells = new List<Coordinate>();

            possibleCells.Add(selectedCell);

            for (int i = 0; i < SelectedShipLength - 1; i++)
            {
                possibleCells.Add(new Coordinate(possibleCells[i].X + Direction.X, possibleCells[i].Y + Direction.Y));
            }

            return possibleCells;


        }

        public void ChangeShipSize(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    if (ShipSizes.Contains(1))
                    {
                        SelectedShipLength = 1;
                    }
                    break;
                case ConsoleKey.D2:
                    if (ShipSizes.Contains(2))
                    {
                        SelectedShipLength = 2;
                    }
                    break;
                case ConsoleKey.D3:
                    if (ShipSizes.Contains(3))
                    {
                        SelectedShipLength = 3;
                    }
                    break;
                case ConsoleKey.D4:
                    if (ShipSizes.Contains(4))
                    {
                        SelectedShipLength = 4;
                    }
                    break;
            }
        }

        public void PlaceShip()
        {
            if (!checkShipPositionIsValid()) return;

            List<Coordinate> possibleCells = GetPossibleCells();

            Ships.Add(new Ship(selectedCell, Direction, SelectedShipLength));

            ShipSizes.Remove(SelectedShipLength);
            if (ShipSizes.Count > 0)
            {
                SelectedShipLength = ShipSizes[0];
            }
            else
            {
                Console.WriteLine("Minden hajó el lett helyezve.");
            }
            
            foreach (Coordinate cell in possibleCells)
            {
  
                grid[cell.X, cell.Y].HasShip = true;
                grid[cell.X, cell.Y].Symbol = '■';
                
            }
        }
    }
}
