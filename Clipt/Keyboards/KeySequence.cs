﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public class KeySequence : IEnumerable<KeyTrigger>
    {
        public int Count => keys.Count;
        public KeyTrigger this[int index] => keys[index];
        public KeySequenceSpan Start => new KeySequenceSpan(this, 0, Count);
        public void Register(KeySequenceHandler handler) => KeySequenceProcessor.Instance.RegisterSequence(this, handler);

        private readonly IReadOnlyList<KeyTrigger> keys;

        public KeySequence(params KeyTrigger[] keys) : this((IEnumerable<KeyTrigger>)keys)
        {
        }

        public KeySequence(IEnumerable<KeyTrigger> keys)
        {
            this.keys = keys.ToArray();
            if (this.keys.Count == 0)
                throw new ArgumentException(nameof(keys));
        }

        public static KeySequence FromString(string input)
        {
            return new KeySequence(input.Select(x => WinApi.VkKeyScan(x)).Select(x => new KeyTrigger(x.Key, x.Modifiers.HasFlag(VkKeyScanModifierKeys.Shift))));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyTrigger> GetEnumerator()
        {
            return keys.GetEnumerator();
        }

        public void Substitute(string replacement)
        {
            Register(keys =>
            {
                foreach (var _ in keys)
                {
                    KeySender.SendKeyPress(KeyCode.Back);
                }
                KeySender.SendString(replacement);
            });
        }
    }
}