using F1H43C_EEJYN9.Entities;
using F1H43C_EEJYN9.Entities.Interfaces;

namespace F1H43C_EEJYN9.Core
{
    public class GameManager
    {
        private static GameManager? _instance;
        private IPlayer HumanPlayer;
        private IPlayer AIPlayer;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public string Start(string playerName)
        {
            HumanPlayer = new HumanPlayer(playerName);
            AIPlayer = new AiPlayer("AI");
            HumanPlayer.PlaceShips();
            AIPlayer.PlaceShips();
            
            Console.WriteLine("Játék indítása...");
            bool isPlayer1Turn = true;
            Thread.Sleep(1000);

            while (IsGameEnded())
            {
                if (isPlayer1Turn)
                {
                    HumanPlayer.Turn(AIPlayer);
                }
                else
                {
                    AIPlayer.Turn(HumanPlayer);
                }
                isPlayer1Turn = !isPlayer1Turn;
            }
            Console.Clear();
            Console.WriteLine($"Nyertes: {GetWinner()}");
            return GetWinner();
        }
        
        private string GetWinner()
        {
            if (HumanPlayer.IsALlShipSunk() && AIPlayer.IsALlShipSunk())
            {
                return "Tie";
            }
            
            return HumanPlayer.IsALlShipSunk() ? AIPlayer.Name : HumanPlayer.Name;
        }

        private bool IsGameEnded()
        {
            return !HumanPlayer.IsALlShipSunk() && !AIPlayer.IsALlShipSunk();
        }
    }
}
