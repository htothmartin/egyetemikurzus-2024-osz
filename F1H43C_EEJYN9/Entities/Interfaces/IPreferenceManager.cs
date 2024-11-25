using F1H43C_EEJYN9.Core;
namespace F1H43C_EEJYN9.Entities.Interfaces;

public interface IPreferenceManager
{
    void SavePreferences(string username, GamePreferences preferences);
    GamePreferences LoadPreferences(string username);
    List<ConsoleColor> GetAvailableColors();
}