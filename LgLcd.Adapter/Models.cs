using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class Color
    {
        public Color()
        {

        }

        public Color(int redValue, int greenValue, int blueValue)
        {
            Red = redValue;
            Green = greenValue;
            Blue = blueValue;
        }

        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }
    }


    public enum EffectType
    {
        FLASH,
        PULSE
    }

    public class Constants
    {
        public const int DEFAULT_RED_PCT = 0;
        public const int DEFAULT_GREEN_PCT = 90;
        public const int DEFAULT_BLUE_PCT = 50;

        public const int LOGI_LCD_MONO_BUTTON_0 = (0x00000001);
        public const int LOGI_LCD_MONO_BUTTON_1 = (0x00000002);
        public const int LOGI_LCD_MONO_BUTTON_2 = (0x00000004);
        public const int LOGI_LCD_MONO_BUTTON_3 = (0x00000008);

        public const int LOGI_LCD_MONO_WIDTH = 160;
        public const int LOGI_LCD_MONO_HEIGHT = 43;
        public const int LOGI_LCD_TYPE_MONO = (0x00000001);
    }
}
