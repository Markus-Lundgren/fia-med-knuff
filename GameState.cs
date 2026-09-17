using System.Text;

class GameState
{
	public List<Player> Players = new();

	public StringBuilder PlayerScore = new();

	public List<Player> PlayerOrder = new();

	public Board GameBoard = new();

	public void PlayerInfo()
	{
		foreach (Player p in Players)
		{
			Console.Write($"{p.Name} color: ");

			switch (p.Color)
			{
				case "red":
					MColoredText.GetColoredText(ConsoleColor.Red, p.Color);
					break;
				case "blue":
					MColoredText.GetColoredText(ConsoleColor.Blue, p.Color);
					break;
				case "green":
					MColoredText.GetColoredText(ConsoleColor.Green, p.Color);
					break;
				case "yellow":
					MColoredText.GetColoredText(ConsoleColor.Yellow, p.Color);
					break;
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}
	public void AddPlayers(Board b, int count)
	{
		List<string> colorList = ["red", "blue", "green", "yellow"];
		Dictionary<int, string> colorMatch = new();

		for (int i = 0; i < count; i++)
		{
			Player p = new();
			ShowGame();
			p.Id = i + 1;
			p.Name = MInput.GetInput($"Spelare {p.Id}, välj ett namn: ");

			while (true)
			{
				ShowGame();
				Console.WriteLine("Tillgängliga färger");
				int colorCount = 0;
				colorMatch.Clear();

				foreach (string c in colorList)
				{
					colorMatch.Add(++colorCount, c);
					Console.WriteLine($"{colorCount}. {c}");
				}
				string color = MInput.GetInput($"Spelare {p.Id}, välj en färg: ");

				if (int.TryParse(color, out int num))
				{
					if (num < 1 || num > colorCount)
					{
						Console.WriteLine("Ej ett giltigt val");
						Console.WriteLine("Tryck valfri knapp för att fortsätta");
						Console.ReadKey(true);
						continue;
					}
					else
					{
						p.Color = colorList[colorList.IndexOf(colorMatch[num].ToLower())];
						colorList.Remove(p.Color);
						Players.Add(p);
						break;
					}
				}
				else if (!colorList.Contains(color, StringComparer.OrdinalIgnoreCase))
				{
					Console.WriteLine("Ej ett giltigt val av färg.");
					Console.WriteLine("Tryck valfri knapp för att fortsätta");
					Console.ReadKey(true);
					continue;
				}
				else
				{
					p.Color = colorList[colorList.IndexOf(color.ToLower())];
					colorList.Remove(p.Color);
					p.AddPieces(p.Color);
					Players.Add(p);
					break;
				}
			}
		}
	}
	public void SetStartOrder(Board b, int playerCount)
	{
		Player[] playerOrder = new Player[playerCount];

		foreach (Player p in Players)
		{
			ShowGame();
			Console.WriteLine("Slå en tärning om vem som börjar!");
			Console.WriteLine();
			foreach (Player x in Players)
			{
				if (x.DiceRoll > 0)
				{
					Console.WriteLine($"{x.Name} fick: {x.DiceRoll}");
				}
			}
			Console.WriteLine($"{p.Name}'s tur!");
			p.RollDice();

			if (p == Players.Last())
			{
				Console.WriteLine();
				Console.WriteLine("Alla spelare har kastat sin tärning!");
				Console.WriteLine("Tryck på valfri knapp för att gå vidare!");
				Console.ReadKey(true);
			}
		}

		Player[] playerArray = new Player[playerCount];
		Players = SetOrder(Players);
		int order = 1;
		ShowGame();
		Console.WriteLine("Spel ordning");
		foreach (Player p in Players)
		{
			Console.WriteLine($"{order++}. {p.Name}");
		}
	}
	private List<Player> SetOrder(List<Player> orderingList)
	{
		List<Player> tempOrder = new();
		var groupPlayerList = orderingList.GroupBy(p => p.DiceRoll).OrderByDescending(g => g.Key);

		foreach (var g in groupPlayerList)
		{
			List<Player> playerTie = g.ToList();

			if (playerTie.Count == 1)
			{
				tempOrder.Add(playerTie[0]);
			}
			else
			{
				foreach (Player p in g)
				{
					Console.WriteLine($"{p.Name} kasta en till tärning!");
					p.RollDice();
				}
				List<Player> temp = SetOrder(playerTie);
				tempOrder.AddRange(temp);
			}
		}
		return tempOrder;
	}

	private void ShowGame()
	{
		GameBoard.DrawBoard();
		PlayerInfo();
	}

	public void SetupGame()
	{
		foreach (Player p in Players)
		{
			foreach (Piece piece in p.Pieces)
			{
				switch (p.Color.ToLower())
				{
					case "red":
						switch (piece.Id)
						{
							case 1:
								piece.Coords.Y = 1;
								piece.Coords.X = 1;
								break;
							case 2:
								piece.Coords.Y = 1;
								piece.Coords.X = 2;
								break;
							case 3:
								piece.Coords.Y = 2;
								piece.Coords.X = 1;
								break;
							case 4:
								piece.Coords.Y = 2;
								piece.Coords.X = 2;
								break;
						}
						break;

					case "blue":
						switch (piece.Id)
						{
							case 1:
								piece.Coords.Y = 1;
								piece.Coords.X = 12;
								break;
							case 2:
								piece.Coords.Y = 1;
								piece.Coords.X = 13;
								break;
							case 3:
								piece.Coords.Y = 2;
								piece.Coords.X = 12;
								break;
							case 4:
								piece.Coords.Y = 2;
								piece.Coords.X = 13;
								break;
						}
						break;

					case "green":
						switch (piece.Id)
						{
							case 1:
								piece.Coords.Y = 12;
								piece.Coords.X = 12;
								break;
							case 2:
								piece.Coords.Y = 12;
								piece.Coords.X = 13;
								break;
							case 3:
								piece.Coords.Y = 13;
								piece.Coords.X = 12;
								break;
							case 4:
								piece.Coords.Y = 13;
								piece.Coords.X = 13;
								break;
						}
						break;

					case "yellow":
						switch (piece.Id)
						{
							case 1:
								piece.Coords.Y = 12;
								piece.Coords.X = 1;
								break;
							case 2:
								piece.Coords.Y = 12;
								piece.Coords.X = 2;
								break;
							case 3:
								piece.Coords.Y = 13;
								piece.Coords.X = 1;
								break;
							case 4:
								piece.Coords.Y = 13;
								piece.Coords.X = 2;
								break;
						}
						break;
				}
				GameBoard.Tiles[piece.Coords.Y, piece.Coords.X]?.Pieces.Add(piece);
				Console.SetCursorPosition(2 + (piece.Coords.X * 2), GameBoard.BoardTop + piece.Coords.Y);
				GameBoard.Tiles[piece.Coords.Y, piece.Coords.X]?.DisplayTile();
			}
		}

	}

	public void Play()
	{
		while (true)
		{
			foreach (Player p in Players)
			{
				Console.Clear();
				ShowGame();
				Console.WriteLine($"{p.Name}'s tur!");
				Console.WriteLine();
				p.RollDice();
			}
		}
	}
}