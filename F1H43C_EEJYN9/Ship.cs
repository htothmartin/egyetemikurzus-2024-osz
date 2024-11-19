namespace F1H43C_EEJYN9
{
    public class Ship
    {
        public Coordinate? Head { get;  set; }
        public Coordinate Direction { get;  set; }
        public int Length { get; }
        
        public bool IsSunk { get; set; }
        
        public string ShipName { get;  set; }

        public Ship(string name ,int length)
        {
            Length = length;
            Head = null;
            ShipName = name;
            IsSunk = false;
        }
    }
}
