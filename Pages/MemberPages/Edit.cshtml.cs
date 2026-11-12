
using Cighi_Maria_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cighi_Maria_Lab2.Pages.MemberPages;

public class EditModel : PageModel
{
    private readonly Cighi_Maria_Lab2Context _context;

    public EditModel(Cighi_Maria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var member = await _context.Member
            .FirstOrDefaultAsync(m => m.ID == id);

        if (member == null)
        {
            return NotFound();
        }

        Member = member;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var memberToUpdate = await _context.Member
            .FirstOrDefaultAsync(m => m.ID == id);

        if (memberToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Member>(
            memberToUpdate,
            "Member",
            m => m.FirstName,
            m => m.LastName,
            m => m.Adress,
            m => m.Phone))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        Member = memberToUpdate;
        return Page();
    }
}
