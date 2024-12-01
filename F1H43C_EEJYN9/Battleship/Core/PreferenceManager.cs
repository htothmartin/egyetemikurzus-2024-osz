using F1H43C_EEJYN9.Entities.Interfaces;
using System.Text.Json;

namespace F1H43C_EEJYN9.Core;

public class PreferenceManager : IPreferenceManager
{
    private readonly string _preferencesDirectory;
    private readonly List<ConsoleColor> _availableColors;
    private readonly GamePreferences _defaultPreferences;

    public PreferenceManager(string preferencesDirectory = "preferences")
    {
        _preferencesDirectory = preferencesDirectory;
        Directory.CreateDirectory(_preferencesDirectory);
        
        _availableColors = Enum.GetValues(typeof(ConsoleColor))
            .Cast<ConsoleColor>()
            .ToList();

        _defaultPreferences = new GamePreferences();
    }

    public bool SavePreferences(string username, GamePreferences preferences)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        ValidateAndApplyDefaults(preferences);

        try
        {
            string filePath = GetPreferenceFilePath(username);
            string jsonString = JsonSerializer.Serialize(preferences, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _ = Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, jsonString);
            return true;
        }
        catch (IOException ioe)
        {
            Console.WriteLine($"IO Error saving preferences: {ioe.Message}");
            return false;
        }
        catch (JsonException je)
        {
            Console.WriteLine($"Serialization Error saving preferences: {je.Message}");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected Error saving preferences: {e.Message}");
            return false;
        }
    }

    public GamePreferences LoadPreferences(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        try
        {
            string filePath = GetPreferenceFilePath(username);
            if (!File.Exists(filePath))
                return new GamePreferences();

            string jsonString = File.ReadAllText(filePath);
            var preferences = JsonSerializer.Deserialize<GamePreferences>(jsonString);
            ValidateAndApplyDefaults(preferences);
            return preferences;
        }
        catch (IOException ioe)
        {
            Console.WriteLine($"Error loading preferences: {ioe.Message}");
            return new GamePreferences();
        }
        catch (JsonException je)
        {
            Console.WriteLine($"Error parsing preferences: {je.Message}");
            return new GamePreferences();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected Error saving preferences: {e.Message}");
       return new GamePreferences();
        }
    }

    public List<ConsoleColor> GetAvailableColors()
    {
        return _availableColors.ToList();
    }

    public void ValidateAndApplyDefaults(GamePreferences preferences)
    {

        if (char.IsWhiteSpace(preferences.ShipCharacter))
        {
            preferences.ShipCharacter = _defaultPreferences.ShipCharacter;
        }

        if (char.IsWhiteSpace(preferences.HitShipCharacter))
        {
            preferences.HitShipCharacter = _defaultPreferences.HitShipCharacter;
        }

        if (char.IsWhiteSpace(preferences.SunkShipCharacter))
        {
            preferences.SunkShipCharacter = _defaultPreferences.SunkShipCharacter;
        }

        if (char.IsWhiteSpace(preferences.WaterCharacter))
        {
            preferences.WaterCharacter = _defaultPreferences.WaterCharacter;
        }

        if (char.IsWhiteSpace(preferences.MissedShotCharacter))
        {
            preferences.MissedShotCharacter = _defaultPreferences.MissedShotCharacter;
        }

        if (!_availableColors.Contains(preferences.ShipColor))
        {
            preferences.ShipColor = _defaultPreferences.ShipColor;
        }

        if (!_availableColors.Contains(preferences.HitShipColor))
        {
            preferences.HitShipColor = _defaultPreferences.HitShipColor;
        }

        if (!_availableColors.Contains(preferences.SunkShipColor))
        {
            preferences.SunkShipColor = _defaultPreferences.SunkShipColor;
        }

        if (!_availableColors.Contains(preferences.WaterColor))
        {
            preferences.WaterColor = _defaultPreferences.WaterColor;
        }

        if (!_availableColors.Contains(preferences.MissedShotColor))
        {
            preferences.MissedShotColor = _defaultPreferences.MissedShotColor;
        }
    }

    private string GetPreferenceFilePath(string username)
    {
        return Path.Combine(_preferencesDirectory, $"{username}_preferences.json");
    }
}