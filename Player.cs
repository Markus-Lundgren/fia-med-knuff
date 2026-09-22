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
			Piece p = new Piece(i + 1, this);
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

		bool canMove = false;

		if (DiceRoll == 1 || DiceRoll == 6)
		{
			canMove = true;
		}

		List<Piece> movable = new();
		Dictionary<int, Piece> pieceMatch = new();
		int pieceCount = 0;

		foreach (Piece p in Pieces)
		{
			//OM StepsToGoal - Steps - DiceRoll > 0
			//Låt ej användare välja den pjäsen
			if ((canMove || !p.InHome) && (Piece.StepsToGoal - p.Steps - DiceRoll < 0))
			{
				movable.Add(p);
				pieceMatch.Add(++pieceCount, p);
				Console.WriteLine($"{pieceCount}. Pjäs {p.Id} {(p.InHome ? "i bas" : "ute")}{((p.Steps > 0) ? $", steg {p.Steps}" : "")}");
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

		Piece temp = movable[0];

		//NÄR tillgänglig pjäs finns
		//Hämta val av pjäs

		while (movable.Count > 1)
		{
			Console.SetCursorPosition(0, cursorTop);
			choice = MInput.GetInputAsInt("Välj en pjäs: ");
			Console.WriteLine();

			if (!pieceMatch.ContainsKey(choice))
			{
				//Console.SetCursorPosition(0, cursorTop);
				Console.Write("Ej ett giltig val! försök igen                                ");
				Console.WriteLine();
				continue;
			}

			temp = movable[movable.IndexOf(pieceMatch[choice])];
			break;
		}


		// foreach (Piece piece in movable)
		// {
		//	if (piece.Id != choice) continue;

		if (temp != null && temp.InHome && (DiceRoll == 1 || DiceRoll == 6))
		{
			temp.Move(b, true);
			Thread.Sleep(250);
		}
		else
		{
			//OM StepsToGoal - Steps - DiceRoll > 0
			//Exit
			for (int i = 0; i < DiceRoll; i++)
			{
				if (i == DiceRoll - 1) temp.Move(b, true);
				else temp.Move(b);
				Thread.Sleep(250);
			}
		}
	}

	public void ShowAvailablePieces()
	{

	}
}