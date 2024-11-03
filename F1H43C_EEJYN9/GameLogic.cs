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
        private readonly char[,] grid = new char[GridSize, GridSize];
        private (int Row, int Col) selectedCell = (0, 0);
        private bool[,] lockedCells = new bool[GridSize, GridSize];

        public GameLogic()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    grid[i, j] = '~';
                }
            }

            // Néhány 'x' karakter elhelyezése hajók szimbolizálására
            grid[2, 3] = 'x';
            grid[2, 4] = 'x';
            grid[5, 1] = 'x';
            grid[5, 2] = 'x';
        }

        public void RenderGrid()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔" + new string('═', GridSize * 2) + "╗");

            for (int i = 0; i < GridSize; i++)
            {
                Console.Write("║");
                for (int j = 0; j < GridSize; j++)
                {
                    if (lockedCells[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (selectedCell.Row == i && selectedCell.Col == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.Write(grid[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine("║");
            }

            Console.WriteLine("╚" + new string('═', GridSize * 2) + "╝");
        }

        public void MoveSelection(ConsoleKey key)
        {
            (int Row, int Col) newSelection = selectedCell;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedCell.Row > 0)
                        newSelection.Row--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedCell.Row < GridSize - 1)
                        newSelection.Row++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selectedCell.Col > 0)
                        newSelection.Col--;
                    break;
                case ConsoleKey.RightArrow:
                    if (selectedCell.Col < GridSize - 1)
                        newSelection.Col++;
                    break;
            }

            selectedCell = newSelection;
        }

        public void LockCell()
        {
            if (lockedCells[selectedCell.Row, selectedCell.Col])
            {
                Console.WriteLine("Hiba: Ez a mező már véglegesen kijelölve lett!");
            }
            else
            {
                lockedCells[selectedCell.Row, selectedCell.Col] = true;
                grid[selectedCell.Row, selectedCell.Col] = '■';
            }
        }
    }
}
