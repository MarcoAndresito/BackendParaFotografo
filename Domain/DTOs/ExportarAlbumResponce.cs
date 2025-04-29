namespace Domain.DTOs;

public class ExportarAlbumResponce
{
    public string Message;
    public string FilePath;

    FileStream archivComprimido { get; set; }
    byte[] archivComprimido2 { get; set; }
    string archivComprimido3 { get; set; }
    public string Message { get; set; }
    public string FilePath { get; set; }
}
