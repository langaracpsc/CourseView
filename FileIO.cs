namespace CourseView;

public class FileIO
{
    public static string ReadFile(string path)
    {
        return new StreamReader(path).ReadToEnd();
    }
}