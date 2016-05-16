using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class Display
    {
        internal static byte[] ConvertToMonochrome(byte[] bitmap)
        {
            byte[] monochromePixels = new byte[bitmap.Length / 4];

            for (int ii = 0; ii < (int)(LcdProxy.LOGI_LCD_MONO_HEIGHT) * (int)LcdProxy.LOGI_LCD_MONO_WIDTH; ii++)
            {
                monochromePixels[ii] = bitmap[ii * 4];
            }

            return monochromePixels;
        }
        public bool UpdateDisplay(List<string> lines)
        {
            try
            {
                foreach (var line in lines.Select((value, i) => new {i, value}))
                {
                    LcdProxy.LogiLcdMonoSetText(line.i, line.value);
                }

                LcdProxy.LogiLcdUpdate();

                return true;
            }
            catch (Exception)
            {
                    
                return false;
            }
        }
    }
}
