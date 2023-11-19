using AutoMapper;
using codemazepractice.application.Services;
using codemazepractice.domain;
using codemazepractice.domain.DTO;
using codemazepractice.persistence.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobManager _blobManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public OwnerController(IUnitOfWork unitOfWork, IBlobManager blobManager, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _blobManager = blobManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        var owners = await _unitOfWork.OwnerRepository.FindAllAsync();
        var ownersDto = _mapper.Map<List<OwnerDto>>(owners);

        return Ok(ownersDto);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccountsBasedOnType()
    {
        return Ok(await _unitOfWork.OwnerRepository.GetAllOwnersWithAssociations());
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

    [HttpPut("image")]
    public async Task<IActionResult> UpdateImage(Guid ownerID, IFormFile formFile)
    {
        string blobServerUrl = $"https://{_configuration["AzureStorageAccountName"]}.blob.core.windows.net";
        bool IfExists = await _unitOfWork.OwnerRepository.ExistsAsync(ownerID);
        if(IfExists)
        {
            Owner? owner = await _unitOfWork.OwnerRepository.FindOneAsync(ownerID);
            var result = await _blobManager.UploadBlob("blobcontainer", formFile, ownerID);
            if(result != null)
            {
                var ownerDto = await _unitOfWork.OwnerRepository.GetOwnerWithAssociations(ownerID);
                return Ok(ownerDto);
            }
            else
            {
                return StatusCode(500, "Failed to upload");
            }
        }
        else
        {
            return NotFound("Owner not found");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PathchOwner(Guid id, [FromBody] JsonPatchDocument<Owner> ownerModel)
    {
        var ownerDto = await _unitOfWork.OwnerRepository.PatchOwnerProperties(id, ownerModel);

        return Ok(ownerDto);
    }
}