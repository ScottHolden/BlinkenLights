using System;
using System.Device.Gpio;
using System.Threading;

namespace _2_PushButton
{
	class Program
	{
		static void Main(string[] args)
		{
			const int buttonPin = 5;
			PinValue lastValue = PinValue.Low;
			
			// Set up our controller
			using (GpioController controller = new GpioController())
			{
				// Set our pin to export
				controller.OpenPin(buttonPin);

				// Set our pin to input
				controller.SetPinMode(buttonPin, PinMode.InputPullUp);

				while (true)
				{
					PinValue pinValue = controller.Read(buttonPin);

					if(pinValue != lastValue)
					{
						lastValue = pinValue;

						Console.WriteLine("Button pin went " + pinValue);
					}

					Thread.Sleep(100);
				}
			}
		}
	}
}
