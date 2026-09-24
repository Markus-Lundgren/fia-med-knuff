using System.Text;
class GameState
{
	public List<Player> Players = new();
	public List<Player> WinnerOrder = new();
	public StringBuilder PlayerScore = new();
	public Board GameBoard = new();
	public int GameTop = Console.CursorTop;
	public int PlayerCount = 0;
	public void PlayerInfo()
	{
		foreach (Player p in Players)
		{
			switch (p.Color)
			{
				case "red":
					MColoredText.GetColoredText(ConsoleColor.Red, p.Name);
					break;
				case "blue":
					MColoredText.GetColoredText(ConsoleColor.Blue, p.Name);
					break;
				case "green":
					MColoredText.GetColoredText(ConsoleColor.Green, p.Name);
					break;
				case "yellow":
					MColoredText.GetColoredText(ConsoleColor.Yellow, p.Name);
					break;
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}
	public void AddPlayers(Board b)
	{
		List<string> colorList = ["red", "blue", "green", "yellow"];
		Dictionary<int, string> colorMatch = new();

		for (int i = 0; i < PlayerCount; i++)
		{
			Player p = new();
			ShowGame();
			p.Id = i + 1;
			p.Name = MInput.GetInput($"Spelare {p.Id}, välj ett namn: ");

			var tmp = Players.Where(pl => pl.Name == p.Name);
			while (tmp.Count() > 0)
			{
				p.Name = MInput.GetInput($"Spelare {p.Id}, välj ett annat namn: ");
			}

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
						p.AddPieces(p.Color);
						Players.Add(p);
						break;
					}
				}

				if (!colorList.Contains(color, StringComparer.OrdinalIgnoreCase))
				{
					Console.WriteLine("Ej ett giltigt val av färg.");
					Console.WriteLine("Tryck valfri knapp för att fortsätta");
					Console.ReadKey(true);
					continue;
				}
				p.Color = colorList[colorList.IndexOf(color.ToLower())];
				colorList.Remove(p.Color);
				p.AddPieces(p.Color);
				Players.Add(p);
				break;
			}
		}
		SetupGame();
	}
	public void SetStartOrder(Board b, int playerCount)
	{
		foreach (Player p in Players)
		{
			int loop = 3;
			ShowGame();
			Board.WriteText("Slå en tärning om vem som börjar!", 1);
			Console.WriteLine();
			foreach (Player x in Players)
			{
				if (x.DiceRoll > 0)
				{
					Board.WriteText($"{x.Name} fick: {x.DiceRoll}", loop++);
				}
			}
			Board.WriteText($"{p.Name}'s tur!", loop + 1);
			p.RollDice(true);

			if (p == Players.Last())
			{
				Board.ClearTextbox();
				Board.WriteText("Alla spelare har kastat sin tärning!", 1);
				Board.WriteText("Tryck på valfri knapp för att gå vidare!", 2);
				Console.ReadKey(true);
			}
		}

		Player[] playerArray = new Player[playerCount];
		Players = SetOrder(Players);
		int order = 1;
		int line = 2;
		ShowGame();
		Board.ClearTextbox();
		Board.WriteText("Turordning", 1);
		foreach (Player p in Players)
		{
			p.PityRoll = 0;
			Board.WriteText($"{order++}. {p.Name}", line++);
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
				piece.MoveHome(GameBoard);
			}
		}
	}
	public void Play()
	{
		while (Players.Count > 1)
		{
			foreach (Player p in Players.ToList())
			{
				if (p.Pieces.Count == 0) continue;
				GameBoard.GameStarted = true;
				ShowGame();
				Board.BoardTop = Console.CursorTop;
				Board.ClearTextbox();
				Board.WriteText(p.Name, 1, 0, p.Color);
				Board.WriteText("'s tur!", 1, p.Name.Length);

				p.RollDice();
				// Test conditions
				// if (p.Pieces[0].InHome) p.DiceRoll = 1;
				// else p.DiceRoll = 62;

				p.PlayRound(GameBoard);
				if (p.Pieces.Count == 0)
				{
					Board.WriteText($"{p.Name} har gått ut med alla sina pjäser", 3);
					WinnerOrder.Add(p);
					Players.Remove(p);
				}
				if (Players.Count == 1)
				{
					WinnerOrder.Add(Players.First());
					PrintScore();
				}
			}
		}
	}
	public void PrintScore()
	{
		Console.Clear();
		GameBoard.DrawBoard();

		PlayerScore.AppendLine("=================================");
		int order = 1;
		foreach (Player p in WinnerOrder)
		{
			PlayerScore.AppendLine($"{order++}. {p.Name} {(WinnerOrder.First() == p ? " - Winner" : (WinnerOrder.Last() == p ? " - Loser" : ""))}");
		}
		PlayerScore.AppendLine("=================================");

		Console.WriteLine(PlayerScore.ToString());
	}
}