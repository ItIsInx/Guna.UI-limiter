public enum LogLevel
{
    Info,
    Warn,
    Success,
    Failure
}
public class Logger
{
    private readonly object _lock = new();
    public void Log(LogLevel level, string message)
    {
        lock (_lock)
        {
            var originalColor = Console.ForegroundColor;
            switch (level)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogLevel.Failure:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine($"         {DateTime.Now} - {level}: {message}");
            Console.ForegroundColor = originalColor;
        }
    }
}