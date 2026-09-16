class GameState
{
	public List<Player> Players { get; set; } = new();

	public void AddPlayers(int count)
	{
		List<string> colorList = ["red", "blue", "green", "yellow"];

		for (int i = 0; i < count; i++)
		{
			Player p = new();
			p.Name = "Player " + (i + 1);


			while (true)
			{
				Console.WriteLine("Tillgängliga färger");
				int colorCount = 1;
				foreach (string c in colorList)
				{
					Console.WriteLine($"{colorCount++}. {c}");
				}
				string color = MInput.GetInput("Välj en färg: ");
				if (!colorList.Contains(color, StringComparer.OrdinalIgnoreCase))
				{
					Console.WriteLine("Ej ett giltigt val av färg.");
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
}

class Player
{
	public string? Name { get; set; }
	public int Score { get; set; }
	public string? Color { get; set; }
	public List<Piece> Pieces { get; set; }
	public int GetDiceRoll() => Random.Shared.Next(1, 7);
}

class Board
{
	public void CreateBoard()
	{
		int boardsize = 15;
		string[,] board = new string[boardsize, boardsize];

		for (int i = 0; i < boardsize; i++)
		{
			for (int j = 0; j < boardsize; j++)
			{
				board[i, j] = "x ";
				Console.Write(board[i, j]);
			}
			Console.WriteLine();
		}
	}
}

class Piece : IColored
{
	public string Color { get; set; }
}

class Home : IColored
{
	public string Color { get; set; }
}
class Space : IColored
{
	public string Color { get; set; }
}

interface IColored
{
	public string Color { get; set; }
}

interface ISafe
{
	//Do something
}
class SafeSpace : Space, ISafe
{

}

class FinishSpace : Space, ISafe
{

}
class Program
{
	static void Main()
	{
		GameState gS = new();
		Board board = new();
		int playerCount = 0;
		Console.WriteLine("Välkommen till Markus med knuff");
		Console.WriteLine("===============================");
		while (true)
		{
			playerCount = MInput.GetInputAsInt("Välj antal spelare 2-4: ");
			if (playerCount < 2 || playerCount > 4)
			{
				Console.WriteLine("Välj mellan 2 och 4 spelare!");
				continue;
			}
			Console.WriteLine($"Antal valda spelare: {playerCount}");
			break;
		}

		gS.AddPlayers(playerCount);

		foreach (Player p in gS.Players)
		{
			Console.Write($"{p.Name} color: ");

			switch (p.Color)
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

			Console.Write(p.Color);
			Console.ResetColor();
			Console.WriteLine();
		}

		board.CreateBoard();
	}
}
