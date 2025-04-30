namespace Domain.DTOs;

public class FotoUploadRequest
{
    public byte[] imageBytes { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
