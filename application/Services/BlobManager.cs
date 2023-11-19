using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using codemazepractice.domain;
using codemazepractice.persistence.Contracts;
using Microsoft.AspNetCore.Http;

namespace codemazepractice.application.Services;

public class BlobManager : IBlobManager
{
    private readonly BlobServiceClient _serviceClient;
    private readonly IUnitOfWork _unitOfWork;

    public BlobManager(BlobServiceClient serviceClient, IUnitOfWork unitOfWork)
    {
        _serviceClient = serviceClient;
        _unitOfWork = unitOfWork;
    }

    private BlobContainerClient GetBlobContainerClient(string containerName)
    {
        return _serviceClient.GetBlobContainerClient(containerName);
    }

    public async Task<IEnumerable<ImageInfo>?> GetAllBlobsAsync(string containerName)
    {
        BlobContainerClient containerClient = GetBlobContainerClient(containerName);
        List<ImageInfo>? blobsList = new List<ImageInfo>();

        try
        {
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                blobsList.Add(new ImageInfo
                {
                    BlobName = blobItem.Name,
                    BlobType = blobItem.Properties.BlobType.Value.ToString(),
                    Etag = blobItem.Properties.ETag.Value.ToString(),
                    AccessTier = blobItem.Properties.AccessTier.Value.ToString(),
                    BlobContentType = blobItem.Properties.ContentType,
                    BlobSize = blobItem.Properties.ContentLength.ToString(),
                    BlobUrl = "",
                    DateCreated = DateOnly.FromDateTime(blobItem.Properties.CreatedOn.Value.DateTime)
                });
            }
        }
        catch (Exception)
        {
            blobsList = null;
        }

        return blobsList;
    }

    public async Task<ImageInfo?> UploadBlob(string containerName, IFormFile formFile, Guid ownerID)
    {
        ImageInfo? result = default!;

        BlobContainerClient containerClient = GetBlobContainerClient(containerName);

        try
        {
            BlobClient blobClient = containerClient.GetBlobClient(formFile.FileName);
            await blobClient.UploadAsync(formFile.OpenReadStream());
            BlobProperties properties = await blobClient.GetPropertiesAsync();
            var imageInfo = new ImageInfo { AccessTier = properties.AccessTier, BlobContentType = properties.ContentType, BlobName = formFile.FileName, BlobSize = properties.ContentLength.ToString(), BlobType = properties.BlobType.ToString(), BlobUrl = $"{containerClient.Uri.ToString()}/{formFile.FileName}", DateCreated = DateOnly.FromDateTime(properties.CreatedOn.DateTime), Etag = properties.ETag.ToString(), OwnerID = ownerID};
            await _unitOfWork.ImageInfoRepository.CreateAsync(imageInfo);
            await _unitOfWork.SaveChangesAsync();
            result = imageInfo;
        }
        catch (Exception)
        {
            result = null;
        }

        return result;
    }


}