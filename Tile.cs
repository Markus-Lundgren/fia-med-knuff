class Tile
{
	public string Color = "NONE";
	public string Id { get; set; }
	public string NextId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public bool Special = false;

	public Tile(int y, int x, string id, string nextId, string color = "NONE", bool special = false)
	{
		X = x;
		Y = y;
		Id = id;
		NextId = nextId;
		Color = color;
		Special = special;
	}
	public void DisplayTile()
	{
		if (Special) MColoredText.GetColoredText(Color, "▨ ");
		else if (Color != "NONE") MColoredText.GetColoredText(Color, "▢ ");
		else Console.Write("▢ ");
	}

	// public void DisplayTile()
	// {
	// 	// Print 3 empty spaces for null spots to preserve column alignment
	// 	if (this == null)
	// 	{
	// 		Console.Write("   ");
	// 		return;
	// 	}

	// 	string display = Id;

	// 	// Map entry tile names to track numbers
	// 	if (display == "enterBlue") display = "1";
	// 	else if (display == "enterGreen") display = "15";
	// 	else if (display == "enterYellow") display = "29";
	// 	else if (display == "enterRed") display = "43";

	// 	// Map home areas
	// 	else if (display == "homeRed") display = "R";
	// 	else if (display == "homeBlue") display = "B";
	// 	else if (display == "homeGreen") display = "G";
	// 	else if (display == "homeYellow") display = "Y";

	// 	// Strip prefixes and path designations
	// 	else
	// 	{
	// 		display = display.Replace("tile", "")
	// 						 .Replace("home", "")
	// 						 .Replace("darkredPath", "")
	// 						 .Replace("darkbluePath", "")
	// 						 .Replace("darkgreenPath", "")
	// 						 .Replace("darkyellowPath", "")
	// 						 .Replace("Red", "")
	// 						 .Replace("Blue", "")
	// 						 .Replace("Green", "")
	// 						 .Replace("Yellow", "");

	// 		if (display == "center") display = "O";
	// 	}

	// 	// Format output: strictly 2 characters wide + 1 space separator
	// 	string output = string.Format("{0,-2} ", display);

	// 	if (Special || Color != "NONE")
	// 	{
	// 		MColoredText.GetColoredText(Color, output);
	// 	}
	// 	else
	// 	{
	// 		Console.Write(output);
	// 	}
	// }
}