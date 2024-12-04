using F1H43C_EEJYN9.Entities.Interfaces;

namespace F1H43C_EEJYN9.Entities;

public class HumanPlayer : Player
{
    public HumanPlayer(string name) : base(name) {}

    public override void PlaceShips()
    {
        Console.WriteLine("Hajók elhelyezése...");
        Thread.Sleep(1000);
        Console.Clear();
        Ship selectedShip = _ships[0];

        while (!IsAllShipPlaced())
        {
            Board.RenderGrid(selectedShip, true);
            Console.WriteLine($"Kérlek helyezd el a {selectedShip.ShipName}-t.");
            Console.WriteLine("A forgatáshoz nyomd meg az 'r' gombot.");
            Console.WriteLine("A pozíció véglegesítéséhez nyomd meg a szóközt.");
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
        Board.RenderGrid(null);
        Console.WriteLine("Sikeresen elhelyeztél minden hajót!");
        Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        Console.ReadKey(true);
    }
    
    public override void Turn(IPlayer enemy)
    {
        bool fired = false;
        while (!fired)
        {
            Board.RenderGrid(null, false, false);
            enemy.Board.RenderGridWithEnemyPos(Board.SelectedCell);
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    fired = enemy.Fire(Board.SelectedCell);
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
}