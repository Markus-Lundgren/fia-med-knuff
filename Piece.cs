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
	}

	public void MoveHome(Board b)
	{
		InHome = true;
		Coords tmp = new();
		Steps = 0;
		StepsLeft = 63;
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
		Console.SetCursorPosition(Coords.X * 2, Board.BoardTop + Coords.Y);
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

			switch (curTile.SpecialInfo)
			{
				case "redGoal":
					if (Color == "red") nextCompare = "darkredPath1";
					break;
				case "blueGoal":
					if (Color == "blue") nextCompare = "darkbluePath1";
					break;
				case "greenGoal":
					if (Color == "green") nextCompare = "darkgreenPath1";
					break;
				case "yellowGoal":
					if (Color == "yellow") nextCompare = "darkyellowPath1";
					break;
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

		//Console.SetCursorPosition(0, b.BoardTop);
		curTile.Pieces.Remove(this);
		Console.SetCursorPosition(curTile.X * 2, Board.BoardTop + curTile.Y);
		curTile?.DisplayTile();

		Coords.X = nextTile.X;
		Coords.Y = nextTile.Y;

		nextTile.Pieces.Add(this);
		Console.SetCursorPosition(nextTile.X * 2, Board.BoardTop + nextTile.Y);
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
						Console.SetCursorPosition(nextTile.X * 2, Board.BoardTop + nextTile.Y);
						nextTile.DisplayTile();
						//b.DrawBoard();
						Board.WriteText($"Spelare {Player.Name} slog ut spelare {p.Player.Name}'s pjäs {p.Id}\nTryck på valfri knapp för att forsätta", 4);
						Console.ReadKey();
					}
				}
			}
			if (nextTile.Id == "center")
			{
				Player.Pieces.Remove(this);
				nextTile.Pieces.Remove(this);
				Console.SetCursorPosition(nextTile.X * 2, Board.BoardTop + nextTile.Y);
				nextTile.DisplayTile();
				//b.DrawBoard();
			}
		}
	}
}
