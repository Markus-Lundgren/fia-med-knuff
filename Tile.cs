class Tile
{
	public string Color = "NONE";
	public string Id { get; set; }
	public string NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }

	public string SpecialInfo = "false";
	public bool Special = false;
	public List<Piece> Pieces = new();

	public Tile(int y, int x, string id, string nextId, string color = "NONE", string special = "false")
	{
		X = x;
		Y = y;
		Id = id;
		NextId = nextId;
		Color = color;
		SpecialInfo = special;
	}

	public Tile() { }
	public void DisplayTile()
	{
		if (Pieces.Count > 0)
		{
			MColoredText.GetColoredText(Pieces[0].Color, Pieces[0].PieceDisplay);
		}
		else if (SpecialInfo == "Goal") MColoredText.GetColoredText(Color, "▨ ");
		else if (Color != "NONE") MColoredText.GetColoredText(Color, "▢ ");
		else Console.Write("▢ ");
	}
}