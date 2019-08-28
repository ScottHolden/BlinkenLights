using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace _4_TempApi
{
	class Program
	{
		static void Main(string[] args)
		{
			WebHost.CreateDefaultBuilder(args)
					.UseStartup<Startup>()
					.Build()
					.Run();
		}
	}
}
