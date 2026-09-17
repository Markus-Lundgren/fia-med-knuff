class Tile
{
	public string Color { get; set; }
	public Tile Next { get; set; }

	public Tile(string color)
	{
		Color = color;
		Next = null;
	}

	//

	public void DisplayTile()
	{

	}
}