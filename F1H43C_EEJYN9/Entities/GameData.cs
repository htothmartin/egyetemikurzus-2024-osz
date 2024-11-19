namespace F1H43C_EEJYN9.Entities;

public record GameData
{
    public DateTime Date { get; set; }
    public string Winner { get; set; }

    public GameData(DateTime date, string winner)
    {
        Date = date;
        Winner = winner;
    }
}