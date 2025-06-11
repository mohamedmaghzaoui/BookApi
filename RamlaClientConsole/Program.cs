using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5163/") };

    static async Task Main(string[] args)
    {
        bool quitter = false;

        while (!quitter)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Voir tous les livres");
            Console.WriteLine("2. Chercher un livre (titre ou auteur, tri possible)");
            Console.WriteLine("3. Chercher un livre par ID");
            Console.WriteLine("4. Ajouter un livre");
            Console.WriteLine("5. Modifier un livre");
            Console.WriteLine("6. Supprimer un livre");
            Console.WriteLine("7. Quitter");
            Console.Write("Choix : ");
            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1": await AfficherLivres(); break;
                case "2": await ChercherParTitreAuteur(); break;
                case "3": await ChercherParId(); break;
                case "4": await AjouterLivre(); break;
                case "5": await ModifierLivre(); break;
                case "6": await SupprimerLivre(); break;
                case "7": quitter = true; break;
                default: Console.WriteLine("Choix invalide."); break;
            }
        }
    }
    static async Task AfficherLivres()
    {
        var livres = await client.GetFromJsonAsync<List<Media>>("livres");

        if (livres == null || livres.Count == 0)
        {
            Console.WriteLine("Aucun livre trouvé.");
            return;
        }

        Console.WriteLine("\n--- Liste des livres ---");
        foreach (var livre in livres)
        {
            Console.WriteLine($"ID: {livre.Id} | Titre: {livre.Title} | Auteur: {livre.Author} | Type: {livre.MediaType}");
        }
    }

    static async Task ChercherParTitreAuteur()
    {
        Console.Write("Titre (laisser vide si aucun filtre) : ");
        string? titre = Console.ReadLine();

        Console.Write("Auteur (laisser vide si aucun filtre) : ");
        string? auteur = Console.ReadLine();

        Console.Write("Trier par (author ou title, laisser vide pour aucun tri) : ");
        string? tri = Console.ReadLine();

        var url = $"livres?title={titre}&author={auteur}&sort={tri}";

        var livres = await client.GetFromJsonAsync<List<Media>>(url);

        if (livres == null || livres.Count == 0)
        {
            Console.WriteLine("Aucun résultat.");
            return;
        }

        Console.WriteLine("\n--- Résultats trouvés ---");
        foreach (var livre in livres)
        {
            Console.WriteLine($"ID: {livre.Id} | Titre: {livre.Title} | Auteur: {livre.Author} | Type: {livre.MediaType}");
        }
    }

    static async Task ChercherParId()
    {
        Console.Write("ID du livre : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalide.");
            return;
        }

        var livre = await client.GetFromJsonAsync<Media>($"livres/{id}");

        if (livre == null)
        {
            Console.WriteLine("Livre introuvable.");
        }
        else
        {
            Console.WriteLine($"\nID: {livre.Id} | Titre: {livre.Title} | Auteur: {livre.Author} | Type: {livre.MediaType}");
        }
    }
    static async Task AjouterLivre()
    {
        Console.Write("Titre : ");
        string? titre = Console.ReadLine();

        Console.Write("Auteur : ");
        string? auteur = Console.ReadLine();

        Console.Write("Type (ebook ou paperbook) : ");
        string? type = Console.ReadLine();

        var livre = new BookRequest
        {
            Title = titre,
            Author = auteur,
            MediaType = type
        };

        var reponse = await client.PostAsJsonAsync("livres", livre);
        Console.WriteLine(await reponse.Content.ReadAsStringAsync());
    }

    static async Task ModifierLivre()
    {
        Console.Write("ID du livre à modifier : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalide.");
            return;
        }

        var ancien = await client.GetFromJsonAsync<Media>($"livres/{id}");
        if (ancien == null)
        {
            Console.WriteLine("Livre non trouvé.");
            return;
        }

        Console.Write($"Nouveau titre (ancien : {ancien.Title}) : ");
        string? titre = Console.ReadLine();

        Console.Write($"Nouvel auteur (ancien : {ancien.Author}) : ");
        string? auteur = Console.ReadLine();

        Console.Write($"Type (ebook / paperbook) (ancien : {ancien.MediaType}) : ");
        string? type = Console.ReadLine();

        var modif = new BookRequest
        {
            Title = string.IsNullOrWhiteSpace(titre) ? ancien.Title : titre,
            Author = string.IsNullOrWhiteSpace(auteur) ? ancien.Author : auteur,
            MediaType = string.IsNullOrWhiteSpace(type) ? ancien.MediaType : type
        };

        var reponse = await client.PutAsJsonAsync($"livres/{id}", modif);
        Console.WriteLine(await reponse.Content.ReadAsStringAsync());
    }

    static async Task SupprimerLivre()
    {
        Console.Write("ID du livre à supprimer : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalide.");
            return;
        }

        var reponse = await client.DeleteAsync($"livres/{id}");
        Console.WriteLine(await reponse.Content.ReadAsStringAsync());
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

    [JsonPropertyName("type")] 
    public string? MediaType { get; set; }
}
