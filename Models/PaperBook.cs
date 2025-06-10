using Models;
public class PaperBook:Media ,IReadable
{
    public override void DisplayInformation()
    {
        Console.WriteLine($"paper book , title:{Title} and Author is {Author}");
    }
}