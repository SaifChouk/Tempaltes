using BenchmarkDotNet.Running;
using System;
using System.IO;

namespace StreamPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PlaceholderClient>();

        }
    }
}
