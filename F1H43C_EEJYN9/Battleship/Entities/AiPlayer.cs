using F1H43C_EEJYN9.Entities.Interfaces;

namespace F1H43C_EEJYN9.Entities;

public class AiPlayer : Player
{
    public AiPlayer(string name) : base(name) {}

    public override void Turn(IPlayer enemy)
    {
        bool fired = false;
        while (!fired)
        {
            Board.GenerateRandomAiPos();
            fired = enemy.Fire(Board.SelectedCell);
        }
        
        
    }

    public override void PlaceShips()
    {
        Console.Clear();
        Console.WriteLine("AI Hajók elhelyezése...");
        Thread.Sleep(1000);
        Console.Clear();
        Ship selectedShip = _ships[0];
        while (!IsAllShipPlaced())
        {
            Board.GenerateRandomAiPos(true);
            if (Board.PlaceShip(selectedShip))
            {
                selectedShip = SelectNextShip();
            }
        }
    }
}