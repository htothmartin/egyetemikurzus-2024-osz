using System;
using System.Collections.Generic;

using F1H43C_EEJYN9;

public class Program
{
    public static void MainMenu()
    {
        Console.WriteLine("Sikeres bejelentkezés! Válassz egy lehetőséget a menüből:");
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
            }
            else
            {
                Console.WriteLine("?? Érvénytelen parancs, próbáld újra.");
            }
        }
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
        Console.WriteLine("Játék indítása...");
        Thread.Sleep(1000);
        GameManager.Instance.ClearConsole();
        GameLogic game = new GameLogic();

        while (true)
        {
            game.RenderGrid();
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Spacebar:
                    game.LockCell();
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    game.MoveSelection(key);
                    break;
            }
        }

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
        GameManager.Instance.Start();
    }

    private static void ExitApplication()
    {
        GameManager.Instance.CloseAllFiles();
        Console.WriteLine("Kilépés...");
        Thread.Sleep(1000);
        Environment.Exit(0);
    }

    public static void Main(string[] args)
    {
        GameManager.Instance.Start();
    }
}
