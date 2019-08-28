using System.Threading.Tasks;

namespace _3_Reaction
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const int buttonPin = 5;
			const int blueLedPin = 22;
			const int greenLedPin = 20;

			using(ReactionGame game = new ReactionGame(buttonPin, blueLedPin, greenLedPin))
			{
				await game.Play();
			}
		}
	}
}
