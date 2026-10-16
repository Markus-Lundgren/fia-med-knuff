class Piece
{
	public string? Color { get; set; }
	public int Id { get; set; }
	public string PieceDisplay = "■ ";
	public bool InHome = true;
	public int Steps = 0;
	public int StepsLeft = 63;
	public static int StepsToGoal = 63;
	public Player Player;
	public Coords Coords;
	public Piece(int id, Player player, string color) => (Id, Player, Color) = (id, player, color);
	public void SetPosition(int x, int y) => Coords = new Coords(x, y);

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
	}

	public void MoveHome(Board b)
	{
		InHome = true;
		Coords tmp = new();
		Steps = 0;
		StepsLeft = 63;
		var (y, x) = (Color.ToLower(), Id) switch
		{
			("red", 1) => (1, 1),
			("red", 2) => (1, 2),
			("red", 3) => (2, 1),
			("red", 4) => (2, 2),

			("blue", 1) => (1, 12),
			("blue", 2) => (1, 13),
			("blue", 3) => (2, 12),
			("blue", 4) => (2, 13),

			("green", 1) => (12, 12),
			("green", 2) => (12, 13),
			("green", 3) => (13, 12),
			("green", 4) => (13, 13),

			("yellow", 1) => (12, 1),
			("yellow", 2) => (12, 2),
			("yellow", 3) => (13, 1),
			("yellow", 4) => (13, 2)
		};

		(tmp.Y, tmp.X) = (y, x);
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

		string nextCompare = curTile.NextId;
		Steps++;
		StepsLeft--;

		foreach (Tile t in b.Tiles)
		{
			//Kolla om du är på en goal tile
			//Välj en path tile som nästa

			if (curTile.SpecialInfo == $"{Color}Goal")
			{
				nextCompare = $"dark{Color}Path1";
			}

			if (t != null && t.Id == nextCompare)
			{
				nextTile = t;
				break;
			}
		}


		if (nextTile == null)
		{
			Console.WriteLine($"ERROR! Felkoppling av tile: {curTile.Id} till {curTile.NextId}");
			Console.ReadKey();
			return;
		}

		Console.SetCursorPosition(0, b.BoardTop);
		curTile.Pieces.Remove(this);
		Console.SetCursorPosition(curTile.X * 2, b.BoardTop + curTile.Y);
		curTile?.DisplayTile();

		Coords.X = nextTile.X;
		Coords.Y = nextTile.Y;

		nextTile.Pieces.Add(this);
		Console.SetCursorPosition(nextTile.X * 2, b.BoardTop + nextTile.Y);
		nextTile.DisplayTile();
		if (lastMove)
		{
			if (nextTile.Pieces.Count > 1)
			{
				foreach (Piece p in nextTile.Pieces.ToList())
				{
					if (p != this && p.Player.Name != Player.Name)
					{
						p.MoveHome(b);
						nextTile.Pieces.Remove(p);
						nextTile.DisplayTile();
						Console.SetCursorPosition(0, Console.WindowHeight - 1);
						b.DrawBoard();
						Console.WriteLine($"Spelare {Player.Name} slog ut spelare {p.Player.Name}'s pjäs {p.Id}\nTryck på valfri knapp för att forsätta");
						Console.ReadKey();
					}
				}
			}
			if (nextTile.Id == "center")
			{
				Player.Pieces.Remove(this);
				nextTile.Pieces.Remove(this);
				b.DrawBoard();
			}
		}
	}
}