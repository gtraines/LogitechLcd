using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LgLcdG13.Adapter
{
    public class Display
    {
        internal static byte[] ConvertToMonochrome(byte[] bitmap)
        {
            byte[] monochromePixels = new byte[bitmap.Length / 4];

            for (int ii = 0; ii < (int)(Constants.LOGI_LCD_MONO_HEIGHT) * (int)Constants.LOGI_LCD_MONO_WIDTH; ii++)
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


        public void LedEffect(Color color, EffectType effectType, int effectLength, int effectInterval)
        {
            LedProxy.LogiLedStopEffects();
            Thread.Sleep(100);
            LedProxy.LogiLedSetLighting(Constants.DEFAULT_RED_PCT, Constants.DEFAULT_GREEN_PCT, Constants.DEFAULT_BLUE_PCT);
            Thread.Sleep(100);

            switch (effectType)
            {
                case (EffectType.FLASH):
                    LedProxy
                        .LogiLedFlashLighting(
                            color.Red, 
                            color.Green, 
                            color.Blue, 
                            effectLength, 
                            effectInterval);
                    break;
                case (EffectType.PULSE):
                    LedProxy
                        .LogiLedPulseLighting(
                            color.Red, 
                            color.Green, 
                            color.Blue, 
                            effectLength, 
                            effectInterval);
                    break;
            }

            Task.Run(() => ResetLightingAfterEffect(effectLength));
        }

        public async Task ResetLightingAfterEffect(int effectLength)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < effectLength)
            {
                Thread.Sleep(50);
            }
            LedProxy.LogiLedSetLighting(Constants.DEFAULT_RED_PCT, Constants.DEFAULT_GREEN_PCT, Constants.DEFAULT_BLUE_PCT);
        }
    }
}
