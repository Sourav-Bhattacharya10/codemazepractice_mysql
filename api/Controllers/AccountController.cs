using codemazepractice.domain;
using codemazepractice.persistence.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        return Ok(await _unitOfWork.AccountRepository.FindAllAsync());
    }

    [HttpGet("/type")]
    public async Task<IActionResult> GetAccountsBasedOnType([FromQuery] string accountType)
    {
        return Ok(await _unitOfWork.AccountRepository.FindByConditionAsync(record => record.AccountType == accountType));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountByID(Guid id)
    {
        return Ok(await _unitOfWork.AccountRepository.FindOneAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(Account account)
    {
        var tempAcc = await _unitOfWork.AccountRepository.CreateAsync(account);
        await _unitOfWork.SaveChangesAsync();

        return Created("", tempAcc);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccount(Account account)
    {
        bool IfExists = await _unitOfWork.AccountRepository.ExistsAsync(account.AccountID);
        if(IfExists)
        {
            var tempAcc = _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();
            return Ok(tempAcc);
        }
        else
        {
            return Content("Record not found");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(Account account)
    {
        bool IfExists = await _unitOfWork.AccountRepository.ExistsAsync(account.AccountID);
        if(IfExists)
        {
            var tempAcc = _unitOfWork.AccountRepository.Delete(account);
            await _unitOfWork.SaveChangesAsync();
            return Ok(tempAcc);
        }
        else
        {
            return Content("Record not found");
        }
    }
}