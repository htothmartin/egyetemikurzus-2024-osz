using System;

public class Cell
{
    public Coordinate coordinate { get; }
    public bool HasShip { get; set; }
    public bool IsHited { get; set; }
    public char Symbol { get; set; }


	public Cell(int row, int col, char symbol)
	{
        coordinate = new Coordinate(row, col);
        IsHited = false;
        HasShip = false;
        Symbol = symbol;
	}
}
