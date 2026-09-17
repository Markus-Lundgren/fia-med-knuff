class Tile
{
	public string Color = "NONE";
	public string Id { get; set; }
	public string NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public bool Special = false;

	public List<Piece> Pieces = new();

	public Tile(int y, int x, string id, string nextId, string color = "NONE", bool special = false)
	{
		X = x;
		Y = y;
		Id = id;
		NextId = nextId;
		Color = color;
		Special = special;
	}
	public void DisplayTile()
	{
		if (Pieces.Count > 0)
		{
			MColoredText.GetColoredText(Pieces[0].Color, Pieces[0].PieceDisplay);
		}
		else if (Special) MColoredText.GetColoredText(Color, "▨ ");
		else if (Color != "NONE") MColoredText.GetColoredText(Color, "▢ ");
		else Console.Write("▢ ");
	}
}