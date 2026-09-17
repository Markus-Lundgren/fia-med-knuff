class Piece
{
	public string? Color { get; set; }
	public int Id { get; set; }

	public (int, int) Position;

	public Piece(int id)
	{
		Id = id;
	}

	public void SetPosition(int x, int y)
	{
		Position = (x, y);
	}

	public void DrawPiece()
	{
		switch (Color)
		{
			case "red":
				Console.ForegroundColor = ConsoleColor.Red;
				break;
			case "blue":
				Console.ForegroundColor = ConsoleColor.Blue;
				break;
			case "green":
				Console.ForegroundColor = ConsoleColor.Green;
				break;
			case "yellow":
				Console.ForegroundColor = ConsoleColor.Yellow;
				break;
		}
		Console.ResetColor();
		Console.WriteLine();

		//ANVÄND egen funktion
	}
}
