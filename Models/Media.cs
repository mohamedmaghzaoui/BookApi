
using System.ComponentModel.DataAnnotations;

namespace Models

{
    
    public interface IReadable
    {
        void DisplayInformation();

    }
    public abstract class Media : IReadable
    {
        
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter a valid title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a valid author.")]
        public string Author { get; set; }
        public abstract void DisplayInformation();


    }
}