using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public static class Proxy
    {
        //LCD SDK 
        public const int LOGI_LCD_MONO_BUTTON_0 = (0x00000001);
        public const int LOGI_LCD_MONO_BUTTON_1 = (0x00000002);
        public const int LOGI_LCD_MONO_BUTTON_2 = (0x00000004);
        public const int LOGI_LCD_MONO_BUTTON_3 = (0x00000008);
        public const int LOGI_LCD_MONO_WIDTH = 160;
        public const int LOGI_LCD_MONO_HEIGHT = 43;
        public const int LOGI_LCD_TYPE_MONO = (0x00000001);


        /// <summary>
        /// Function that should be called when the user wants to configure your 
        /// application. If no configuration panel is provided or needed, 
        /// leave this parameter NULL.
        /// </summary>
        /// <param name="connection">Current connection</param>
        /// <param name="pContext">Current context</param>
        /// <returns></returns>
        public delegate int lgLcdOnConfigureCB(int connection, IntPtr pContext);
        /// <summary>
        /// Function that should be called when the state of the soft buttons changes. 
        /// If no notification is needed, leave this parameter NULL.
        /// </summary>
        /// <param name="device">Device sending buttons</param>
        /// <param name="dwButtons">Mask showing which buttons were pressed</param>
        /// <param name="pContext">Current context</param>
        /// <returns></returns>
		public delegate int lgLcdOnSoftButtonsCB(int device, int dwButtons, IntPtr pContext);

        /// <summary>
        /// The lgLcdDeviceDesc structure describes the properties of an attached device. 
        /// This information is returned through a call to lgLcdEnumerate().
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdDeviceDesc
        {
            /// <summary>
            /// Specifies the width of the display in pixels.
            /// </summary>
			public int Width;
            /// <summary>
            /// Specifies the height of the display in pixels.
            /// </summary>
            public int Height;
            /// <summary>
            /// Specifies the depth of the bitmap in bits per pixel.
            /// </summary>
			public int Bpp;
            /// <summary>
            /// Specifies the number of soft buttons that the device has.
            /// </summary>
            public int NumSoftButtons;
        }

        /// <summary>
        /// The lgLcdBitmapHeader exists at the beginning of any bitmap structure 
        /// defined in lgLcd. Following the header is the actual bitmap as an array 
        /// of bytes, as illustrated by lgLcdBitmap160x43x1.
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdBitmapHeader
        {
            /// <summary>
            /// Specifies the format of the structure following the header. 
            /// Currently, only LGLCD_BMP_FORMAT_160x43x1 is supported
            /// </summary>
    		public uint Format;
        }

        /// <summary>
        /// 160x43x1 bitmap.  This includes a header and an array
        /// of bytes (1 for each pixel.)
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdBitmap160x43x1
        {
            /// <summary>
            /// Header information telling what kind of bitmap this structure
            /// represents (currently only one format exists, see lgLcdBitmapHeader.)
            /// </summary>
			public lgLcdBitmapHeader hdr;
            /// <summary>
            /// Contains the display bitmap with 160x43 pixels. Every byte represents
            /// one pixel, with &gt;=128 being “on” and &lt;128 being “off”.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6880)]
            public byte[] pixels;
        }

        /// <summary>
        /// The lgLcdConfigureContext is part of the lgLcdConnectContext and 
        /// is used to give the library enough information to allow the user 
        /// to configure your application. The registered callback is called when the user 
        /// clicks the “Configure…” button in the LCDMon list of applications.
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdConfigureContext
        {
            /// <summary>
            /// Specifies a pointer to a function that should be called when the 
            /// user wants to configure your application. If no configuration panel 
            /// is provided or needed, leave this parameter NULL.
            /// </summary>
			public lgLcdOnConfigureCB configCallback;
            /// <summary>
            /// Specifies an arbitrary context value of the application that is passed
            /// back to the client in the event that the registered configCallback 
            /// function is invoked.
            /// </summary>
			public IntPtr configContext;
        }

        /// <summary>
        /// The lgLcdConnectContext contains all the information that is needed to 
        /// connect your application to LCDMon through lgLcdConnect(). Upon successful connection, 
        /// it also contains the connection handle that has to be used in subsequent calls to 
        /// lgLcdEnumerate() and lgLcdOpen().
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdConnectContext
        {
            /// <summary>
            /// Specifies a string that contains the “friendly name” of your application. 
            /// This name is presented to the user whenever a list of applications is shown.
            /// </summary>
			public string appFriendlyName;
            /// <summary>
            /// Specifies whether your connection is temporary (.isPersistent = FALSE) or 
            /// whether it is a regular connection that should be added to the list 
            /// (.isPersistent = TRUE).
            /// </summary>
			public bool isPersistent;
            /// <summary>
            /// Specifies whether your application can be started by LCDMon or not.
            /// </summary>
			public bool isAutostartable;
            /// <summary>
            /// Specifies context that is necessary to call back for configuration of 
            /// your application. See lgLcdConfigureContext for more details.
            /// </summary>
			public lgLcdConfigureContext onConfigure;
            /// <summary>
            /// Upon successful connection, this member holds the “connection handle” 
            /// which is used in subsequent calls to lgLcdEnumerate() and lgLcdOpen(). 
            /// A value of LGLCD_INVALID_CONNECTION denotes an invalid connection.
            /// </summary>
			public int connection;
        }

        /// <summary>
        /// The lgLcdSoftbuttonsChangedContext is part of the lgLcdOpenContext and 
        /// is used to give the library enough information to allow changes in the 
        /// state of the soft buttons to be signaled into the calling application 
        /// through a callback.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct lgLcdSoftbuttonsChangedContext
        {
            /// <summary>
            /// Specifies a pointer to a function that should be called when the 
            /// state of the soft buttons changes. If no notification is needed, 
            /// leave this parameter NULL.
            /// </summary>
			public lgLcdOnSoftButtonsCB softbuttonsChangedCallback;
            /// <summary>
            /// Specifies an arbitrary context value of the application that is 
            /// passed back to the client in the event that soft buttons are being 
            /// pressed or released. The new value of the soft buttons is reported 
            /// in the dwButtons parameter of the callback function.
            /// </summary>
            public IntPtr softbuttonsChangedContext;
        }

        /// <summary>
        /// The lgLcdOpenContext contains all the information that is needed to open 
        /// a specified LCD display through lgLcdOpen(). Upon successful completion 
        /// of the open it contains the device handle that has to be used in subsequent 
        /// calls to lgLcdReadSoftButtons(), lgLcdUpdateBitmap() and lgLcdClose().
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
        public struct lgLcdOpenContext
        {
            /// <summary>
            /// Specifies the connection (previously opened through lgLcdConnect()) which 
            /// this lgLcdOpen() call is for.
            /// </summary>
			public int connection;
            /// <summary>
            /// Specifies the index of the device to open (see lgLcdEnumerate() for details).
            /// </summary>
			public int index;
            /// <summary>
            /// Specifies the details for the callback function that should be invoked when
            /// device has changes in its soft button status, i.e. the user has pressed or
            /// a soft button. For details, see lgLcdSoftbuttonsChangedContext.
            /// </summary>
			public lgLcdSoftbuttonsChangedContext onSoftbuttonsChanged;
            /// <summary>
            /// Upon successful opening, this member holds the device handle which is used 
            /// in subsequent calls to lgLcdReadSoftButtons(), lgLcdUpdateBitmap() and 
            /// lgLcdClose(). A value of LGLCD_INVALID_DEVICE denotes an invalid device.
            /// </summary>
			public int device;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
		public static uint LGLCD_SYNC_UPDATE(uint priority) { return 0x80000000 | priority; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
		public static uint LGLCD_ASYNC_UPDATE(uint priority) { return priority; }

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
    }
}
