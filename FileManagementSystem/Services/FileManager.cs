namespace FileManagementSystem.Services;

using FileManagementSystem.Models;
using System.IO;

// Core file management service
public class FileManager
{
    private string _currentDirectory;

    public FileManager()
    {
        _currentDirectory = Directory.GetCurrentDirectory();
    }

    public void ShowCurrentDirectory()
    {
        Console.WriteLine($"📂 Current Directory: {_currentDirectory}");
        Console.WriteLine($"📊 Drive Info: {Path.GetPathRoot(_currentDirectory)}");
        
        var driveInfo = new DriveInfo(Path.GetPathRoot(_currentDirectory)!);
        Console.WriteLine($"💾 Total Space: {FormatBytes(driveInfo.TotalSize)}");
        Console.WriteLine($"🆓 Available Space: {FormatBytes(driveInfo.AvailableFreeSpace)}");
        Console.WriteLine($"📈 Used Space: {FormatBytes(driveInfo.TotalSize - driveInfo.AvailableFreeSpace)}");
    }

    public void ListFiles()
    {
        try
        {
            var files = GetFileList(_currentDirectory);
            
            if (files.Count == 0)
            {
                Console.WriteLine("📭 No files found in current directory.");
                return;
            }

            Console.WriteLine($"\n📋 Files in {Path.GetFileName(_currentDirectory)}:");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Name",-30} {"Size",-12} {"Type",-20} {"Modified",-15}");
            Console.WriteLine(new string('-', 80));

            foreach (var file in files.OrderBy(f => f.IsDirectory).ThenBy(f => f.Name))
            {
                var icon = file.IsDirectory ? "📁" : "📄";
                var name = file.Name.Length > 28 ? file.Name[..25] + "..." : file.Name;
                Console.WriteLine($"{icon} {name,-28} {file.SizeFormatted,-12} {file.FileType,-20} {file.ModifiedDate:yyyy-MM-dd HH:mm}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error listing files: {ex.Message}");
        }
    }

    public void SearchFiles()
    {
        Console.Write("🔍 Enter search term: ");
        var searchTerm = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine("❌ Search term cannot be empty.");
            return;
        }

        try
        {
            var allFiles = GetAllFilesRecursively(_currentDirectory);
            var matchingFiles = allFiles.Where(f => 
                f.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                f.Extension.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (matchingFiles.Count == 0)
            {
                Console.WriteLine($"🔍 No files found matching '{searchTerm}'.");
                return;
            }

            Console.WriteLine($"\n🔍 Found {matchingFiles.Count} files matching '{searchTerm}':");
            foreach (var file in matchingFiles)
            {
                var icon = file.IsDirectory ? "📁" : "📄";
                Console.WriteLine($"{icon} {file.FullPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error searching files: {ex.Message}");
        }
    }

    public void CreateFile()
    {
        Console.Write("📝 Enter file name: ");
        var fileName = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("❌ File name cannot be empty.");
            return;
        }

        try
        {
            var filePath = Path.Combine(_currentDirectory, fileName);
            
            if (File.Exists(filePath))
            {
                Console.WriteLine("❌ File already exists.");
                return;
            }

            Console.Write("📝 Enter file content (press Enter twice to finish): ");
            var lines = new List<string>();
            string? line;
            
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"✅ File '{fileName}' created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error creating file: {ex.Message}");
        }
    }

    public void DeleteFile()
    {
        Console.Write("🗑️  Enter file name to delete: ");
        var fileName = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("❌ File name cannot be empty.");
            return;
        }

        try
        {
            var filePath = Path.Combine(_currentDirectory, fileName);
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ File not found.");
                return;
            }

            Console.Write($"⚠️  Are you sure you want to delete '{fileName}'? (y/n): ");
            var confirm = Console.ReadLine()?.Trim().ToLower();
            
            if (confirm == "y" || confirm == "yes")
            {
                File.Delete(filePath);
                Console.WriteLine($"✅ File '{fileName}' deleted successfully!");
            }
            else
            {
                Console.WriteLine("❌ Deletion cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error deleting file: {ex.Message}");
        }
    }

    public void ShowFileInfo()
    {
        Console.Write("ℹ️  Enter file name: ");
        var fileName = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("❌ File name cannot be empty.");
            return;
        }

        try
        {
            var filePath = Path.Combine(_currentDirectory, fileName);
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ File not found.");
                return;
            }

            var fileInfo = new System.IO.FileInfo(filePath);
            var customFileInfo = CreateCustomFileInfo(fileInfo);
            
            Console.WriteLine($"\n📄 File Information for '{fileName}':");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Name: {customFileInfo.Name}");
            Console.WriteLine($"Full Path: {customFileInfo.FullPath}");
            Console.WriteLine($"Extension: {customFileInfo.Extension}");
            Console.WriteLine($"Type: {customFileInfo.FileType}");
            Console.WriteLine($"Size: {customFileInfo.SizeFormatted}");
            Console.WriteLine($"Created: {customFileInfo.CreatedDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Modified: {customFileInfo.ModifiedDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Last Accessed: {customFileInfo.LastAccessDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Read Only: {customFileInfo.IsReadOnly}");
            Console.WriteLine($"Hidden: {customFileInfo.IsHidden}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error getting file info: {ex.Message}");
        }
    }

    private List<FileInfo> GetFileList(string directory)
    {
        var files = new List<FileInfo>();
        
        try
        {
            // Add directories
            foreach (var dir in Directory.GetDirectories(directory))
            {
                var dirInfo = new System.IO.DirectoryInfo(dir);
                files.Add(new FileInfo
                {
                    Name = dirInfo.Name,
                    FullPath = dirInfo.FullName,
                    Extension = "",
                    SizeInBytes = 0,
                    CreatedDate = dirInfo.CreationTime,
                    ModifiedDate = dirInfo.LastWriteTime,
                    LastAccessDate = dirInfo.LastAccessTime,
                    Attributes = dirInfo.Attributes,
                    IsDirectory = true,
                    IsReadOnly = (dirInfo.Attributes & FileAttributes.ReadOnly) != 0,
                    IsHidden = (dirInfo.Attributes & FileAttributes.Hidden) != 0
                });
            }

            // Add files
            foreach (var file in Directory.GetFiles(directory))
            {
                var fileInfo = new System.IO.FileInfo(file);
                files.Add(CreateCustomFileInfo(fileInfo));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error reading directory: {ex.Message}");
        }

        return files;
    }

    private FileInfo CreateCustomFileInfo(System.IO.FileInfo fileInfo)
    {
        return new FileInfo
        {
            Name = fileInfo.Name,
            FullPath = fileInfo.FullName,
            Extension = fileInfo.Extension,
            SizeInBytes = fileInfo.Length,
            CreatedDate = fileInfo.CreationTime,
            ModifiedDate = fileInfo.LastWriteTime,
            LastAccessDate = fileInfo.LastAccessTime,
            Attributes = fileInfo.Attributes,
            IsDirectory = false,
            IsReadOnly = fileInfo.IsReadOnly,
            IsHidden = (fileInfo.Attributes & FileAttributes.Hidden) != 0
        };
    }

    private List<FileInfo> GetAllFilesRecursively(string directory)
    {
        var allFiles = new List<FileInfo>();
        
        try
        {
            var files = GetFileList(directory);
            allFiles.AddRange(files);

            foreach (var dir in Directory.GetDirectories(directory))
            {
                allFiles.AddRange(GetAllFilesRecursively(dir));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error reading directory recursively: {ex.Message}");
        }

        return allFiles;
    }

    private string FormatBytes(long bytes)
    {
        if (bytes < 1024) return $"{bytes} B";
        if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
        if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024.0 * 1024.0):F1} MB";
        return $"{bytes / (1024.0 * 1024.0 * 1024.0):F1} GB";
    }
}
