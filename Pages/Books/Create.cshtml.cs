
using Cighi_Maria_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nume_Pren_Lab2.Models;

namespace Cighi_Maria_Lab2.Pages.Books;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Cighi_Maria_Lab2Context _context;

    public CreateModel(Cighi_Maria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(), "ID", "PublisherName");

        ViewData["AuthorsID"] = new SelectList(
            _context.Set<Cighi_Maria_Lab2.Models.Authors>(), "ID", "LastName");

        var book = new Book();
        book.BookCategories = new List<BookCategory>();

        PopulateAssignedCategoryData(_context, book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Set<Publisher>(), "ID", "PublisherName");

            ViewData["AuthorsID"] = new SelectList(
                _context.Set<Cighi_Maria_Lab2.Models.Authors>(), "ID", "LastName");

            Book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, Book);

            return Page();
        }

        Book.BookCategories = new List<BookCategory>();

        foreach (var cat in selectedCategories)
        {
            Book.BookCategories.Add(new BookCategory
            {
                CategoryID = int.Parse(cat)
            });
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
