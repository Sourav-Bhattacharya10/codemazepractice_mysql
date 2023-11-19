using codemazepractice.domain;
using codemazepractice.persistence.Converters;
using Microsoft.EntityFrameworkCore;

namespace codemazepractice.persistence;

public class CodeMazeContext: DbContext
{
    public CodeMazeContext(DbContextOptions<CodeMazeContext> options): base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ImageInfo>().HasKey(ii => ii.ImageID); // Fluent API to define Primary Key
    }

    public DbSet<Owner> Owner { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<ImageInfo> ImageInfo { get; set; }
}