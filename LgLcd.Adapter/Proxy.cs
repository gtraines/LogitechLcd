using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public static class LcdProxy
    {
        //LCD SDK
        #region LCD Funcs

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdInit(String friendlyName, int lcdType);

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdIsConnected(int lcdType);

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdIsButtonPressed(int button);

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLcdUpdate();

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLcdShutdown();

        // Monochrome LCD functions
        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdMonoSetBackground(byte[] monoBitmap);

        [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdMonoSetText(int lineNumber, String text);

        #endregion LCD Funcs
    }

    public static class LedProxy
    {

        //LED SDK
        private const int LOGI_DEVICETYPE_MONOCHROME_ORD = 0;

        private const int LOGI_DEVICETYPE_RGB_ORD = 1;
        private const int LOGI_DEVICETYPE_PERKEY_RGB_ORD = 2;

        public const int LOGI_DEVICETYPE_MONOCHROME = (1 << LOGI_DEVICETYPE_MONOCHROME_ORD);
        public const int LOGI_DEVICETYPE_RGB = (1 << LOGI_DEVICETYPE_RGB_ORD);
        public const int LOGI_DEVICETYPE_PERKEY_RGB = (1 << LOGI_DEVICETYPE_PERKEY_RGB_ORD);
        public const int LOGI_LED_BITMAP_WIDTH = 21;
        public const int LOGI_LED_BITMAP_HEIGHT = 6;
        public const int LOGI_LED_BITMAP_BYTES_PER_KEY = 4;

        public const int LOGI_LED_BITMAP_SIZE = LOGI_LED_BITMAP_WIDTH * LOGI_LED_BITMAP_HEIGHT * LOGI_LED_BITMAP_BYTES_PER_KEY;
        public const int LOGI_LED_DURATION_INFINITE = 0;

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedInit();

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetTargetDevice(int targetDevice);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSaveCurrentLighting();

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedRestoreLighting();

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedFlashLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedStopEffects();

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingFromBitmap(byte[] bitmap);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithScanCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithHidCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithQuartzCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithKeyName(keyboardNames keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSaveLightingForKey(keyboardNames keyName);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedRestoreLightingForKey(keyboardNames keyName);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedFlashSingleKey(keyboardNames keyName, int redPercentage, int greenPercentage, int bluePercentage, int msDuration, int msInterval);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedPulseSingleKey(keyboardNames keyName, int startRedPercentage, int startGreenPercentage, int startBluePercentage, int finishRedPercentage, int finishGreenPercentage, int finishBluePercentage, int msDuration, bool isInfinite);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedStopEffectsOnKey(keyboardNames keyName);

        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLedShutdown();


    }
}