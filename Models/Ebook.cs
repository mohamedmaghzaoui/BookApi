using Models;
public class Ebook:Media,IReadable
{
    public override void DisplayInformation()
    {
        Console.WriteLine($"this is a ebook named {Title} created by {Author}");
    }

}