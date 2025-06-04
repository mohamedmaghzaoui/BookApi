using Models;
public class PaperBook : Media, IReadable
{
    public override void DisplayInformation()
    {
        Console.WriteLine($"PaperBook: {Title} by {Author}");

    }

}