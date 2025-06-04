using Models;
public class Ebook:Media,IReadable
{
    public override void DisplayInformation()
    {
        Console.WriteLine($"PaperBook: {Title} by {Author}");

    }

}