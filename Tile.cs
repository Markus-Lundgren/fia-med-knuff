class Tile
{
	public string Color = "NONE";
	public string Id { get; set; }
	public string NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public string SpecialInfo = "false";
	public List<Piece> Pieces = new();

	public Tile(int y, int x, string id, string nextId, string color = "NONE", string special = "false") =>
	(Y, X, Id, NextId, Color, SpecialInfo) = (y, x, id, nextId, color, special);

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