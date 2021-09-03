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


		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int MultiplyDelegate(int arg1, int arg2);

		public static MultiplyDelegate MultiplyAction = Multiply;

		public void ExecuteByPassingFunctionPointer(int arg1, int arg2)
		{
			delegate* unmanaged[Stdcall]<int, int, int> multiplyPointer = (delegate* unmanaged[Stdcall]<int, int, int>)Marshal.GetFunctionPointerForDelegate(MultiplyAction);

			var ptrAddressInDec = new IntPtr(multiplyPointer);
			string ptrAddressInHex = ptrAddressInDec.ToString("X");

			Console.WriteLine("Function pointer address (managed side): " + ptrAddressInHex);

			int result = DllImportExample.MultiplyWithFunctionPopinter(arg1, arg2, multiplyPointer);
		}

		public void ExecuteWithNativeFunctionPointer(int arg1, int arg2)
		{
			delegate* unmanaged<int, int, int> multiplyPointer = DllImportExample.GetStaticFunctionPointer();
			var value = multiplyPointer(arg1, arg2);
			Console.WriteLine("ExecuteWithNativeFunctionPointer: " + value);
		}


	}

	public unsafe class DllImportExample
	{
		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static delegate* unmanaged<int, int, int> GetStaticFunctionPointer();

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int Multiply(int arg1, int arg2);

		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int MultiplyWithFunctionPopinter(int arg1, int arg2, delegate* unmanaged[Stdcall]<int, int, int> multiplFcn);
	}
}
