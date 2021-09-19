using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace FunctionPointerTests
{
	public unsafe class MultiplicationBenchmark
	{
		public delegate int managedDelegate(int arg1, int arg2);

		public static int Multiply(int arg1, int arg2)
		{
			return arg1 * arg2;
		}

		public void DelegateArray()
		{
			var intList = new int[6];
			//var delegateList = new List<delegate* <int, int, int>>();
		}


		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int MultiplyDelegate(int arg1, int arg2);

		public static MultiplyDelegate MultiplyAction = Multiply;

		public void ExecuteByPassingFunctionPointer(int arg1, int arg2)
		{
			delegate* unmanaged[Stdcall]<int, int, int> multiplyPointer = (delegate* unmanaged[Stdcall]<int, int, int>)Marshal.GetFunctionPointerForDelegate(MultiplyAction);
			void* voidPointer = multiplyPointer;
			delegate* unmanaged[Stdcall]<int, int, int> otherPointer = (delegate* unmanaged[Stdcall]<int, int, int>)voidPointer;
			var ptrAddressInDec = new IntPtr(multiplyPointer);
			string ptrAddressInHex = ptrAddressInDec.ToString("X");

			Console.WriteLine("Function pointer address (managed side): " + ptrAddressInHex);

			int result = DllImportExample.MultiplyWithFunctionPointer(arg1, arg2, multiplyPointer);
		}

		public void ExecuteWithNativeFunctionPointer(int arg1, int arg2)
		{
			delegate* unmanaged<int, int, int> multiplyPointer = DllImportExample.GetMultiplyFunctionPointer();
			var value = multiplyPointer(arg1, arg2);
			Console.WriteLine("ExecuteWithNativeFunctionPointer: " + value);
		}

		private static int MultiplyInstanceFunction(MultiplicationBenchmark instance, int arg1, int arg2)
		{
			return arg1 * arg2;
		}

		public void TestWithInstanceFunction(int a, int b)
		{
			delegate* managed<MultiplicationBenchmark, int, int, int> instanceDelegate = &MultiplyInstanceFunction;

		}


	}

	public unsafe class DllImportExample
	{
		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static delegate* unmanaged<int, int, int> GetMultiplyFunctionPointer();

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int Multiply(int arg1, int arg2);

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int MultiplyWithFunctionPointer(int arg1, int arg2, delegate* unmanaged[Stdcall]<int, int, int> multiplFcn);

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int PerformBenchmarkWithFunctionPointer(int loopCount, delegate* <int, int, int> multiplFcn);
	}
}
