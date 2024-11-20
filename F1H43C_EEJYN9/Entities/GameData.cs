namespace F1H43C_EEJYN9.Entities;

public record GameData
{
    public  DateTime Date { get; init; }
    public string Winner { get; init; }

    public GameData(DateTime date, string winner)
    {
        Date = date;
        Winner = winner;
    }
}