class Board
{
	public int BoardTop { get; set; }

	public Tile?[,] Tiles { get; set; } =
	{
		{ new(0,0,"homeRed","","darkred"), new(0,1,"homeRed","","darkred"), new(0,2,"homeRed","","darkred"), new(0,3,"homeRed","","darkred"), null, null, new(0,6,"tile55","tile56"), new(0,7,"tile56","enterBlue", "darkblue", "blueGoal"), new(0,8,"enterBlue","tile2","blue"), null, null, new(0,11,"homeBlue","","darkblue"), new(0,12,"homeBlue","","darkblue"), new(0,13,"homeBlue","","darkblue"), new(0,14,"homeBlue","","darkblue") },
		{ new(1,0,"homeRed","","darkred"), new(1,1,"homeRed1","enterRed","darkred"), new(1,2,"homeRed2","enterRed","darkred"), new(1,3,"homeRed","","darkred"), null, null, new(1,6,"tile54","tile55"), new(1,7,"darkbluePath1","darkbluePath2","darkblue"), new(1,8,"tile2","tile3"), null, null, new(1,11,"homeBlue","","darkblue"), new(1,12,"homeBlue1","enterBlue","darkblue"), new(1,13,"homeBlue2","enterBlue","darkblue"), new(1,14,"homeBlue","","darkblue") },
		{ new(2,0,"homeRed","","darkred"), new(2,1,"homeRed3","enterRed","darkred"), new(2,2,"homeRed4","enterRed","darkred"), new(2,3,"homeRed","","darkred"), null, null, new(2,6,"tile53","tile54"), new(2,7,"darkbluePath2","darkbluePath3","darkblue"), new(2,8,"tile3","tile4"), null, null, new(2,11,"homeBlue","","darkblue"), new(2,12,"homeBlue3","enterBlue","darkblue"), new(2,13,"homeBlue4","enterBlue","darkblue"), new(2,14,"homeBlue","","darkblue") },
		{ new(3,0,"homeRed","","darkred"), new(3,1,"homeRed","","darkred"), new(3,2,"homeRed","","darkred"), new(3,3,"homeRed","","darkred"), null, null, new(3,6,"tile52","tile53"), new(3,7,"darkbluePath3","darkbluePath4","darkblue"), new(3,8,"tile4","tile5"), null, null, new(3,11,"homeBlue","","darkblue"), new(3,12,"homeBlue","","darkblue"), new(3,13,"homeBlue","","darkblue"), new(3,14,"homeBlue","","darkblue") },
		{ null, null, null, null, null, null, new(4,6,"tile51","tile52"), new(4,7,"darkbluePath4","darkbluePath5","darkblue"), new(4,8,"tile5","tile6"), null, null, null, null, null, null },
		{ null, null, null, null, null, null, new(5,6,"tile50","tile51"), new(5,7,"darkbluePath5","darkbluePath6","darkblue"), new(5,8,"tile6","tile7"), null, null, null, null, null, null },
		{ new(6,0,"enterRed","tile44","red"), new(6,1,"tile44","tile45"), new(6,2,"tile45","tile46"), new(6,3,"tile46","tile47"), new(6,4,"tile47","tile48"), new(6,5,"tile48","tile49"), new(6,6,"tile49","tile50"), new(6,7,"darkbluePath6","center","darkblue"), new(6,8,"tile7","tile8"), new(6,9,"tile8","tile9"), new(6,10,"tile9","tile10"), new(6,11,"tile10","tile11"), new(6,12,"tile11","tile12"), new(6,13,"tile12","tile13"), new(6,14,"tile13","tile14") },
		{ new(7,0,"tile42","enterRed", "darkred", "redGoal"), new(7,1,"darkredPath1","darkredPath2","darkred"), new(7,2,"darkredPath2","darkredPath3","darkred"), new(7,3,"darkredPath3","darkredPath4","darkred"), new(7,4,"darkredPath4","darkredPath5","darkred"), new(7,5,"darkredPath5","darkredPath6","darkred"), new(7,6,"darkredPath6","center","darkred"), new(7,7,"center","", "white", "Goal"), new(7,8,"darkgreenPath6","center","darkgreen"), new(7,9,"darkgreenPath5","darkgreenPath6","darkgreen"), new(7,10,"darkgreenPath4","darkgreenPath5","darkgreen"), new(7,11,"darkgreenPath3","darkgreenPath4","darkgreen"), new(7,12,"darkgreenPath2","darkgreenPath3","darkgreen"), new(7,13,"darkgreenPath1","darkgreenPath2","darkgreen"), new(7,14,"tile14","enterGreen", "darkgreen", "greenGoal") },
		{ new(8,0,"tile41","tile42"), new(8,1,"tile40","tile41"), new(8,2,"tile39","tile40"), new(8,3,"tile38","tile39"), new(8,4,"tile37","tile38"), new(8,5,"tile36","tile37"), new(8,6,"tile35","tile36"), new(8,7,"darkyellowPath6","center","darkyellow"), new(8,8,"tile21","tile22"), new(8,9,"tile20","tile21"), new(8,10,"tile19","tile20"), new(8,11,"tile18","tile19"), new(8,12,"tile17","tile18"), new(8,13,"tile16","tile17"), new(8,14,"enterGreen","tile16","green") },
		{ null, null, null, null, null, null, new(9,6,"tile34","tile35"), new(9,7,"darkyellowPath5","darkyellowPath6","darkyellow"), new(9,8,"tile22","tile23"), null, null, null, null, null, null },
		{ null, null, null, null, null, null, new(10,6,"tile33","tile34"), new(10,7,"darkyellowPath4","darkyellowPath5","darkyellow"), new(10,8,"tile23","tile24"), null, null, null, null, null, null },
		{ new(11,0,"homeYellow","","darkyellow"), new(11,1,"homeYellow","","darkyellow"), new(11,2,"homeYellow","","darkyellow"), new(11,3,"homeYellow","","darkyellow"), null, null, new(11,6,"tile32","tile33"), new(11,7,"darkyellowPath3","darkyellowPath4","darkyellow"), new(11,8,"tile24","tile25"), null, null, new(11,11,"homeGreen","","darkgreen"), new(11,12,"homeGreen","","darkgreen"), new(11,13,"homeGreen","","darkgreen"), new(11,14,"homeGreen","","darkgreen") },
		{ new(12,0,"homeYellow","","darkyellow"), new(12,1,"homeYellow1","enterYellow","darkyellow"), new(12,2,"homeYellow2","enterYellow","darkyellow"), new(12,3,"homeYellow","","darkyellow"), null, null, new(12,6,"tile31","tile32"), new(12,7,"darkyellowPath2","darkyellowPath3","darkyellow"), new(12,8,"tile25","tile26"), null, null, new(12,11,"homeGreen","","darkgreen"), new(12,12,"homeGreen1","enterGreen","darkgreen"), new(12,13,"homeGreen2","enterGreen","darkgreen"), new(12,14,"homeGreen","","darkgreen") },
		{ new(13,0,"homeYellow","","darkyellow"), new(13,1,"homeYellow3","enterYellow","darkyellow"), new(13,2,"homeYellow4","enterYellow","darkyellow"), new(13,3,"homeYellow","","darkyellow"), null, null, new(13,6,"tile30","tile31"), new(13,7,"darkyellowPath1","darkyellowPath2","darkyellow"), new(13,8,"tile26","tile27"), null, null, new(13,11,"homeGreen","","darkgreen"), new(13,12,"homeGreen3","enterGreen","darkgreen"), new(13,13,"homeGreen4","enterGreen","darkgreen"), new(13,14,"homeGreen","","darkgreen") },
		{ new(14,0,"homeYellow","","darkyellow"), new(14,1,"homeYellow","","darkyellow"), new(14,2,"homeYellow","","darkyellow"), new(14,3,"homeYellow","","darkyellow"), null, null, new(14,6,"enterYellow","tile30","yellow"), new(14,7,"tile28","enterYellow", "darkyellow", "yellowGoal"), new(14,8,"tile27","tile28"), null, null, new(14,11,"homeGreen","","darkgreen"), new(14,12,"homeGreen","","darkgreen"), new(14,13,"homeGreen","","darkgreen"), new(14,14,"homeGreen","","darkgreen") }
	};

	public void DrawBoard()
	{
		Console.Clear();
		Console.WriteLine("\x1b[3J");
		Console.Clear();
		Console.WriteLine("========Markus med knuff========");
		Console.WriteLine();
		BoardTop = Console.CursorTop;

		for (int y = 0; y < 15; y++)
		{
			for (int x = 0; x < 15; x++)
			{
				if (Tiles[y, x] != null) Tiles[y, x]?.DisplayTile();
				else Console.Write("  ");
				if (x == 14) Console.WriteLine();
			}
		}
	}
}