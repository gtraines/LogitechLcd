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
            
            LcdProxy.LogiLcdInit(AppFriendlyName, LcdProxy.LOGI_LCD_TYPE_MONO);
            Success = LcdProxy.LogiLcdIsConnected(LcdProxy.LOGI_LCD_TYPE_MONO);

        }

        public string AppFriendlyName { get; set; }

        public bool Success { get; set; }

        public bool LedSuccess { get; set; }


        public static async Task BlueLedEffect(int effectLength)
        {

            var countdown = new Stopwatch();
            LedProxy.LogiLedPulseLighting(0, 100, 0, effectLength, 200);
            countdown.Start();
            while (countdown.ElapsedMilliseconds < effectLength)
            {
                
            }
        }

        public static async Task RedLedEffect(int effectLength)
        {

            var countdown = new Stopwatch();
            LedProxy.LogiLedFlashLighting(0, 100, 0, effectLength, 100);
            countdown.Start();
            while (countdown.ElapsedMilliseconds < effectLength)
            {

            }

        }
    }
}
