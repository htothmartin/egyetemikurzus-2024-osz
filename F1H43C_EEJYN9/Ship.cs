namespace F1H43C_EEJYN9
{
    public class Ship
    {
        public Coordinate Head { get; private set; }
        public Coordinate Direction { get; private set; }
        private int Length { get; }

        public Ship(Coordinate head, Coordinate direction, int length)
        {
            Head = head;
            Direction = direction;
            Length = length;
        }
    }
}
