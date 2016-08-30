using System;
using Nancy.Hosting.Self;

namespace ModbusHandler
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			StartNancy();
			Console.ReadLine();
		}

		static void StartNancy()
		{
			using (var host = new NancyHost(new Uri("http://localhost:1234")))
			{
				host.Start();
				Console.WriteLine("Running on http://localhost:1234");
				Console.ReadLine();
			}
		}
	}
}
