using F1H43C_EEJYN9.Core;
using F1H43C_EEJYN9.Entities;

public class Program
{
    private string _username;
    
    public static void MainMenu()
    {
        Console.WriteLine("Válassz egy lehetőséget a menüből:");
        Console.WriteLine("1. Játék indítása");
        Console.WriteLine("2. Preferenciák módosítása");
        Console.WriteLine("3. Statisztikak");
        Console.WriteLine("4. Kijelentkezés");
        Console.WriteLine("5. Kilépés");

        while (true)
        {
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && commands.ContainsKey(input))
            {
                commands[input].Invoke();
                break;
            }
            else
            {
                Console.WriteLine("?? Érvénytelen parancs, próbáld újra.");
            }
        }

        MainMenu();
    }

    private static readonly Dictionary<string, Action> commands = new()
    {
        {"1", StartGame},
        {"2", ModifyPreferences},
        {"3", Statistics},
        {"4", Logout},
        {"5", ExitApplication}
    };
    
    private static readonly Dictionary<string, Action> statisticCommands = new()
    {
        {"1", UserManager.Instance.UserStatistics},
        {"2", UserManager.Instance.GlobalStatistics},
    };

    private static void Statistics()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Válassz egy lehetőséget a menüből:");
            Console.WriteLine("1. Felhasználó statisztikák");
            Console.WriteLine("2. Globális statisztikák");
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && statisticCommands.ContainsKey(input))
            {
                statisticCommands[input].Invoke();
                break;
            }
            else
            {
                Console.WriteLine("?? Érvénytelen parancs, próbáld újra.");
            }
        }
        Console.Clear();
    }

    private static void StartGame()
    {
        string winner = GameManager.Instance.Start(UserManager.Instance.CurrentUser.Name);
        UserManager.Instance.CurrentUser.GamesPlayed.Add(new GameData(DateTime.Now, winner));
    }

    private static void ModifyPreferences()
    {
        Console.WriteLine("Preferenciák módosítása...");
        // Ide jön majd a preferenciák módosításának logikája
    }

    private static void Logout()
    {
        UserManager.Instance.CloseAllFiles();
        Console.WriteLine("Kijelentkezés...");
        Thread.Sleep(1000);
        // Bezárunk minden megnyitott fájlt és újraindítjuk a GameManager-t

    }

    private static void ExitApplication()
    {
        UserManager.Instance.CloseAllFiles();
        Console.WriteLine("Kilépés...");
        Thread.Sleep(1000);
        Environment.Exit(0);
    }

    private static void Login()
    {
        do
        {
            Console.Clear();

            if (UserManager.Instance.UserLogin())
            {
                // Console.WriteLine("GameManager elindítva...");
                Console.WriteLine("Sikeres bejelentkezés! ");
                MainMenu();
                // Visszatérünk a főmenübe
            }
            else
            {
                Console.WriteLine("Bejelentkezés nem sikerült! Nyomj 'q'-t a kilépéshez, vagy nyomj meg bármilyen más gombot a próbálkozáshoz.");
            }

            if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }

        } while (true);
    }

    public static void Main(string[] args)
    {
        Login();
    }
}
