using System.Text;

class Board
{
	public StringBuilder Frame = new();
	public int BoardTop { get; set; }

	Tile?[,] Tiles { get; set; } =
	{
		{new Tile(0,0, "darkred"), new Tile(0,1, "darkred"), new Tile(0, 2, "darkred"), new Tile(0, 3, "darkred"), null, null, new Tile(0,6), new Tile(0,7), new Tile(0,8, "blue"), null, null, new Tile(0, 11, "darkblue"), new Tile(0, 12, "darkblue"), new Tile(0, 13, "darkblue"), new Tile(0, 14, "darkblue")},
		{new Tile(1,0, "darkred"), new Tile(1,1, "darkred"), new Tile(1, 2, "darkred"), new Tile(1, 3, "darkred"), null, null, new Tile(1,6), new Tile(1,7, "darkblue"), new Tile(1,8), null, null, new Tile(1, 11, "darkblue"), new Tile(1, 12, "darkblue"), new Tile(1, 13, "darkblue"), new Tile(1, 14, "darkblue")},
		{new Tile(2,0, "darkred"), new Tile(2,1, "darkred"), new Tile(2, 2, "darkred"), new Tile(2, 3, "darkred"), null, null, new Tile(2,6), new Tile(2,7, "darkblue"), new Tile(2,8), null, null, new Tile(2, 11, "darkblue"), new Tile(2, 12, "darkblue"), new Tile(2, 13, "darkblue"), new Tile(2, 14, "darkblue")},
		{new Tile(3,0, "darkred"), new Tile(3,1, "darkred"), new Tile(3, 2, "darkred"), new Tile(3, 3, "darkred"), null, null, new Tile(3,6), new Tile(3,7, "darkblue"), new Tile(3,8), null, null, new Tile(3, 11, "darkblue"), new Tile(3, 12, "darkblue"), new Tile(3, 13, "darkblue"), new Tile(3, 14, "darkblue")},
		{null, null, null, null, null, null, new Tile(4,6), new Tile(4,7, "darkblue"), new Tile(4,8), null, null, null, null, null, null},
		{null, null, null, null, null, null, new Tile(5,6), new Tile(5,7, "darkblue"), new Tile(5,8), null, null, null, null, null, null},
		{new Tile(6,0, "red"), new Tile(6,1), new Tile(6,2), new Tile(6,3), new Tile(6,4), new Tile(6,5), new Tile(6,6), new Tile(6,7), new Tile(6,8), new Tile(6,9), new Tile(6,10), new Tile(6,11), new Tile(6,12), new Tile(6,13), new Tile(6,14)},
		{new Tile(7,0), new Tile(7,1, "darkred"), new Tile(7,2, "darkred"), new Tile(7,3, "darkred"), new Tile(7,4, "darkred"), new Tile(7,5, "darkred"), new Tile(7,6), new Tile(7,7), new Tile(7,8), new Tile(7,9, "darkgreen"), new Tile(7,10, "darkgreen"), new Tile(7,11, "darkgreen"), new Tile(7,12, "darkgreen"), new Tile(7,13, "darkgreen"), new Tile(7,14)},
		{new Tile(8,0), new Tile(8,1), new Tile(8,2), new Tile(8,3), new Tile(8,4), new Tile(8,5), new Tile(8,6), new Tile(8,7), new Tile(8,8), new Tile(8,9), new Tile(8,10), new Tile(8,11), new Tile(8,12), new Tile(8,13), new Tile(8,14, "green")},
		{null, null, null, null, null, null, new Tile(9,6), new Tile(9,7, "darkyellow"), new Tile(9,8), null, null, null, null, null, null},
		{null, null, null, null, null, null, new Tile(10,6), new Tile(10,7, "darkyellow"), new Tile(10,8), null, null, null, null, null, null},
		{new Tile(11,0, "darkyellow"), new Tile(11,1, "darkyellow"), new Tile(11, 2, "darkyellow"), new Tile(11, 3, "darkyellow"), null, null, new Tile(11,6), new Tile(11,7, "darkyellow"), new Tile(11,8), null, null, new Tile(11, 11, "darkgreen"), new Tile(11, 12, "darkgreen"), new Tile(11, 13, "darkgreen"), new Tile(11, 14, "darkgreen")},
		{new Tile(12,0, "darkyellow"), new Tile(12,1, "darkyellow"), new Tile(12, 2, "darkyellow"), new Tile(12, 3, "darkyellow"), null, null, new Tile(12,6), new Tile(12,7, "darkyellow"), new Tile(12,8), null, null, new Tile(12, 11, "darkgreen"), new Tile(12, 12, "darkgreen"), new Tile(12, 13, "darkgreen"), new Tile(12, 14, "darkgreen")},
		{new Tile(13,0, "darkyellow"), new Tile(13,1, "darkyellow"), new Tile(13, 2, "darkyellow"), new Tile(13, 3, "darkyellow"), null, null, new Tile(13,6), new Tile(13,7, "darkyellow"), new Tile(13,8), null, null, new Tile(13, 11, "darkgreen"), new Tile(13, 12, "darkgreen"), new Tile(13, 13, "darkgreen"), new Tile(13, 14, "darkgreen")},
		{new Tile(14,0, "darkyellow"), new Tile(14,1, "darkyellow"), new Tile(14, 2, "darkyellow"), new Tile(14, 3, "darkyellow"), null, null, new Tile(14,6, "yellow"), new Tile(14,7), new Tile(14,8), null, null, new Tile(14, 11, "darkgreen"), new Tile(14, 12, "darkgreen"), new Tile(14, 13, "darkgreen"), new Tile(14, 14, "darkgreen")},
	};

	public Board()
	{
		int boardsize = 15;

		Frame.Clear();
		List<Tile> TileList = new();
		//string[,] board = new string[boardsize, boardsize];

		for (int y = 0; y < boardsize; y++)
		{
			for (int x = 0; x < boardsize; x++)
			{
				//board[i, j] = "x ";
				// if (i == 0)
				// {
				// 	if (j > 3 && j < 6)
				// 	{
				// 		Frame.Append(" ");
				// 		continue;
				// 	}
				// }
				if (y < Tiles.GetLength(0))
				{
					if (Tiles[y, x] != null) Tiles[y, x].DisplayTile();
					else Console.Write("  ");
					if (x == 14) Console.WriteLine();
				}
				Frame.Append(" ");
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
		for (int y = 0; y < 15; y++)
		{
			for (int x = 0; x < 15; x++)
			{
				if (Tiles[y, x] != null) Tiles[y, x].DisplayTile();
				else Console.Write("  ");
				if (x == 14) Console.WriteLine();
			}
		}
	}
}