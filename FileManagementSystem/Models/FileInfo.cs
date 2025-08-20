namespace FileManagementSystem.Models;

// Custom file information model
public class FileInfo
{
    public string Name { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime LastAccessDate { get; set; }
    public FileAttributes Attributes { get; set; }
    public bool IsDirectory { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsHidden { get; set; }

    public string SizeFormatted
    {
        get
        {
            if (SizeInBytes < 1024)
                return $"{SizeInBytes} B";
            else if (SizeInBytes < 1024 * 1024)
                return $"{SizeInBytes / 1024.0:F1} KB";
            else if (SizeInBytes < 1024 * 1024 * 1024)
                return $"{SizeInBytes / (1024.0 * 1024.0):F1} MB";
            else
                return $"{SizeInBytes / (1024.0 * 1024.0 * 1024.0):F1} GB";
        }
    }

    public string FileType
    {
        get
        {
            if (IsDirectory)
                return "Directory";
            
            return Extension.ToLower() switch
            {
                ".txt" => "Text File",
                ".doc" or ".docx" => "Word Document",
                ".xls" or ".xlsx" => "Excel Spreadsheet",
                ".ppt" or ".pptx" => "PowerPoint Presentation",
                ".pdf" => "PDF Document",
                ".jpg" or ".jpeg" => "JPEG Image",
                ".png" => "PNG Image",
                ".gif" => "GIF Image",
                ".mp3" => "MP3 Audio",
                ".mp4" => "MP4 Video",
                ".zip" => "ZIP Archive",
                ".rar" => "RAR Archive",
                ".exe" => "Executable",
                ".dll" => "Dynamic Link Library",
                _ => "Unknown File Type"
            };
        }
    }

    public override string ToString()
    {
        var attributes = new List<string>();
        if (IsReadOnly) attributes.Add("ReadOnly");
        if (IsHidden) attributes.Add("Hidden");
        if (IsDirectory) attributes.Add("Directory");
        
        var attrString = attributes.Count > 0 ? $" [{string.Join(", ", attributes)}]" : "";
        
        return $"{Name} ({SizeFormatted}){attrString}";
    }
}
