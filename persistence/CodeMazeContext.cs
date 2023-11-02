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

    public DbSet<Owner> Owner { get; set; }
    public DbSet<Account> Account { get; set; }
}