
namespace F1H43C_EEJYN9.Entities
{
    public class Ship
    {
        public Coordinate? Head { get;  set; }
        public Coordinate Direction { get;  set; }
        public int Length { get; }
        
        public bool IsSunk { get; private set; }
        
        public string ShipName { get;  set; }

        public Ship(string name ,int length)
        {
            Length = length;
            Head = null;
            ShipName = name;
            IsSunk = false;
        }

        public void CheckShipIsSunk(Board board)
        {
            if(Head == null) return;
            
            for (int i = 0; i < Length ; i++)
            {
                if (!board.Grid[Head.Value.X + i * Direction.X, Head.Value.Y + i * Direction.Y].IsHit)
                {
                    return;
                }
            }
            IsSunk = true;
        }
    }
}
