using System.Drawing;
using System.Text;
// class Home
// {
// 	public string Color { get; set; }
// }

// class TileList
// {
// 	List<Tile> TList = new();
// }
class Program
{
	static void Main()
	{
		Console.Clear();
		GameState gS = new();

		int playerCount = 0;

		while (true)
		{
			Console.Clear();
			Console.WriteLine("=Välkommen till Markus med knuff=");
			Console.WriteLine("=================================");
			Console.Write("Välj antal spelare 2-4: ");
			string input = Console.ReadLine()!;
			if (!int.TryParse(input, out playerCount))
			{
				Console.WriteLine("Du måste skriva ett NUMMER mellan 2 och 4");
				Console.WriteLine("Tryck valfri knapp för att gå vidare");
				Console.ReadKey(true);
				continue;
			}
			if (playerCount < 2 || playerCount > 4)
			{
				Console.WriteLine("Välj mellan 2 och 4 spelare!");
				Console.WriteLine("Tryck valfri knapp för att gå vidare");
				Console.ReadKey(true);
				continue;
			}
			Console.WriteLine($"Antal valda spelare: {playerCount}");
			break;
		}

		gS.AddPlayers(gS.GameBoard, playerCount);
		gS.SetStartOrder(gS.GameBoard, playerCount);

		while (true)
		{

		}
	}
}
