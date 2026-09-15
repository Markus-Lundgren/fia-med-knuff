static class MInput
{
	public static string GetInput(string message, bool isInt = false)
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

			if (isInt && !int.TryParse(sOutput, out int number))
			{
				Console.WriteLine("Du måste använda siffror");
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
			}
			else
			{
				return num;
			}
		}
	}
}