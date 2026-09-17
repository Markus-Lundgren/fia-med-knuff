record TileHolder(int Id, string Color, int NextId, int SpecialNExtId);


class Tile
{
	public string Color = "NONE";
	public int Id { get; set; }
	public int NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public bool Visible = true;
	public Tile Next { get; set; }

	public Tile(int y, int x, string color = "NONE")
	{
		X = x;
		Y = y;
		//Color = color;
		Color = color;
		Next = null;
	}
	public void DisplayTile()
	{
		if (Color != "NONE") MColoredText.GetColoredText(Color, "▢ ");
		else Console.Write("▢ ");
	}
}