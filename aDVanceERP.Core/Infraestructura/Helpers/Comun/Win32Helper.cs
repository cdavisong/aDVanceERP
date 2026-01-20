using System.Runtime.InteropServices;

namespace aDVanceERP.Core.Infraestructura.Helpers.Comun {
    public static class Win32 {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern nint CreateWindowEx(
            int dwExStyle,
            string lpClassName,
            string lpWindowName,
            uint dwStyle,
            int x, int y, int nWidth, int nHeight,
            nint hWndParent,
            nint hMenu,
            nint hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(nint hwnd);

        [DllImport("kernel32.dll")]
        public static extern nint GetModuleHandle(string lpModuleName);

        public const uint WS_POPUP = 0x80000000;
        public const int CW_USEDEFAULT = unchecked((int) 0x80000000);
    }
}
