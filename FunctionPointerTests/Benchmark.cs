using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FunctionPointerTests
{
	public unsafe class Benchmark
	{
		private readonly MultiplyClass multiplyClass;

		private delegate int multiplyDelegate(int arg1, int arg2); // Définition du delegate
		private readonly multiplyDelegate multiplyManagedDelegate;

		private readonly delegate*<int, int, int> multiplyManagedPointer;
		private readonly delegate* unmanaged<int, int, int> multiplyUnmanagedPointer;

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static delegate* unmanaged<int, int, int> GetMultiplyFunctionPointer();

		private const int loopCount = 800000;

		public Benchmark()
		{
			this.multiplyClass = new MultiplyClass();
			this.multiplyManagedDelegate = this.multiplyClass.Multiply;
			this.multiplyManagedPointer = &Multiply;
			this.multiplyUnmanagedPointer = GetMultiplyFunctionPointer();
		}

		[Benchmark]
		public void InstanceFunctionCall()
		{
			var firstArray = new int[] { 23, 87, 51, 98, 29, 75, 93, 48, 24, 83, 47, 38, 62, 22, 97, 15, 52, 41, 74, 13 };
			var secondArray = firstArray.Reverse().ToArray();

			int arrayLength = firstArray.Length;
			int value = 0;
			int offset = 0;
			bool add = true;
			for (int i = 0; i < loopCount; i++)
			{
				for (int j = 0; j < arrayLength; j++)
				{
					int index = (offset + j) % arrayLength;
					int multiplicationResult = this.multiplyClass.Multiply(firstArray[index], secondArray[index]);
					if (add)
						value += multiplicationResult;
					else
						value -= multiplicationResult;

					add = !add;
				}

				offset++;
			}

		}

		[Benchmark]
		public void ManagedDelegateCall()
		{
			var firstArray = new int[] { 23, 87, 51, 98, 29, 75, 93, 48, 24, 83, 47, 38, 62, 22, 97, 15, 52, 41, 74, 13 };
			var secondArray = firstArray.Reverse().ToArray();

			int arrayLength = firstArray.Length;
			int value = 0;
			int offset = 0;
			bool add = true;
			for (int i = 0; i < loopCount; i++)
			{
				for (int j = 0; j < arrayLength; j++)
				{
					int index = (offset + j) % arrayLength;
					int multiplicationResult = this.multiplyManagedDelegate(firstArray[index], secondArray[index]);
					if (add)
						value += multiplicationResult;
					else
						value -= multiplicationResult;

					add = !add;
				}

				offset++;
			}

		}

		[Benchmark]
		public void ManagedFunctionPointerCall()
		{
			var firstArray = new int[] { 23, 87, 51, 98, 29, 75, 93, 48, 24, 83, 47, 38, 62, 22, 97, 15, 52, 41, 74, 13 };
			var secondArray = firstArray.Reverse().ToArray();

			int arrayLength = firstArray.Length;
			int value = 0;
			int offset = 0;
			bool add = true;
			for (int i = 0; i < loopCount; i++)
			{
				for (int j = 0; j < arrayLength; j++)
				{
					int index = (offset + j) % arrayLength;
					int multiplicationResult = this.multiplyManagedPointer(firstArray[index], secondArray[index]);
					if (add)
						value += multiplicationResult;
					else
						value -= multiplicationResult;

					add = !add;
				}

				offset++;
			}

		}

		[Benchmark]
		public void UnmanagedFunctionPointerCall()
		{
			var firstArray = new int[] { 23, 87, 51, 98, 29, 75, 93, 48, 24, 83, 47, 38, 62, 22, 97, 15, 52, 41, 74, 13 };
			var secondArray = firstArray.Reverse().ToArray();

			int arrayLength = firstArray.Length;
			int value = 0;
			int offset = 0;
			bool add = true;
			for (int i = 0; i < loopCount; i++)
			{
				for (int j = 0; j < arrayLength; j++)
				{
					int index = (offset + j) % arrayLength;
					int multiplicationResult = this.multiplyManagedPointer(firstArray[index], secondArray[index]);
					if (add)
						value += multiplicationResult;
					else
						value -= multiplicationResult;

					add = !add;
				}

				offset++;
			}

		}


		[Benchmark]
		public void ProvideFunctionPointerToNativeFunction()
		{
			DllImportExample.PerformBenchmarkWithFunctionPointer(loopCount, this.multiplyManagedPointer);
		}

		private static int Multiply(int arg1, int arg2)
		{
			return arg1 * arg2;
		}
	}

	public class MultiplyClass
	{
		public int Multiply(int arg1, int arg2)
		{
			return arg1 * arg2;
		}
	}
}
