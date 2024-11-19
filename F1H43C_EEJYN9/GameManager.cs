using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1H43C_EEJYN9
{
    public class GameManager
    {
        private static GameManager? _instance;
        private Player HumanPlayer;
        private Player AIPlayer;

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

        public void Start(string playerName)
        {
            HumanPlayer = new Player(playerName);
            AIPlayer = new Player("AI");
            HumanPlayer.PlaceShips();
            AIPlayer.PlaceAIShips();
            
            Console.WriteLine("Játék indítása...");
            bool IsPlayer1Turn = true;
            Thread.Sleep(1000);

            while (IsGameEnded())
            {
                if (IsPlayer1Turn)
                {
                    Turn(HumanPlayer, AIPlayer);
                }
                else
                {
                    AITurn(AIPlayer, HumanPlayer);
                }
                IsPlayer1Turn = !IsPlayer1Turn;
            }
            Console.Clear();
            Console.WriteLine($"Nyertes: {GetWinner()}");
        }

      

        public void CloseAllFiles()
        {
            Console.WriteLine("Minden fájl bezárva.");
            ClearConsole();
            // Itt kerülnek bezárásra a megnyitott fájlok, ha vannak
        }

        private string GetWinner()
        {
            if (HumanPlayer.IsALlShipIsSunk() && AIPlayer.IsALlShipIsSunk())
            {
                return "Tie";
            }
            
            return HumanPlayer.IsALlShipIsSunk() ? AIPlayer.Name : HumanPlayer.Name;
        }

        private void Turn(Player current, Player enemy)
        {
            bool isFired = false;
            while (!isFired)
            {
                
                current.Board.RenderGrid(null, false, false);
                enemy.Board.RenderGridWithEnemyPos(current.Board.selectedCell);
 
                isFired = current.MoveCrosshair();
                if (isFired)
                {
                    isFired = enemy.Fire(current.Board.selectedCell);
                }
            }
           
        }

        private void AITurn(Player current, Player enemy)
        {
            bool isFired = false;
            
            while (!isFired)
            {
                current.Board.GenerateRandomAIPos();
                isFired = enemy.Fire(current.Board.selectedCell);
            }
        }

        private bool IsGameEnded()
        {
            return !HumanPlayer.IsALlShipIsSunk() && !AIPlayer.IsALlShipIsSunk();
        }

        

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
