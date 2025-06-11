using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

    static async Task Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n==== Menu ====");
            Console.WriteLine("1. Afficher tous les livres");
            Console.WriteLine("2. Rechercher un livre");
            Console.WriteLine("3. Ajouter un livre");
            Console.WriteLine("4. Modifier un livre");
            Console.WriteLine("5. Supprimer un livre");
            Console.WriteLine("6. Quitter");
            Console.Write("Choix : ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": await GetAllBooks(); break;
                    case "2": await SearchBook(); break;
                    case "3": await AddBook(); break;
                    case "4": await UpdateBook(); break;
                    case "5": await DeleteBook(); break;
                    case "6": exit = true; break;
                    default: Console.WriteLine("Choix invalide."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }
    }

    static async Task GetAllBooks()
    {
        var books = await client.GetFromJsonAsync<List<Media>>("livres");
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id}: {book.Title} - {book.Author}");
        }
    }

    static async Task SearchBook()
    {
        Console.Write("Titre ? ");
        string? title = Console.ReadLine();
        Console.Write("Auteur ? ");
        string? author = Console.ReadLine();

        var url = $"livres?title={title}&author={author}";
        var books = await client.GetFromJsonAsync<List<Media>>(url);
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id}: {book.Title} - {book.Author}");
        }
    }

    static async Task AddBook()
    {
        Console.Write("Titre : ");
        string? title = Console.ReadLine();
        Console.Write("Auteur : ");
        string? author = Console.ReadLine();
        Console.Write("Type (ebook / paperbook) : ");
        string? mediaType = Console.ReadLine();

        var newBook = new BookRequest
        {
            Title = title,
            Author = author,
            MediaType = mediaType
        };

        var response = await client.PostAsJsonAsync("livres", newBook);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    static async Task UpdateBook()
    {
        Console.Write("ID du livre à modifier : ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nouveau titre : ");
        string? title = Console.ReadLine();
        Console.Write("Nouvel auteur : ");
        string? author = Console.ReadLine();

        var update = new BookRequest
        {
            Title = title,
            Author = author,
            MediaType = "ebook" // ou "paperbook", pas important pour update
        };

        var response = await client.PutAsJsonAsync($"livres/{id}", update);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    static async Task DeleteBook()
    {
        Console.Write("ID du livre à supprimer : ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var response = await client.DeleteAsync($"livres/{id}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}

public class BookRequest
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? MediaType { get; set; }
}

public class Media
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
}
