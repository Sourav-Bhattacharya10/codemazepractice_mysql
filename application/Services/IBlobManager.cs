using codemazepractice.domain;
using Microsoft.AspNetCore.Http;

namespace codemazepractice.application.Services;

public interface IBlobManager
{
    Task<IEnumerable<ImageInfo>?> GetAllBlobsAsync(string containerName);
    Task<ImageInfo?> UploadBlob(string containerName, IFormFile formFile, Guid ownerID);
}