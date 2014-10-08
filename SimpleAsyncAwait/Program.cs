using System;
using System.Threading.Tasks;

namespace SimpleAsyncAwait
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			new MainClass ().WelcomeAsync ().Wait ();
			Console.ReadKey ();
		}

		public async Task WelcomeAsync() {
			await Task.Delay (1);
			Console.WriteLine ("Welcome to Xamarin Evolve!");
		}
	}
}
