static class MColoredText
{
	public static void GetColoredText(ConsoleColor color, string? message, bool IsLine = false)
	{
		if (!IsLine)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}
	}
	public static void GetColoredText(string input, string? message, bool IsLine = false)
	{
		ConsoleColor color = ConsoleColor.Black;
		switch (input.ToLower())
		{
			case "black":
				color = ConsoleColor.Black;
				break;
			case "darkblue":
				color = ConsoleColor.DarkBlue;
				break;
			case "darkgreen":
				color = ConsoleColor.DarkGreen;
				break;
			case "darkcyan":
				color = ConsoleColor.DarkCyan;
				break;
			case "darkred":
				color = ConsoleColor.DarkRed;
				break;
			case "darkmagenta":
				color = ConsoleColor.DarkMagenta;
				break;
			case "darkyellow":
				color = ConsoleColor.DarkYellow;
				break;
			case "gray":
			case "grey":
				color = ConsoleColor.Gray;
				break;
			case "darkgray":
			case "darkgrey":
				color = ConsoleColor.DarkGray;
				break;
			case "blue":
				color = ConsoleColor.Blue;
				break;
			case "green":
				color = ConsoleColor.Green;
				break;
			case "cyan":
				color = ConsoleColor.Cyan;
				break;
			case "red":
				color = ConsoleColor.Red;
				break;
			case "magenta":
				color = ConsoleColor.Magenta;
				break;
			case "yellow":
				color = ConsoleColor.Yellow;
				break;
			case "white":
				color = ConsoleColor.White;
				break;
			default:
				return;
		}

		if (!IsLine)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}
	}
}