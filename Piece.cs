class Piece
{
	public string? Color { get; set; }
	public int Id { get; set; }
	public string PieceDisplay = "■ ";
	public Coords Coords;
	public Piece(int id)
	{
		Id = id;
	}

	public void SetPosition(int x, int y)
	{
		Coords = new Coords(x, y);
	}

	public void DrawPiece(Board b)
	{
		switch (Color)
		{
			case "red":
				MColoredText.GetColoredText(ConsoleColor.Red, PieceDisplay);
				break;
			case "blue":
				MColoredText.GetColoredText(ConsoleColor.Blue, PieceDisplay);
				break;
			case "green":
				MColoredText.GetColoredText(ConsoleColor.Green, PieceDisplay);
				break;
			case "yellow":
				MColoredText.GetColoredText(ConsoleColor.Yellow, PieceDisplay);
				break;
		}
		//ANVÄND egen funktion
	}
}
