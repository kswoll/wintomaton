﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using Clipt.Keyboards;

namespace Clipt.WinApis
{
    public class WinApi
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern KeyStateShort GetKeyState(KeyCode key);

        [DllImport("user32.dll")]
        internal static extern bool GetKeyboardState(KeyStateByte[] keyState);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] ModifierKeys fsModifiers, [In] uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint numberOfInputs, Input[] inputs, int sizeOfInputStructure);

        [DllImport("user32.dll")]
        internal static extern IntPtr PostMessage(IntPtr hWnd, WindowMessage message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, WindowMessage message, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelHookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern VkKeyScanExResult VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern VkKeyScanExResult VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetKeyboardLayout(int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MapVirtualKey(uint key, MapVirtualKeyType mapType);

        [DllImport("user32.dll")]
        internal static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool EnumWindows(EnumThreadDelegate lpfn, IntPtr lParam);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd">The handle to the window</param>
        /// <param name="lpdwProcessId">Writes the process id</param>
        /// <returns>Thread id</returns>
        [DllImport("user32.dll", SetLastError=true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError=true)]
        internal static extern int GetWindowText(IntPtr hwnd, [Out] StringBuilder buffer, int maxCount);

        [DllImport("user32.dll", SetLastError=true)]
        internal static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError=true)]
        internal static extern bool IsWindow(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError=true)]
        internal static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError=true)]
        internal static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("psapi.dll", SetLastError=true)]
        internal static extern uint GetProcessImageFileName(IntPtr processHandle, StringBuilder buffer, uint size);

        public const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(int access, uint inheritHandle, uint processId);

        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
    }
}