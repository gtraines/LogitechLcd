using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class G13Device
    {
        public Connection Connection { get; set; }
        public Display Display = new Display();

        public G13Device()
        {
            Connection = new Connection();
        }

        public G13Device(string appFriendlyName)
        {
            Connection = new Connection(appFriendlyName);
        }

        public bool UpdateDisplay(List<string> displayLines)
        {
            return Display.UpdateDisplay(displayLines);
        }

        public Func<int> Button0Handler { get; set; }
        public Func<int> Button1Handler { get; set; }
        public Func<int> Button2Handler { get; set; }
        public Func<int> Button3Handler { get; set; } 

        public int CheckButtons()
        {
            if (Proxy.LogiLcdIsButtonPressed(Proxy.LOGI_LCD_MONO_BUTTON_0))
            {
                return Button0Handler();
            }
            else if (Proxy.LogiLcdIsButtonPressed(Proxy.LOGI_LCD_MONO_BUTTON_1))
            {
                return Button1Handler();
            }
            else if (Proxy.LogiLcdIsButtonPressed(Proxy.LOGI_LCD_MONO_BUTTON_2))
            {
                return Button2Handler();
            }
            else if (Proxy.LogiLcdIsButtonPressed(Proxy.LOGI_LCD_MONO_BUTTON_3))
            {
                return Button3Handler();
            }
            else
            {
                return -1;
            }
        }
         
    }
}
