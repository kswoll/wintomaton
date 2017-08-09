﻿using Clipt.Keyboards;
using NUnit.Framework;

namespace Wintomaton.Tests.Keyboards
{
    [TestFixture]
    public class KeyStrokeProcessorTests
    {
        [Test]
        public void OneKeyKeyStroke()
        {
            var processor = new KeyStrokeProcessor();
            var keyStroke = new KeyStroke(KeyCode.A);
            KeyStroke? stroke = null;
            processor.Register(keyStroke, x => stroke = x);
            processor.ProcessKey(KeyCode.A);

            Assert.IsNotNull(stroke);
        }

        [Test]
        public void TwoKeyKeyStroke()
        {
            var processor = new KeyStrokeProcessor();
            var keyStroke = new KeyStroke(KeyCode.A, KeyCode.Control);
            KeyStroke? stroke = null;
            processor.Register(keyStroke, x => stroke = x);
            processor.ProcessKey(KeyCode.Control);
            processor.ProcessKey(KeyCode.A);

            Assert.IsNotNull(stroke);
        }
    }
}