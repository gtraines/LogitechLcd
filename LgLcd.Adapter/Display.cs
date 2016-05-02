using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public static class Display
    {

        public static bool UpdateDisplay(List<string> lines)
        {
            try
            {
                foreach (var line in lines.Select((value, i) => new {i, value}))
                {
                    Proxy.LogiLcdMonoSetText(line.i, line.value);
                }

                Proxy.LogiLcdUpdate();

                return true;
            }
            catch (Exception)
            {
                    
                return false;
            }
        }
    }
}
