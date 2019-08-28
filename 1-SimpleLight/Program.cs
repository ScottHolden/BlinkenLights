using System.Device.Gpio;
using System.Threading;

namespace _1_SimpleLight
{
	class Program
	{
		static void Main()
		{
			const int ledPin = 20;

			// Set up our controller
			using (GpioController controller = new GpioController())
			{
				// Set our pin to export
				controller.OpenPin(ledPin);

				// Set our pin to output
				controller.SetPinMode(ledPin, PinMode.Output);

				for(int i=0; i<3; i++)
				{
					// Turn our pin on
					controller.Write(ledPin, PinValue.High);

					Thread.Sleep(1000);

					// Turn our pin off
					controller.Write(ledPin, PinValue.Low);

					Thread.Sleep(1000);
				}

				// Close our pin
				controller.ClosePin(ledPin);
			}
		}
	}
}
