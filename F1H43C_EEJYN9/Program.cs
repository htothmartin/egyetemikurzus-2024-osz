using System;
using System.Collections.Generic;

using F1H43C_EEJYN9;

public class Program
{
    private string _username;
    
    public static void MainMenu()
    {
        Console.WriteLine("Válassz egy lehetőséget a menüből:");
        Console.WriteLine("1. Játék indítása");
        Console.WriteLine("2. Preferenciák módosítása");
        Console.WriteLine("3. Kijelentkezés");
        Console.WriteLine("4. Kilépés");

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
        {"3", Logout},
        {"4", ExitApplication}
    };

    private static void StartGame()
    {
        GameManager.Instance.Start(UserManager.Instance.CurrentUser.Name);
    }

    private static void ModifyPreferences()
    {
        Console.WriteLine("Preferenciák módosítása...");
        // Ide jön majd a preferenciák módosításának logikája
    }

    private static void Logout()
    {
        Console.WriteLine("Kijelentkezés...");
        Thread.Sleep(1000);
        // Bezárunk minden megnyitott fájlt és újraindítjuk a GameManager-t
        GameManager.Instance.CloseAllFiles();
    }

    private static void ExitApplication()
    {
        GameManager.Instance.CloseAllFiles();
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
                Console.WriteLine("Login failed! Press 'q' to quit or press any other button to try again.");
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
