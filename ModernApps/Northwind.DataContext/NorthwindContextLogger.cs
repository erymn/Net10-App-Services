namespace Northwind.DataContext;

public class NorthwindContextLogger
{
    public static void WriteLine(string message)
    {
        string folder  = Path.Combine(GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "book-logs");
        
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string dateTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string path = Path.Combine(folder, $"northwindlog-{dateTimeStamp}.txt");
        
        StreamWriter textFile = File.AppendText(path);
        textFile.WriteLine(message);
        textFile.Close();
    }

    private static string GetFolderPath(Environment.SpecialFolder desktopDirectory)
    {
        return desktopDirectory.ToString();
    }
}