using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _4_TempApi
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHostedService<TemperatureHostedService>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Run(WriteTemperature);
		}

		private async Task WriteTemperature(HttpContext context)
		{
			await context.Response.WriteAsync(TemperatureHostedService.CurrentTemperature + "\n");
		}
	}
}
