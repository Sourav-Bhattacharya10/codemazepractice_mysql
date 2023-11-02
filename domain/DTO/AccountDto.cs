namespace codemazepractice.domain.DTO;

public class AccountDto
{
    public Guid AccountID { get; set; }
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string AccountType { get; set; } = string.Empty;
}