class Player
{
	public string? Name { get; set; }
	public int Id { get; set; }
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
		Console.WriteLine($"Du fick: {diceRoll}!");
		Console.CursorVisible = true;
	}
}