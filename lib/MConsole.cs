static class MConsole
{
	public static void ClearConsoleLine(int cursorTop)
	{
		Console.SetCursorPosition(0, cursorTop);
		Console.Write(new string(' ', Console.WindowWidth));
		Console.SetCursorPosition(0, cursorTop);
	}
	public static void ClearConsoleLineMulti(int cursorTop, int lines)
	{
		for (int i = 0; i < lines; i++)
		{
			if (cursorTop + i < Console.BufferHeight)
			{
				Console.SetCursorPosition(0, cursorTop + i);
				Console.Write(new string(' ', Console.WindowWidth));
			}
		}
		Console.SetCursorPosition(0, cursorTop);
	}
}