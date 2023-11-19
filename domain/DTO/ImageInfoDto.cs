namespace codemazepractice.domain.DTO;

public class ImageInfoDto
{
    public Guid ImageID { get; set; }
    public string BlobName { get; set; } = string.Empty;
    public string BlobType { get; set; } = string.Empty;
    public DateOnly DateCreated { get; set; }
    public string Etag { get; set; } = string.Empty;
    public string BlobSize { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
    public string AccessTier { get; set; } = string.Empty;
    public string BlobContentType { get; set; } = string.Empty;
}