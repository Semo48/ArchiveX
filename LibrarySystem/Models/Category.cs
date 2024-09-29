using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
