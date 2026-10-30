
using Cighi_Maria_Lab2.Models;

namespace Cighi_Maria_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();

        public IEnumerable<Book> Books { get; set; }
            = new List<Book>();
    }
}
