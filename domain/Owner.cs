namespace codemazepractice.domain;

public class Owner
{
    public Guid OwnerID { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public IEnumerable<Account> Accounts { get; set; } = default!;
}