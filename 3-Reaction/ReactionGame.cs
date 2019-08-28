using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _3_Reaction
{
	class ReactionGame : IDisposable
	{
		private readonly Random _r = new Random();
		private readonly GpioController _controller;
		private readonly int _buttonPin;
		private readonly int _blueLedPin;
		private readonly int _greenLedPin;
		public ReactionGame(int buttonPin, int blueLedPin, int greenLedPin)
		{
			_buttonPin = buttonPin;
			_blueLedPin = blueLedPin;
			_greenLedPin = greenLedPin;

			_controller = new GpioController();

			// Set our pins to export
			_controller.OpenPin(_buttonPin);
			_controller.OpenPin(_blueLedPin);
			_controller.OpenPin(_greenLedPin);

			// Set our pins to input/output
			_controller.SetPinMode(_buttonPin, PinMode.InputPullUp);
			_controller.SetPinMode(_blueLedPin, PinMode.Output);
			_controller.SetPinMode(_greenLedPin, PinMode.Output);

			_controller.Write(_blueLedPin, PinValue.Low);
			_controller.Write(_greenLedPin, PinValue.Low);
		}

		public async Task Play()
		{
			Console.WriteLine("Get ready!");

			int holdOff = _r.Next(1, 3);

			for (int i = 0; i < holdOff; i++)
			{
				if((await PlayButton(true)) < TimeSpan.Zero)
				{
					Console.WriteLine("Too early!!");
					return;
				}
			}

			TimeSpan result = await PlayButton(false);

			if (result < TimeSpan.Zero)
				Console.WriteLine("Too early!!");
			else
				Console.WriteLine($"Reaction time of {result.TotalMilliseconds}ms");
		}

		private async Task<TimeSpan> PlayButton(bool fakeOut)
		{
			int waitMs = _r.Next(1500, 6000);
			WaitForEventResult pre = await _controller.WaitForEventAsync(_buttonPin, PinEventTypes.Falling, 
																			TimeSpan.FromMilliseconds(waitMs));

			if (!pre.TimedOut) return TimeSpan.FromSeconds(-1);

			if (fakeOut)
				return await ShowFakeout();
			else
				return await ShowReal();
		}

		private async Task<TimeSpan> ShowReal()
		{
			Stopwatch sw = Stopwatch.StartNew();

			await ShowRound(_greenLedPin, 30000);

			return sw.Elapsed;
		}

		private Task<TimeSpan> ShowFakeout() =>
			ShowRound(_blueLedPin, 3000);

		private async Task<TimeSpan> ShowRound(int pin, int msTimeout)
		{
			_controller.Write(pin, PinValue.High);

			WaitForEventResult fake = await _controller.WaitForEventAsync(_buttonPin, PinEventTypes.Falling,
																		TimeSpan.FromMilliseconds(msTimeout));

			_controller.Write(pin, PinValue.Low);

			return fake.TimedOut ? TimeSpan.Zero : TimeSpan.FromSeconds(-1);
		}

		public void Dispose()
		{
			_controller.Dispose();
		}
	}
}
