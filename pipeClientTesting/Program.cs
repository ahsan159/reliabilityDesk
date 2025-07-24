using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;

namespace pipeClientTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program Started");
            NamedPipeClientStream pipe = new NamedPipeClientStream("clientPipe");
            pipe.Connect();
        }
    }
}
