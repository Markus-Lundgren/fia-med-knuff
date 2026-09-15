static class MInput
{
	public static string GetInput(string message)
	{
		while (true)
		{
			Console.Write(message);
			string sOutput = Console.ReadLine()!.Trim();

			if (string.IsNullOrWhiteSpace(sOutput))
			{
				Console.WriteLine("Fältet får inte vara tomt!");
				continue;
			}
			return sOutput;
		}
	}

	public static int GetInputAsInt(string message)
	{
		while (true)
		{
			Console.Write(message);
			if (!int.TryParse(Console.ReadLine(), out int num))
			{
				Console.WriteLine("Du måste använda siffror!");
				continue;
			}
			return num;
		}
	}

	public static string CheckInput(string input, string message = "Skriv in ett giltigt input: ")
	{
		while (true)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				Console.Write(message);
				input = Console.ReadLine()!;
				continue;
			}
			return input;
		}
	}

	public static int CheckInputInt(string input, string message = "Skriv in ett giltigt input: ")
	{
		while (true)
		{
			if (!int.TryParse(input, out int num))
			{
				Console.Write(message);
				input = Console.ReadLine()!;
				continue;
			}
			return num;
		}
	}
}