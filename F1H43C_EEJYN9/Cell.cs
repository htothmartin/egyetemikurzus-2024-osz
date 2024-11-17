
namespace F1H43C_EEJYN9
{
    public class Cell
    {
        public Coordinate coordinate { get; }
        public bool HasShip { get; set; }
        public bool IsHit { get; set; }
        public char Symbol { get; set; }


        public Cell(int row, int col, char symbol)
        {
            coordinate = new Coordinate(row, col);
            IsHit = false;
            HasShip = false;
            Symbol = symbol;
        }
    }
}
