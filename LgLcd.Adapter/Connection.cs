using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace LgLcdG13.Adapter
{
    public class Connection
    {
        public Connection(string appFriendlyName="testapp")
        {
            AppFriendlyName = appFriendlyName;
            
            LcdProxy.LogiLcdInit(AppFriendlyName, Constants.LOGI_LCD_TYPE_MONO);
            Success = LcdProxy.LogiLcdIsConnected(Constants.LOGI_LCD_TYPE_MONO);
            if (Success)
            {
                LedSuccess = LedProxy.LogiLedInit();
            }
            

        }

        public string AppFriendlyName { get; set; }

        public bool Success { get; set; }

        public bool LedSuccess { get; set; }
    }
}
