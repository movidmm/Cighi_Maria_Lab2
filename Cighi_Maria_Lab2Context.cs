using Microsoft.EntityFrameworkCore;

public class Cighi_Maria_Lab2Context(DbContextOptions<Cighi_Maria_Lab2Context> options) : DbContext(options)
{
    public DbSet<Cighi_Maria_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Cighi_Maria_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Cighi_Maria_Lab2.Models.Authors> Authors { get; set; } = default!;
    public DbSet<Cighi_Maria_Lab2.Models.Category> Category { get; set; } = default!;
}
