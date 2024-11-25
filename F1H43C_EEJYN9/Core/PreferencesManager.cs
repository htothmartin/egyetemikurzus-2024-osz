using F1H43C_EEJYN9.Entities.Interfaces;
namespace F1H43C_EEJYN9.Core;

public class PreferencesManager
{
    private static PreferencesManager? instance;
    private readonly IPreferenceManager _preferenceManager;
    private readonly Dictionary<string, Action> _preferenceCommands;

    public static PreferencesManager Instance
    {
        get
        {
            instance ??= new PreferencesManager();
            return instance;
        }
    }

    private PreferencesManager()
    {
        _preferenceManager = new PreferenceManager();
        _preferenceCommands = new Dictionary<string, Action>
        {
            { "1", ModifyCharacters },
            { "2", ModifyColors },
            { "3", () => Console.WriteLine("Kilépés...") }
        };
    }

    public void InitializePreferences(string username)
    {
        string preferencesPath = Path.Combine("preferences", $"{username}_preferences.json");
        if (!File.Exists(preferencesPath))
        {
            var defaultPreferences = new GamePreferences();
            _preferenceManager.SavePreferences(username, defaultPreferences);
        }
    }

    private void ModifyCharacters()
    {
        var preferences = _preferenceManager.LoadPreferences(UserManager.Instance.CurrentUser.Name);
        bool modified = false;

        Console.Clear();
        Console.WriteLine("Karakter beállítások módosítása:");
        Console.WriteLine($"1. Hajó karakter: {preferences.ShipCharacter}");
        Console.WriteLine($"2. Eltalált hajó karakter: {preferences.HitShipCharacter}");
        Console.WriteLine($"3. Elsüllyedt hajó karakter: {preferences.SunkShipCharacter}");
        Console.WriteLine($"4. Víz karakter: {preferences.WaterCharacter}");
        Console.WriteLine($"5. Céltévesztés karakter: {preferences.MissedShotCharacter}");
        Console.WriteLine("6. Vissza");

        Console.Write("\nVálassz egy opciót (1-6): ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int choice) && choice >= 1 && choice <= 5)
        {
            Console.Write("Add meg az új karaktert: ");
            var charInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(charInput))
            {
                char newChar = charInput[0];
                switch (choice)
                {
                    case 1: preferences.ShipCharacter = newChar; break;
                    case 2: preferences.HitShipCharacter = newChar; break;
                    case 3: preferences.SunkShipCharacter = newChar; break;
                    case 4: preferences.WaterCharacter = newChar; break;
                    case 5: preferences.MissedShotCharacter = newChar; break;
                }
                modified = true;
            }
        }

        if (modified)
        {
            _preferenceManager.SavePreferences(UserManager.Instance.CurrentUser.Name, preferences);
            Console.WriteLine("Beállítások mentve!");
        }
        Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
        Console.ReadKey();
    }

    private void ModifyColors()
    {
        var preferences = _preferenceManager.LoadPreferences(UserManager.Instance.CurrentUser.Name);
        var availableColors = _preferenceManager.GetAvailableColors();
        bool modified = false;

        Console.Clear();
        Console.WriteLine("Szín beállítások módosítása:");
        Console.WriteLine($"1. Hajó színe: {preferences.ShipColor}");
        Console.WriteLine($"2. Eltalált hajó színe: {preferences.HitShipColor}");
        Console.WriteLine($"3. Elsüllyedt hajó színe: {preferences.SunkShipColor}");
        Console.WriteLine($"4. Víz színe: {preferences.WaterColor}");
        Console.WriteLine($"5. Céltévesztés színe: {preferences.MissedShotColor}");
        Console.WriteLine("6. Vissza");

        Console.Write("\nVálassz egy opciót (1-6): ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int choice) && choice >= 1 && choice <= 5)
        {
            Console.WriteLine("\nElérhető színek:");
            for (int i = 0; i < availableColors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableColors[i]}");
            }

            Console.Write($"\nVálassz egy színt (1-{availableColors.Count}): ");
            if (int.TryParse(Console.ReadLine(), out int colorChoice) && colorChoice >= 1 && colorChoice <= availableColors.Count)
            {
                var selectedColor = availableColors[colorChoice - 1];
                switch (choice)
                {
                    case 1: preferences.ShipColor = selectedColor; break;
                    case 2: preferences.HitShipColor = selectedColor; break;
                    case 3: preferences.SunkShipColor = selectedColor; break;
                    case 4: preferences.WaterColor = selectedColor; break;
                    case 5: preferences.MissedShotColor = selectedColor; break;
                }
                modified = true;
            }
        }

        if (modified)
        {
            _preferenceManager.SavePreferences(UserManager.Instance.CurrentUser.Name, preferences);
            Console.WriteLine("Beállítások mentve!");
        }
        Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
        Console.ReadKey();
    }

    public void ShowPreferencesMenu()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Válassz egy lehetőséget a menüből:");
            Console.WriteLine("1. Karakterek módosítása");
            Console.WriteLine("2. Színek módosítása");
            Console.WriteLine("3. Vissza a főmenübe");

            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && _preferenceCommands.ContainsKey(input))
            {
                _preferenceCommands[input].Invoke();
                if (input == "3") break;
            }
            else
            {
                Console.WriteLine("?? Érvénytelen parancs, próbáld újra.");
            }
            Console.Clear();
        }
    }
    
    public IPreferenceManager GetPreferenceManager()
    {
        return _preferenceManager;
    }
}