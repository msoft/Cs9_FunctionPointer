using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionPointerTests
{
	public unsafe class Benchmark
	{
		private MultiplicationBenchmark multiplicationBenchmark;
		private delegate*<int, int, int> multiplyPointer;
		private MultiplicationBenchmark.managedDelegate functionDelegate;
		private delegate* unmanaged<int, int, int> multiplyStaticFunctionPointer;

		private const int loopCount = 800000;

		public Benchmark()
		{
			this.multiplicationBenchmark = new MultiplicationBenchmark();
			this.multiplyPointer = &DllImportExample.Multiply;
			this.functionDelegate = MultiplicationBenchmark.Multiply;
			this.multiplyStaticFunctionPointer = DllImportExample.GetStaticFunctionPointer();
		}

		[Benchmark]
		public int PInvoke()
		{
			int value = 1;
			for (int i = 0; i < loopCount; i++)
			{
				value = DllImportExample.Multiply(value, 2);
				if (value > Int32.MaxValue || value < 0)
					value = 1;
			}

			return value;
		}

		[Benchmark]
		public void NormalCall()
		{
			int value = 1;
			for (int i = 0; i < loopCount; i++)
			{
				value = MultiplicationBenchmark.Multiply(value, 2);
				if (value > Int32.MaxValue || value < 0)
					value = 1;
			}
		}

		[Benchmark]
		public void FunctionPointer()
		{
			int value = 1;
			for (int i = 0; i < loopCount; i++)
			{
				value = this.multiplyPointer(value, 2);
				if (value > Int32.MaxValue || value < 0)
					value = 1;
			}
		}

		[Benchmark]
		public void ManagedDelegate()
		{
			int value = 1;
			for (int i = 0; i < loopCount; i++)
			{
				value = this.functionDelegate(value, 2);
				if (value > Int32.MaxValue || value < 0)
					value = 1;
			}
		}

		[Benchmark]
		public void DelegateToStaticFunctionPointer()
		{
			int value = 1;
			for (int i = 0; i < loopCount; i++)
			{
				value = this.multiplyStaticFunctionPointer(value, 2);
				if (value > Int32.MaxValue || value < 0)
					value = 1;
			}
		}
	}
}
