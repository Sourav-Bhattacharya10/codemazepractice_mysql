namespace codemazepractice.domain;

public class Account
{
    public Guid AccountID { get; set; }
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string AccountType { get; set; } = string.Empty;
    public Guid OwnerID { get; set; }
}  