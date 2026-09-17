record TileHolder(int Id, string Color, int NextId, int SpecialNExtId);


class Tile
{
	public string Color { get; set; }
	public int Id { get; set; }
	public int NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public bool Visible = true;
	public Tile Next { get; set; }

	public Tile(int y, int x)
	{
		X = x;
		Y = y;
		//Color = color;
		Next = null;
	}
	public void DisplayTile()
	{
		Console.Write("x ");
	}
}