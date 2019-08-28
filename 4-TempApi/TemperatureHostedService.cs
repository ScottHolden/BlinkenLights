using System;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.DHTxx;
using Microsoft.Extensions.Hosting;

namespace _4_TempApi
{
	public class TemperatureHostedService: IHostedService, IDisposable
	{
		public static string CurrentTemperature { get; private set; } = "Not read yet.";
		private readonly Dht11 _sensor;
		private const int SensorPin = 13;
		private Timer _timer;

		public TemperatureHostedService()
		{
			_sensor = new Dht11(SensorPin);
		}

		private void ReadTemperature(object _)
		{
			double temp = _sensor.Temperature.Celsius;
			double humid = _sensor.Humidity;

			if (_sensor.IsLastReadSuccessful &&
				!double.IsNaN(temp) &&
				!double.IsNaN(humid))
			{
				CurrentTemperature = $"It is currently {temp} degrees at {humid}% humidity.";
			}
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(ReadTemperature, null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_sensor.Dispose();
		}
	}
}
