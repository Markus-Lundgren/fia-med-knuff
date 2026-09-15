class Page
{

}

class Book // Bok HAR sidor, bok ÄR INTE sidor
{
	public Page[] Pages { get; set; }
	public string Author { get; set; }
	public string Genre { get; set; }
	public string Title { get; set; }
	public int PageCount { get; set; }

}