
using Cighi_Maria_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nume_Pren_Lab2.Models;

namespace Cighi_Maria_Lab2.Pages.Books;

public class EditModel : BookCategoriesPageModel
{
    private readonly Cighi_Maria_Lab2Context _context;

    public EditModel(Cighi_Maria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);

        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(), "ID", "PublisherName");

        ViewData["AuthorsID"] = new SelectList(
            _context.Set<Cighi_Maria_Lab2.Models.Authors>(), "ID", "LastName");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.Price,
            b => b.PublishingDate,
            b => b.PublisherID,
            b => b.AuthorsID))
        {
            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        UpdateBookCategories(
            _context, selectedCategories, bookToUpdate);

        PopulateAssignedCategoryData(_context, bookToUpdate);

        Book = bookToUpdate;

        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(), "ID", "PublisherName");

        ViewData["AuthorsID"] = new SelectList(
            _context.Set<Cighi_Maria_Lab2.Models.Authors>(), "ID", "LastName");

        return Page();
    }
}
