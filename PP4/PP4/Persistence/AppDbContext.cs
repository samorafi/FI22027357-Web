using PP4.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace PP4.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TitleTag> TitlesTags => Set<TitleTag>();

    public string DbPath { get; }

    public AppDbContext()
    {
        var baseDir = Directory.GetCurrentDirectory();
        var dataDir = Path.Combine(baseDir, "data");
        Directory.CreateDirectory(dataDir);
        DbPath = Path.Combine(dataDir, "books.db");
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        var baseDir = Directory.GetCurrentDirectory();
        var dataDir = Path.Combine(baseDir, "data");
        Directory.CreateDirectory(dataDir);
        DbPath = Path.Combine(dataDir, "books.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nombre de tabla para la entidad de unión
        modelBuilder.Entity<TitleTag>().ToTable("TitlesTags");

        // Relaciones
        modelBuilder.Entity<Title>()
            .HasOne(t => t.Author)
            .WithMany(a => a.Titles)
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TitleTag>()
            .HasOne(tt => tt.Title)
            .WithMany(t => t.TitlesTags)
            .HasForeignKey(tt => tt.TitleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TitleTag>()
            .HasOne(tt => tt.Tag)
            .WithMany(tg => tg.TitlesTags)
            .HasForeignKey(tt => tt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        // Todas las propiedades requeridas (además de DataAnnotations)
        modelBuilder.Entity<Author>().Property(p => p.AuthorName).IsRequired();
        modelBuilder.Entity<Title>().Property(p => p.TitleName).IsRequired();
        modelBuilder.Entity<Tag>().Property(p => p.TagName).IsRequired();
        modelBuilder.Entity<TitleTag>().Property(p => p.TitleId).IsRequired();
        modelBuilder.Entity<TitleTag>().Property(p => p.TagId).IsRequired();

        // Orden de columnas en Title: TitleId, AuthorId, TitleName
        modelBuilder.Entity<Title>().Property(p => p.TitleId).HasColumnOrder(0);
        modelBuilder.Entity<Title>().Property(p => p.AuthorId).HasColumnOrder(1);
        modelBuilder.Entity<Title>().Property(p => p.TitleName).HasColumnOrder(2);
    }
}
