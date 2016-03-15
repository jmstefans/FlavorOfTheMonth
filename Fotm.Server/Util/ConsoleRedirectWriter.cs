using System;
using System.IO;

namespace Fotm.Server.Util
{
    public class ConsoleRedirectWriter : RedirectWriter
    {
        readonly TextWriter _consoleTextWriter; // keeps Visual Studio console in scope

        public ConsoleRedirectWriter()
        {
            _consoleTextWriter = Console.Out;
            OnWrite += delegate (string text) { _consoleTextWriter.Write(text); };
            Console.SetOut(this);
        }

        public void Release()
        {
            Console.SetOut(_consoleTextWriter);
        }
    }
}
