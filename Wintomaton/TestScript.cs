﻿using System.Diagnostics;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Wintomaton.Inputs;
using Wintomaton.Utils;
using Wintomaton.WinApis;
using Wintomaton.Windows;

namespace Wintomaton
{
    public class TestScript : Script
    {
        public override void Run()
        {
            EnableKeyboardHook();
            EnableMouseHook();

//            Clipboard.Changed += () => Tray.Icon.ShowBalloonTip("Copied!", Clipboard.GetText(), BalloonIcon.Info);

            Mouse.AddHotMouse(new MouseEvent(MouseEventType.WheelUp, KeyCode.LeftControl), Actions.Nothing);
            Mouse.AddHotMouse(new MouseEvent(MouseEventType.WheelDown, KeyCode.LeftControl), Actions.Nothing);

            Keyboard.AddShortcut(new Shortcut(KeyCode.V, KeyCode.LeftControl, KeyCode.LeftWindows), _ =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = Text.TrimIndent(text);
                Clipboard.Paste(text);
            });

            Keyboard.AddHotKey(KeyCode.Escape,
                ModifierKeys.Alt | ModifierKeys.Ctrl | ModifierKeys.Shift | ModifierKeys.Windows, () => Application.Current.Shutdown());

//            Keyboard.RegisterStroke(new KeyStroke(KeyCode.LeftControl, KeyCode.LeftMenu, KeyCode.R), stroke => Debug.WriteLine("Stroked!"));

            Keyboard.AddShortcut(new Shortcut(KeyCode.ExtraButton2), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.Home);
                Keyboard.SendKeyUp(KeyCode.Control);
            });
            Keyboard.AddShortcut(new Shortcut(KeyCode.ExtraButton1), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.End);
                Keyboard.SendKeyUp(KeyCode.Control);
            });

            Keyboard.AddShortcut(new Shortcut(KeyCode.OEM3, KeyCode.LeftMenu), hotKey =>
            {
                var activeWindow = WinApi.GetForegroundWindow();
                var windows = Windows.GetVisibleWindowsWithSameProcess(activeWindow);
                Windows.ActivateNextWindow(windows, activeWindow);
            });

            Keyboard.AddHotKey(KeyCode.W, ModifierKeys.Ctrl | ModifierKeys.Shift, () =>
            {
                var window = WinApi.GetForegroundWindow();
                var processName = Windows.GetWindowProcessImageName(window);
                var windowText = Windows.GetWindowText(window);
                Messages.Show(
                    $"Process Name:\r\n{processName}\r\n\r\nWindow Text:\r\n{windowText}",
                    "Window Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.OK,
                    MessageBoxOptions.ServiceNotification);
            });

            Keyboard.AddShortcut(new Shortcut(KeyCode.Left, KeyCode.F24), new KeyStroke(KeyCode.Home));
            Keyboard.AddShortcut(new Shortcut(KeyCode.Right, KeyCode.F24), new KeyStroke(KeyCode.End));
            Keyboard.AddShortcut(new Shortcut(KeyCode.Up, KeyCode.F24), new KeyStroke(KeyCode.Prior));
            Keyboard.AddShortcut(new Shortcut(KeyCode.Down, KeyCode.F24), new KeyStroke(KeyCode.Next));
            Keyboard.AddShortcut(new Shortcut(KeyCode.BrowserHome), new KeyStroke(KeyCode.Application));

//            Keyboard.ReplaceKey(KeyCode.N1, KeyCode.N2);
//            Keyboard.ReplaceKey(KeyCode.N2, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.LeftButton, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.RightButton, KeyCode.G);

//            var layout = WinApi.GetKeyboardLayout(0);
//            var result = WinApi.VkKeyScanEx('D', layout);
//            var lowOrderByte = BitUtils.GetLowOrderByte(result);
//            var highOrderByte = BitUtils.GetHighOrderByte(result);

            // Emoji / textmoji replacements
            KeySequence.FromString("$merge").Substitute("⛙");
            KeySequence.FromString("$shrug").Substitute("¯\\_(ツ)_/¯");
            KeySequence.FromString("$check").Substitute("✅");
            KeySequence.FromString("$drop").Substitute("💧");
            KeySequence.FromString("$pill").Substitute("💊");
            KeySequence.FromString("$code").Substitute("🏗️");
            KeySequence.FromString("$syringe").Substitute("💉");
            KeySequence.FromString("$cloud").Substitute("☁️");
            KeySequence.FromString("$mute").Substitute("🔇");
            KeySequence.FromString("$ambulance").Substitute("🚑");
            KeySequence.FromString("$sheet").Substitute("🗞");
            KeySequence.FromString("$pointright").Substitute("👉");
            KeySequence.FromString("$arrowright").Substitute("➜");
            KeySequence.FromString("$boom").Substitute("💥");
            KeySequence.FromString("$bump").Substitute("⏫");
            KeySequence.FromString("$db").Substitute("🏦");





//            KeySequence.FromString("tEst").Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("hello");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Register(keys => Debug.WriteLine("Success"));
        }
    }
}