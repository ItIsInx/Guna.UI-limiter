Logger logger = new();
Console.Title = "Guna Limiter by itisinx";
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine
(
    @"

        _______                      _       _       _                   
       (_______)                    (_)     (_)     (_)  _               
        _   ___ _   _ ____  _____    _       _ ____  _ _| |_ _____  ____ 
       | | (_  | | | |  _ \(____ |  | |     | |    \| (_   _) ___ |/ ___)
       | |___) | |_| | | | / ___ |  | |_____| | | | | | | |_| ____| |    
        \_____/|____/|_| |_\_____|  |_______)_|_|_|_|_|  \__)_____)_|   --By itisinx 
     "
);
logger.Log(LogLevel.Info, " => Task has been started!");
bool isDetected = false;
Task thread = Task.Run(() =>
{
    if (!isDetected)
    {
        WindowHider.HideGunaDialog();
        isDetected = true;
    }
});
Console.ReadKey();