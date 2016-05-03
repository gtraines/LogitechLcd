using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class Display
    {

        public bool UpdateDisplay(List<string> lines)
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
