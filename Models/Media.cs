using System.ComponentModel.DataAnnotations;

namespace Models

{
    
    public interface IReadable
    {
        void DisplayInformation();

    }
    public abstract class Media : IReadable
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter a Valid Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "please enter a valid Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter a valid Type")]
        public string Type { get; set; }

        public abstract void DisplayInformation();


    }
}