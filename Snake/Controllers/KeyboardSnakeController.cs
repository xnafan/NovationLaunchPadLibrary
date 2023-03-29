//using NESControllerLibrary;
//using System.Diagnostics;
//using System.Runtime.InteropServices;

//namespace Snake.Controllers
//{
//    internal class KeyboardSnakeController : SnakeControllerBase
//    {


        
//        private const int WH_KEYBOARD_LL = 13;
//        private const int WM_KEYDOWN = 0x0100;

//        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

//        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

//        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

//        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

//        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        private static extern IntPtr GetModuleHandle(string lpModuleName);


//        private static LowLevelKeyboardProc _proc = HookCallback;
//        private static IntPtr _hookID = IntPtr.Zero;

//        public static void KeyboardSnakeController()
//        {
//            using (Process curProcess = Process.GetCurrentProcess())
//            using (ProcessModule curModule = curProcess.MainModule)
//            {
//                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
//                Console.WriteLine("Press any key to exit.");
//                Console.ReadKey();
//                UnhookWindowsHookEx(_hookID);
//            }
//        }


//        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
//        {
//            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
//            {
//                int vkCode = Marshal.ReadInt32(lParam);
//                Console.WriteLine("Key pressed: " + ((Keys)vkCode).ToString());
//            }
//            return CallNextHookEx(_hookID, nCode, wParam, lParam);
//        }
//        private void _controller_ButtonStateChanged(object? sender, NESControllerEventArgs e)
//        {
//            if (e.Action == NESControllerButtonAction.Pressed)
//            {
//                switch (e.Button)
//                {
//                    case NESControllerButton.Up:
//                        OnButtonChange(Direction.Up);
//                        break;
//                    case NESControllerButton.Down:
//                        OnButtonChange(Direction.Down);
//                        break;
//                    case NESControllerButton.Left:
//                        OnButtonChange(Direction.Left);
//                        break;
//                    case NESControllerButton.Right:
//                        OnButtonChange(Direction.Right);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//    }
//}