using System.Drawing;
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
	public void AddPlayers(Board b, int count)
	{
		List<string> colorList = ["red", "blue", "green", "yellow"];
		Dictionary<int, string> colorMatch = new();

		for (int i = 0; i < count; i++)
		{
			Player p = new();
			p.Name = "Player " + (i + 1);

			while (true)
			{
				b.DrawBoard();
				PlayerInfo();
				Console.WriteLine("Tillgängliga färger");
				int colorCount = 1;
				colorMatch.Clear();

				foreach (string c in colorList)
				{
					colorMatch.Add(colorCount, c);
					Console.WriteLine($"{colorCount++}. {c}");
				}
				string color = MInput.GetInput("Välj en färg: ");

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
}

class Player
{
	public string? Name { get; set; }

	public int DiceRoll = 0;
	public int Score { get; set; }
	public string? Color { get; set; }
	public List<Piece> Pieces = new();
	public int GetDiceRoll() => Random.Shared.Next(1, 7);
	public void RollDice()
	{
		Console.WriteLine("Slå en tärning!");
		Console.Write("Tryck valfri knapp!");
		int currentCursor = Console.CursorTop;
		Console.ReadKey(true);
		Console.SetCursorPosition(0, currentCursor);
		Console.Write("                    ");
		Console.CursorVisible = false;

		int diceRoll = 0;
		currentCursor = Console.CursorTop;

		for (int i = 0; i < 7; i++)
		{
			Console.SetCursorPosition(0, currentCursor);

			diceRoll = GetDiceRoll();
			Console.Write($"Tärning: {diceRoll}");

			Thread.Sleep(250);
		}
		DiceRoll = diceRoll;
		Console.SetCursorPosition(0, currentCursor);
		Console.Write($"Du fick: {diceRoll}!");
		Console.CursorVisible = true;
	}
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
	}

	public void DrawBoard()
	{
		Console.Clear();
		Console.WriteLine("\x1b[3J");
		Console.Clear();
		Console.WriteLine("========Markus med knuff========");
		Console.WriteLine("================================");
		Console.WriteLine();
		Console.WriteLine(Frame.ToString());
	}
}

class Piece : IColored
{
	public string Color { get; set; }
	public int Id { get; set; }

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
	}
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
		board.CreateBoard();

		int playerCount = 0;

		Console.WriteLine("=Välkommen till Markus med knuff=");
		Console.WriteLine("=================================");
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

		gS.AddPlayers(board, playerCount);

		Player[] playerOrder = new Player[playerCount];

		foreach (Player p in gS.Players)
		{
			board.DrawBoard();
			gS.PlayerInfo();
			Console.WriteLine("Slå en tärning om vem som börjar!");
			Console.WriteLine();
			foreach (Player x in gS.Players)
			{
				if (x.DiceRoll > 0)
				{
					Console.WriteLine($"{x.Name} fick: {x.DiceRoll}");
				}
			}
			Console.WriteLine($"{p.Name}'s tur!");
			p.RollDice();

			if (p == gS.Players.Last())
			{
				Console.WriteLine();
				Console.WriteLine("Alla spelare har kastat sin tärning!");
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("Tryck på valfri knapp för nästa spelare");
			}
			Console.ReadKey(true);
		}

		board.DrawBoard();
		gS.PlayerInfo();

		foreach (Player x in gS.Players)
		{
			if (x.DiceRoll > 0)
			{
				Console.WriteLine($"{x.Name} fick: {x.DiceRoll}");
			}
		}



	}
}
