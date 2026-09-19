class Player
{
	public string? Name { get; set; }
	public int Id { get; set; }
	public int DiceRoll = 0;
	public int Score { get; set; }
	public string? Color { get; set; }
	public List<Piece> Pieces;

	public Player()
	{
		Pieces = new();
	}
	public void AddPieces(string color)
	{
		for (int i = 0; i < 4; i++)
		{
			Piece p = new Piece(i + 1);
			p.Color = color;
			Pieces.Add(p);
			Console.WriteLine("Added piece " + (i + 1));
		}
	}
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
		Console.WriteLine($"Du fick: {diceRoll}!");
		Console.CursorVisible = true;
	}

	public void PlayRound(Board b)
	{
		if (DiceRoll == 0) RollDice();

		//Skapa logik för att skriva ut tillgängliga pjäser baserade på vissa kriterier

		//OM ALLA pjäser är i boet OCH DiceRoll != 1 ELLER DiceRoll != 6
		//Visa igen, ge ett tröst meddelande
		//

		bool canMove = false;

		if (DiceRoll == 1 || DiceRoll == 6)
		{
			canMove = true;
		}

		List<Piece> movable = new();

		int pieceCount = 0;

		foreach (Piece p in Pieces)
		{
			if (canMove || !p.InHome)
			{
				movable.Add(p);
				Console.WriteLine($"{++pieceCount}. Pjäs {p.Id}");
				continue;
			}
		}

		if (movable.Count == 0)
		{
			Console.WriteLine("Du kan inte flytta några pjäser!");
			Console.WriteLine("Tryck på valfri knapp för nästa spelare!");
			Console.ReadKey();
			return;
		}

		Console.WriteLine();
		int cursorTop = Console.CursorTop;
		int choice = 0;


		//TODO: Fixa logiskt feltänk
		while (true)
		{
			Console.SetCursorPosition(0, cursorTop);
			choice = MInput.GetInputAsInt("Välj en pjäs (skriv siffran till höger om pjäs pls): ");
			Console.WriteLine();
			if (choice < movable.Count || choice > movable.Count)
			{
				Console.SetCursorPosition(0, cursorTop);
				Console.WriteLine("Ej ett giltig val! försök igen                                ");
				continue;
			}
			break;
		}

		foreach (Piece piece in movable)
		{
			if (piece.Id != choice) continue;

			if (piece.InHome)
			{
				piece.Move(b);
				Thread.Sleep(250);
			}
			else
			{
				for (int i = 0; i < DiceRoll; i++)
				{
					piece.Move(b);
					Thread.Sleep(250);
				}
			}
		}
	}

	public void ShowAvailablePieces()
	{

	}
}