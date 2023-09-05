//using updateApi.interfaces;

namespace updateApi.Services
{   
public class LogService :IOLogServices
{
    private readonly string filePath;
    public LogService(IWebHostEnvironment web)
    {
        filePath = Path.Combine(web.ContentRootPath, "Logs", "logs.log")      ;
    }
    public void Log(LogLevel level, string message)
    {
        using (var sr = new StreamWriter(filePath, true))
        {
            sr.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} [{level}] {message}");
        }

    }

}
}
