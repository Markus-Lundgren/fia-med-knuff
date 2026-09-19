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

	public void MoveHome(Board b)
	{
		Coords tmp = new();
		switch (Color?.ToLower())
		{
			case "red":
				switch (Id)
				{
					case 1:
						tmp.Y = 1;
						tmp.X = 1;
						break;
					case 2:
						tmp.Y = 1;
						tmp.X = 2;
						break;
					case 3:
						tmp.Y = 2;
						tmp.X = 1;
						break;
					case 4:
						tmp.Y = 2;
						tmp.X = 2;
						break;
				}
				break;

			case "blue":
				switch (Id)
				{
					case 1:
						tmp.Y = 1;
						tmp.X = 12;
						break;
					case 2:
						tmp.Y = 1;
						tmp.X = 13;
						break;
					case 3:
						tmp.Y = 2;
						tmp.X = 12;
						break;
					case 4:
						tmp.Y = 2;
						tmp.X = 13;
						break;
				}
				break;

			case "green":
				switch (Id)
				{
					case 1:
						tmp.Y = 12;
						tmp.X = 12;
						break;
					case 2:
						tmp.Y = 12;
						tmp.X = 13;
						break;
					case 3:
						tmp.Y = 13;
						tmp.X = 12;
						break;
					case 4:
						tmp.Y = 13;
						tmp.X = 13;
						break;
				}
				break;

			case "yellow":
				switch (Id)
				{
					case 1:
						tmp.Y = 12;
						tmp.X = 1;
						break;
					case 2:
						tmp.Y = 12;
						tmp.X = 2;
						break;
					case 3:
						tmp.Y = 13;
						tmp.X = 1;
						break;
					case 4:
						tmp.Y = 13;
						tmp.X = 2;
						break;
				}
				break;
		}

		Coords = tmp;
		b.Tiles[Coords.Y, Coords.X]?.Pieces.Add(this);
		Console.SetCursorPosition(2 + (Coords.X * 2), b.BoardTop + Coords.Y);
		b.Tiles[Coords.Y, Coords.X]?.DisplayTile();
	}

	public void Move()
	{

	}
}
