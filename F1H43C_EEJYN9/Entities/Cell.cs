
namespace F1H43C_EEJYN9.Entities
{
    public class Cell
    {
        public bool HasShip { get; set; }
        public bool IsHit { get; set; }
        public char Symbol { get; set; }


        public Cell(char symbol)
        {
            IsHit = false;
            HasShip = false;
            Symbol = symbol;
        }
    }
}
