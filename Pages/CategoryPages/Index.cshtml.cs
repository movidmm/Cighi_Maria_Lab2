
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cighi_Maria_Lab2.Models;
using Cighi_Maria_Lab2.Models.ViewModels;

namespace Cighi_Maria_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Cighi_Maria_Lab2Context _context;

    public IndexModel(Cighi_Maria_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = new List<Category>();

    public CategoryIndexData CategoryData { get; set; } = new CategoryIndexData();

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();

        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Authors)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        Category = CategoryData.Categories.ToList();

        if (id != null)
        {
            Category? category = Category.FirstOrDefault(c => c.ID == id.Value);

            if (category != null)
            {
                CategoryID = category.ID;

                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book)
                    .ToList();
            }
        }
    }
}
