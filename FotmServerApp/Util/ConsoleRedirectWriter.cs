using System;
using System.IO;

namespace Fotm.Server.Util
{
    public class ConsoleRedirectWriter : RedirectWriter
    {
        TextWriter consoleTextWriter; //keeps Visual Studio console in scope.

        public ConsoleRedirectWriter()
        {
            consoleTextWriter = Console.Out;
            this.OnWrite += delegate (string text) { consoleTextWriter.Write(text); };
            Console.SetOut(this);
        }

        public void Release()
        {
            Console.SetOut(consoleTextWriter);
        }
    }
}
