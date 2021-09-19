using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;
using BenchmarkDotNet.Validators;
using System;
using System.Runtime.InteropServices;

namespace FunctionPointerTests
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");

			//var benchmark = new Benchmark();
			//benchmark.ProvideFunctionPointerToNativeFunction();
			////int result = benchmark.PInvoke();
			//int result = benchmark.SimpleInstanceCall();
			//Console.WriteLine(result);

			//result = benchmark.ProvideManagedFunctionPointer();
			//Console.WriteLine(result);

			//var dotnetCli32bit = NetCoreAppSettings
			//	.NetCoreApp50
			//	.WithCustomDotNetCliPath(@"C:\Program Files (x86)\dotnet\dotnet.exe", "32 bit cli");

			//var x86core50 = Job.ShortRun
			//	.WithPlatform(BenchmarkDotNet.Environments.Platform.X86)
			//	.WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp50
			//	.WithCustomDotNetCliPath(@"C:\Projects\performance\tools\dotnet\x86\dotnet.exe")))
			//	.WithId("x86 .NET Core 5.0"); // displayed in the results table

			//var x86core50 = Job.ShortRun
			//	.WithPlatform(BenchmarkDotNet.Environments.Platform.X86)
			//	.WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp50))
			//	.WithId("x86 .NET Core 5.0"); // displayed in the results table

			var x86core21 = Job.ShortRun
				.WithPlatform(BenchmarkDotNet.Environments.Platform.X86)
				.WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp50.WithCustomDotNetCliPath(@"C:\Program Files\dotnet\dotnet.exe")))
				.WithId("ARM64 .NET Core 5.0"); // displayed in the results table

			var config = DefaultConfig.Instance
				.AddJob(x86core21);

			//var summary = BenchmarkRunner.Run<Benchmark>(config);


			//var config = DefaultConfig.Instance
			//	.AddJob(x86core50);

			var summary = BenchmarkRunner.Run<Benchmark>(config);
		}


		[DllImport("CalledNativeDll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public extern static int Multiply(int arg1, int arg2);
	}
}
