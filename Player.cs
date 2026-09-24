class Player
{
	public string? Name { get; set; }
	public int Id { get; set; }
	public int DiceRoll = 0;
	public string? Color { get; set; }
	public List<Piece> Pieces;
	public int PityRoll = 0;
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
	public void RollDice(bool start = false)
	{
		int diceRollTop = Console.CursorTop;
		if (!start) Board.WriteText("Slå en tärning!", 4);
		Board.WriteText("Tryck valfri knapp!", 5);
		Console.ReadKey(true); ;

		int diceRoll = 0;
		Board.WriteText($"Tärning: ", 6);
		for (int i = 0; i < 7; i++)
		{
			diceRoll = GetDiceRoll();
			Board.WriteText($"{diceRoll}", 6, 9);
			Thread.Sleep(250);
		}
		DiceRoll = diceRoll;
		bool homeCheck = false;
		foreach (Piece p in Pieces)
		{
			if (!p.InHome)
			{
				homeCheck = true;
				break;
			}
		}
		if (!homeCheck) PityRoll++;
		if (PityRoll == 3)
		{
			DiceRoll = 6;
			PityRoll = 0;
		}
		Board.WriteText($"Du fick: {DiceRoll}", 2);
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
		int line = 3;

		foreach (Piece p in Pieces.OrderByDescending(p => p.Steps).ToList())
		{
			//OM StepsToGoal - Steps - DiceRoll > 0
			//Låt ej användare välja den pjäsen
			//OBS om 6a 
			if ((canMove || !p.InHome) && (Piece.StepsToGoal - p.Steps - DiceRoll >= 0))
			{
				movable.Add(p);
				pieceMatch.Add(++pieceCount, p);
				Board.WriteText($"{pieceCount}. Pjäs {p.Id} {(p.InHome ? "i bas" : "ute")}{((p.Steps > 0) ? $", steg {p.Steps}" : "")}", line++);
				continue;
			}
		}

		if (movable.Count == 0)
		{
			Board.ClearTextbox();
			Board.WriteText("Du kan inte flytta några pjäser!", 1);
			Board.WriteText("Tryck på valfri knapp för nästa spelare!", 2);
			Console.ReadKey();
			return;
		}
		int choice = 0;

		Piece temp = movable[0];

		while (movable.Count > 1)
		{
			choice = MInput.GetInputAsInt("Välj en pjäs: ");
			line++;

			if (!pieceMatch.ContainsKey(choice))
			{
				Board.WriteText("Ej ett giltig val! försök igen", line + 1);
				continue;
			}
			Board.WriteText("", ++line);

			temp = movable[movable.IndexOf(pieceMatch[choice])];
			var tmp = Pieces.Where(p => p.Steps == temp.Steps + DiceRoll);

			if (tmp.Count() > 0 && !temp.InHome)
			{
				Board.WriteText("Du kan inte flytta denna pjäs", line + 1);
				Board.WriteText("Tryck på valfri knapp för att gå vidare", line + 2);
				Console.ReadKey();
				continue;
			}
			break;
		}
		if (temp != null && temp.InHome && (DiceRoll == 1 || DiceRoll == 6))
		{
			temp.Move(b, true);
			Thread.Sleep(125);
		}
		else
		{
			//OM StepsToGoal - Steps - DiceRoll > 0
			//Exit
			for (int i = 0; i < DiceRoll; i++)
			{
				if (i == DiceRoll - 1) temp.Move(b, true);
				else temp.Move(b);
				Thread.Sleep(125);
			}
		}
	}
}