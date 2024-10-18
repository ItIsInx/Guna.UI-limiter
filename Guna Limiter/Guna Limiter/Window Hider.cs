using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class WindowHider
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    private const int SW_HIDE = 0;
    public static void HideGunaDialog()
    {
        Logger logger = new();
        bool hasLoggedSuccess = false;
        bool hasLoggedWarn = false;

        Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    EnumWindows((hWnd, lParam) =>
                    {
                        StringBuilder windowText = new(256);
                        GetWindowText(hWnd, windowText, windowText.Capacity);

                        var processes = Process.GetProcesses();
                        foreach (var p in processes)
                        {
                            if (windowText.ToString().Contains("Guna.UI2", StringComparison.OrdinalIgnoreCase) &&
                                p.ProcessName == "devenv")
                            {
                                ShowWindow(hWnd, SW_HIDE);
                                if (!hasLoggedSuccess)
                                {
                                    logger.Log(LogLevel.Success, " => Guna has been limited!");
                                    hasLoggedSuccess = true;
                                }
                                return false;
                            }
                        }

                        return true;

                    }, IntPtr.Zero);

                    if (!hasLoggedSuccess && !hasLoggedWarn)
                    {
                        logger.Log(LogLevel.Warn, " => Didn't find Guna window!");
                        hasLoggedWarn = true;
                    }

                }
                catch{
                }

                Thread.Sleep(1000);
            }
        });
    }
}