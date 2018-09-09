
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface ILogger
    {
        void LogInfo(string controller, string route, string message);
        void LogError(string controller, string route, string errorMessage);
     //   void SendLog(LogModel lm);
    }
}
