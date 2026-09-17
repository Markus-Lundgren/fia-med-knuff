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

	}
	public void AddPlayers(Board b, int count)
	{
		List<string> colorList = ["red", "blue", "green", "yellow"];
		Dictionary<int, string> colorMatch = new();

		for (int i = 0; i < count; i++)
		{
			Player p = new();
			b.DrawBoard();
			PlayerInfo();
			p.Id = i + 1;
			p.Name = MInput.GetInput($"Spelare {p.Id}, välj ett namn: ");

			while (true)
			{
				b.DrawBoard();
				PlayerInfo();
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
			b.DrawBoard();
			PlayerInfo();
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

		b.DrawBoard();
		PlayerInfo();
		Player[] playerArray = new Player[playerCount];
		Players = SetOrder(Players);
		int order = 1;
		b.DrawBoard();
		PlayerInfo();
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
	public void Play()
	{
		while (true)
		{
			foreach (Player p in Players)
			{
				Console.Clear();
				ShowGame();
				Console.WriteLine("Dags att spela!");

				p.RollDice();
			}
		}
	}
}