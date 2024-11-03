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

        public void Start()
        {

            do
            {
                ClearConsole();

                if (UserLogin())
                {
                    // Console.WriteLine("GameManager elindítva...");
                    Program.MainMenu();
                    // Visszatérünk a főmenübe
                }
                else
                {
                    Console.WriteLine("Login failed! Press 'q' to quit or press any other button to try again.");
                }

                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }

            } while (true);
            
        }

        public void CloseAllFiles()
        {
            Console.WriteLine("Minden fájl bezárva.");
            ClearConsole();
            // Itt kerülnek bezárásra a megnyitott fájlok, ha vannak
        }

        private bool UserLogin()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine().Trim();

            if (!UserValidator.IsValidUsername(username))
            {
                Console.WriteLine("Invalid username format. Must be at least 5 characters and contain only letters and numbers.");
                return false;
            }

            var user = UserRepository.Instance.GetOrCreateUser(username);
            ClearConsole();
            Console.WriteLine($"Welcome, {user.Name}!\n");
            return true;
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
