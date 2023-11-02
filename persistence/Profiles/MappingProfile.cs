using AutoMapper;
using codemazepractice.domain;
using codemazepractice.domain.DTO;

namespace codemazepractice.persistence.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<Owner, OwnerDto>().ReverseMap();
    }
}