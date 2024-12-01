namespace F1H43C_EEJYN9.Core;

public class GamePreferences
{
    public char ShipCharacter { get; set; } = 'S';
    public char HitShipCharacter { get; set; } = 'H';
    public char SunkShipCharacter { get; set; } = 'X';
    public char WaterCharacter { get; set; } = '~';
    public char MissedShotCharacter { get; set; } = 'O';
    
    public ConsoleColor ShipColor { get; set; } = ConsoleColor.White;
    public ConsoleColor HitShipColor { get; set; } = ConsoleColor.Red;
    public ConsoleColor SunkShipColor { get; set; } = ConsoleColor.DarkRed;
    public ConsoleColor WaterColor { get; set; } = ConsoleColor.Blue;
    public ConsoleColor MissedShotColor { get; set; } = ConsoleColor.Gray;
}