using codemazepractice.domain;
using codemazepractice.persistence.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public OwnerController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        return Ok(await _unitOfWork.OwnerRepository.FindAllAsync());
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccountsBasedOnType()
    {
        return Ok(await _unitOfWork.OwnerRepository.GetAllOwnersWithAssociatedAccounts());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOwnerByID(Guid id)
    {
        return Ok(await _unitOfWork.OwnerRepository.FindOneAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwner(Owner owner)
    {
        var tempAcc = await _unitOfWork.OwnerRepository.CreateAsync(owner);
        await _unitOfWork.SaveChangesAsync();

        return Created("", tempAcc);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOwner(Owner owner)
    {
        bool IfExists = await _unitOfWork.OwnerRepository.ExistsAsync(owner.OwnerID);
        if(IfExists)
        {
            var tempAcc = _unitOfWork.OwnerRepository.Update(owner);
            await _unitOfWork.SaveChangesAsync();
            return Ok(tempAcc);
        }
        else
        {
            return Content("Record not found");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(Owner owner)
    {
        bool IfExists = await _unitOfWork.OwnerRepository.ExistsAsync(owner.OwnerID);
        if(IfExists)
        {
            var tempAcc = _unitOfWork.OwnerRepository.Delete(owner);
            await _unitOfWork.SaveChangesAsync();
            return Ok(tempAcc);
        }
        else
        {
            return Content("Record not found");
        }
    }
}