using System;

namespace Test.Dev.Components
{
    public class SimpleLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}