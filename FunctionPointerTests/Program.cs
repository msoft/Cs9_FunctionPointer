using BenchmarkDotNet.Running;
using System;

namespace FunctionPointerTests
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var benchmark = new Benchmark();
			int result = benchmark.PInvoke();
			Console.WriteLine(result);

			//var summary = BenchmarkRunner.Run<Benchmark>();
		}
	}
}
