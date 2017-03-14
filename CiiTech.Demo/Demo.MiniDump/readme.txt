MiniDump demo:
1.捕获程序运行异常（ThreadException）和未处理的异常（UnhandledException）
    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CD_UnhandledException);
    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(App_ThreadException);

2.将异常的堆栈信息保存到Dump.txt文件中
    string path = Path.Combine(Application.StartupPath, "Dumps\\");
    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    path = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

    string msg = "An unhandled exception occurred. \r\n\r\nError message: " +
                         e.Message + "\r\n\r\nStack Trace: " + e.StackTrace;

    string runingBit = "Running as " + (IntPtr.Size == 4 ? "32-bit" : "64-bit");

    Process process = Process.GetCurrentProcess();
    string swVersion = Application.ProductVersion + " Build Date: " +
                    System.IO.File.GetLastWriteTime(Application.ExecutablePath).ToCSTTime().ToString("yyyy-MM-dd HH:mm:ss") + "    " + runingBit
                        + "\r\n\r\nMemory: " + process.PrivateMemorySize64 / 1024 / 1024
                        + "    GDI Objects: " + GetGuiResources(process.Handle, 0)
                        + "    User Objects: " + GetGuiResources(process.Handle, 1);

    if (e is AggregateException) //handle task wrapped exceptions
    {
    	foreach (var inner in (e as AggregateException).InnerExceptions) msg += inner.Message + "\r\n\r\n";
    }

    LogStackTrace(path + ".txt", swVersion + "    "  + "\r\n\r\n" + msg);

