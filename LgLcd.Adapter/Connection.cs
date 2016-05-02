using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class Connection
    {
        public Connection(string appFriendlyName="testapp")
        {
            AppFriendlyName = appFriendlyName;
            
            Proxy.LogiLcdInit(AppFriendlyName, Proxy.LOGI_LCD_TYPE_MONO);
            Success = Proxy.LogiLcdIsConnected(Proxy.LOGI_LCD_TYPE_MONO);
        }

        public string AppFriendlyName { get; set; }

        public bool Success { get; set; }
        
    }
}
