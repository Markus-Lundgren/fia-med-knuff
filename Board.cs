using System.Text;

class Board
{
	public StringBuilder Frame = new();
	public int BoardTop { get; set; }
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
		//Console.WriteLine("================================");
		Console.WriteLine();
		BoardTop = Console.CursorTop;
		Console.WriteLine(Frame.ToString());
	}
}