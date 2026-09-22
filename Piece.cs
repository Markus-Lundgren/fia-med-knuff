class Piece
{
	public string? Color { get; set; }
	public int Id { get; set; }
	public string PieceDisplay = "■ ";
	public bool InHome = true;
	public bool InGame = true;
	public int Steps = 0;
	public Player Player;
	public Coords Coords;
	public Piece(int id, Player player)
	{
		Id = id;
		Player = player;
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
		InHome = true;
		Coords tmp = new();
		Steps = 0;
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

		Tile tile = b.Tiles[Coords.Y, Coords.X];

		if (!tile.Pieces.Contains(this)) tile.Pieces.Add(this);
		Console.SetCursorPosition(Coords.X * 2, b.BoardTop + Coords.Y);
		tile.DisplayTile();
	}

	public void Move(Board b, bool lastMove = false)
	{
		InHome = false;
		Tile curTile = b.Tiles[Coords.Y, Coords.X];
		Tile nextTile = null;
		Steps++;

		Console.CursorVisible = false;

		foreach (Tile t in b.Tiles)
		{
			if (t != null && t.Id == curTile.NextId)
			{
				nextTile = t;
				break;
			}
		}

		if (nextTile != null)
		{
			curTile.Pieces.Remove(this);

			Console.SetCursorPosition(curTile.X * 2, b.BoardTop + curTile.Y);
			curTile?.DisplayTile();

			Coords.X = nextTile.X;
			Coords.Y = nextTile.Y;

			nextTile.Pieces.Add(this);
			Console.SetCursorPosition(nextTile.X * 2, b.BoardTop + nextTile.Y);
			nextTile.DisplayTile();
			if (lastMove && nextTile.Pieces.Count > 1)
			{
				foreach (Piece p in nextTile.Pieces.ToList())
				{
					if (p != this)
					{
						p.MoveHome(b);
						nextTile.Pieces.Remove(p);
						nextTile.DisplayTile();
						Console.SetCursorPosition(0, Console.WindowHeight - 1);
						Console.WriteLine($"Spelare {Player.Name} slog ut spelare {p.Player.Name}'s pjäs {p.Id}\nTryck på valfri knapp för att forsätta");
						Console.ReadKey();
					}
				}
			}
		}
		else
		{
			Console.WriteLine($"ERROR! Felkoppling av tile: {curTile.Id} till {curTile.NextId}");
			Console.ReadKey();
		}
		Console.CursorVisible = true;
	}

	private void MoveToSafe()
	{

	}
}
