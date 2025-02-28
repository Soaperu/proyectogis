using Serilog;
using Sigcatmin.pro.Application.Interfaces; 

namespace Sigcatmin.pro.Shared.Implements
{
    public class LoggerService : ILoggerService
    {
        public void LogError(Exception ex, string message = "")
        {
            Log.Error(message, ex);
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }
    }
}
