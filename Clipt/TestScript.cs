﻿using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Clipt.Keyboards;
using Clipt.WinApis;

namespace Clipt
{
    public class TestScript : Script
    {
        private static HotKeyHandler someHotKey = SomeHotKey;

        public override void Run()
        {
            EnableKeyboardHook();

            Keyboard.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Alt, KeyCode.V, () =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = Text.TrimIndent(text);
                Clipboard.Paste(text);
            });

            Keyboard.AddShortcut(
                ModifierKeys.Alt | ModifierKeys.Ctrl | ModifierKeys.Shift | ModifierKeys.Windows,
                KeyCode.Escape,
                () => Application.Current.Shutdown());

//            Keyboard.RegisterStroke(new KeyStroke(KeyCode.LeftControl, KeyCode.LeftMenu, KeyCode.R), stroke => Debug.WriteLine("Stroked!"));

            Keyboard.RegisterStroke(new KeyStroke(KeyCode.ExtraButton2), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.Home);
                Keyboard.SendKeyUp(KeyCode.Control);
            });
            Keyboard.RegisterStroke(new KeyStroke(KeyCode.ExtraButton1), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.End);
                Keyboard.SendKeyUp(KeyCode.Control);
            });

            Keyboard.AddHotKey(new HotKey(KeyCode.OEM3, KeyCode.LeftMenu), hotKey =>
            {
                var activeWindow = WinApi.GetForegroundWindow();
                var activeThread = WinApi.GetWindowThreadProcessId(activeWindow, out var processId);
                var ourProcess = Process.GetProcessById((int)processId);
                var builder2 = new StringBuilder(255);
                var result = WinApi.OpenProcess(WinApi.PROCESS_ALL_ACCESS, 0, processId);
                WinApi.GetProcessImageFileName((uint)result, builder2, 255);
                bool closed = WinApi.CloseHandle(result);
                var ourProcessName = builder2.ToString();

                IntPtr nextWindow = IntPtr.Zero;
                IntPtr lastWindow = IntPtr.Zero;
                WinApi.EnumWindows(
                    (wnd, param) =>
                    {
/*
                        var builder = new StringBuilder(255);
                        WinApi.GetWindowText(wnd, builder, 255);
                        Debug.WriteLine(builder);
                        Debug.WriteLine(wnd);
*/

                        if (!WinApi.IsWindowVisible(wnd))
                            return true;

                        WinApi.GetWindowThreadProcessId(wnd, out var newProcessId);

                        var result2 = WinApi.OpenProcess(WinApi.PROCESS_ALL_ACCESS, 0, newProcessId);
                        WinApi.GetProcessImageFileName((uint)result2, builder2, 255);
                        bool closed2 = WinApi.CloseHandle(result2);

                        if (builder2.ToString() != ourProcessName)
                            return true;

                        nextWindow = wnd;

                        if (lastWindow == activeWindow)
                        {
                            WinApi.SetForegroundWindow(wnd);
                            return false;
                        }

                        lastWindow = wnd;
                        return true;
                    },
                    IntPtr.Zero);
            });

            Keyboard.AddHotKey(new HotKey(KeyCode.Left, KeyCode.F24), new KeyStroke(KeyCode.Home));
            Keyboard.AddHotKey(new HotKey(KeyCode.Right, KeyCode.F24), new KeyStroke(KeyCode.End));
            Keyboard.AddHotKey(new HotKey(KeyCode.Up, KeyCode.F24), new KeyStroke(KeyCode.Prior));
            Keyboard.AddHotKey(new HotKey(KeyCode.Down, KeyCode.F24), new KeyStroke(KeyCode.Next));

//            Keyboard.ReplaceKey(KeyCode.N1, KeyCode.N2);
//            Keyboard.ReplaceKey(KeyCode.N2, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.LeftButton, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.RightButton, KeyCode.G);

//            var layout = WinApi.GetKeyboardLayout(0);
//            var result = WinApi.VkKeyScanEx('D', layout);
//            var lowOrderByte = BitUtils.GetLowOrderByte(result);
//            var highOrderByte = BitUtils.GetHighOrderByte(result);

            KeySequence.FromString("tEst").Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("hello");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Register(keys => Debug.WriteLine("Success"));
        }

        private static void SomeHotKey(HotKey hotKey)
        {

        }
    }
}