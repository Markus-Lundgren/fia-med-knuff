using System.Text;

class GameState
{
	public List<Player> Players { get; set; } = new();

	public StringBuilder PlayerScore = new();

	public void PlayerInfo()
	{
		//PlayerInfo.Clear();
		foreach (Player p in Players)
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

	}
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

	public int DiceRoll = 0;
	public int Score { get; set; }
	public string? Color { get; set; }
	public List<Piece> Pieces { get; set; }
	public int GetDiceRoll() => Random.Shared.Next(1, 7);
}

class Board
{
	public StringBuilder Frame = new();
	public void CreateBoard()
	{
		int boardsize = 15;
		Frame.Clear();
		//string[,] board = new string[boardsize, boardsize];

		for (int i = 0; i < boardsize; i++)
		{
			for (int j = 0; j < boardsize; j++)
			{
				//board[i, j] = "x ";
				Frame.Append("x ");
			}
			Frame.AppendLine();
		}
		//Console.WriteLine(Frame.ToString());
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
		Console.Clear();
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

		board.CreateBoard();

		Player[] playerOrder = new Player[playerCount];

		foreach (Player p in gS.Players)
		{
			Console.Clear();
			Console.WriteLine(board.Frame.ToString());
			gS.PlayerInfo();
			Console.WriteLine("Slå en tärning om vem som börjar!");
			foreach (Player x in gS.Players)
			{
				if (x.DiceRoll > 0)
				{
					Console.WriteLine($"{x.Name} fick: {x.DiceRoll}");
				}
			}
			Console.WriteLine($"{p.Name}'s tur!");
			Console.WriteLine("Slå en tärning!");
			Console.WriteLine("Tryck valfri knapp!");

			Console.ReadKey(true);
			Console.CursorVisible = false;

			int diceRoll = 0;
			int currentCursor = Console.CursorTop;

			for (int i = 0; i < 7; i++)
			{
				Console.SetCursorPosition(0, currentCursor);

				diceRoll = p.GetDiceRoll();
				Console.Write($"Tärning: {diceRoll}");

				Thread.Sleep(250);
			}
			p.DiceRoll = diceRoll;
			Console.SetCursorPosition(0, currentCursor);
			Console.Write($"Du fick: {diceRoll}! Tryck på valfri knapp för nästa spelare.");
			Console.CursorVisible = true;
			Console.ReadKey(true);
		}
	}
}
