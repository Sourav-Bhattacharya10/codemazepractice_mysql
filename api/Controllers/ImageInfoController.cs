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

public class ImageInfoController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobManager _blobManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ImageInfoController(IUnitOfWork unitOfWork, IBlobManager blobManager, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _blobManager = blobManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        return Ok(await _unitOfWork.ImageInfoRepository.FindAllAsync());
    }
}