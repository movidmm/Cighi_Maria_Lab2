using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cighi_Maria_Lab2.Models;

namespace Cighi_Maria_Lab2.Pages.AuthorsPages;

public class IndexModel : PageModel
{
    private readonly Cighi_Maria_Lab2Context _context;

    public IndexModel(Cighi_Maria_Lab2Context context)
    {
        _context = context;
    }

    public IList<Cighi_Maria_Lab2.Models.Authors> Authors { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.ToListAsync();
    }
}
