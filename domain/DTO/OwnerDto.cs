namespace codemazepractice.domain.DTO;

public class OwnerDto
{
    public Guid OwnerID { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public IEnumerable<AccountDto> Accounts { get; set; } = default!;
}