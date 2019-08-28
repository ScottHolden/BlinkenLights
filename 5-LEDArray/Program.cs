using System;
using System.Device.Spi;
using System.Threading;
using Iot.Device.Max7219;

namespace _5_LEDArray
{
	class Program
	{
		static void Main()
		{
			SpiConnectionSettings connectionSettings = new SpiConnectionSettings(0, 0)
			{
				ClockFrequency = 10000000,
				Mode = SpiMode.Mode0
			};

			using (SpiDevice spi = SpiDevice.Create(connectionSettings))
			{
				using (Max7219 device = new Max7219(spi))
				{
					device.Init();

					// Double init
					device.Init();

					device[0] = 0b01010101;
					device[1] = 0b10101010;
					device[2] = 0b01010101;
					device[3] = 0b10111010;
					device[4] = 0b01011101;
					device[5] = 0b10111010;
					device[6] = 0b01011101;
					device[7] = 0b10111010;

					device.Flush();

					Console.WriteLine("Ready for next");
					Console.ReadLine();











					// Rotate the image

					device.Rotation = RotationType.Left;
					device.Flush();
					Thread.Sleep(1000);

					device.Rotation = RotationType.Half;
					device.Flush();
					Thread.Sleep(1000);

					device.Rotation = RotationType.Right;
					device.Flush();
					Thread.Sleep(1000);

					device.Rotation = RotationType.None;
					device.Flush();

					Console.WriteLine("Ready for next");
					Console.ReadLine();





					// Show some text

					device.Init();

					device.Rotation = RotationType.None;

					MatrixGraphics graphics = new MatrixGraphics(device, Fonts.Default);
					
					graphics.ShowMessage("Hello Alt.Net!", alwaysScroll: true);

					Console.WriteLine("Done!");
					Console.ReadLine();




					device.ClearAll();
				}
			}
			
		}
	}
}
