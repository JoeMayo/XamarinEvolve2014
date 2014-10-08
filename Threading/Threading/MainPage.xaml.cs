using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
	public partial class MainPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        async void OneThreadButtonClickedAsync(object sender, EventArgs e)
        {
            await OneThreadFirstLevelAsync();
        }

        async Task OneThreadFirstLevelAsync()
        {
            await OneThreadSecondLevelAsync();
        }

        async Task OneThreadSecondLevelAsync()
        {
            await OneThreadThirdLevelAsync();
        }

        async Task OneThreadThirdLevelAsync()
        {
            await Task.FromResult(7);
        }

        async void SeparateThreadButtonClickedAsync(object sender, EventArgs e)
        {
            await SeparateThreadFirstLevel();
        }

        async Task SeparateThreadFirstLevel()
        {
            await Task.Run(() => 
                Debug.Write("Thread ID in Task.RunAsync: " + Thread.CurrentThread.ManagedThreadId));
            await SeparateThreadSecondLevel();
        }

        async Task SeparateThreadSecondLevel()
        {
            await Task.Delay(1000);
            Debug.Write("Thread ID after Delay: " + Thread.CurrentThread.ManagedThreadId);
        }

        async void ConfigureAwaitButtonClickedAsync(object sender, EventArgs e)
        {
            await ConfigureAwaitFirstLevel();
            Debug.Write("Thread ID after ConfigureAwaitFirstLevel: " + Thread.CurrentThread.ManagedThreadId);
        }

        async Task ConfigureAwaitFirstLevel()
        {
            await ConfigureAwaitSecondLevel().ConfigureAwait(false);
            Debug.Write("Thread ID after 1st Delay: " + Thread.CurrentThread.ManagedThreadId);
        }

        async Task ConfigureAwaitSecondLevel()
        {
            await Task.Delay(1000);
            Debug.Write("Thread ID after 2nd Delay: " + Thread.CurrentThread.ManagedThreadId);
        }

        async void VoidButtonClickedAsync(object sender, EventArgs e)
        {
            try
            {
                VoidFirstLevel();
            }
            catch (InvalidOperationException ex)
            {
                Debug.Write("This will never be called: " + ex.ToString());
            }
        }

        async void VoidFirstLevel()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("Thrown from async void method");
        }

        async void DeadlockButtonClickedAsync(object sender, EventArgs e)
        {
            DeadlockFirstLevel().Wait();
        }

        async Task<string> DeadlockFirstLevel()
        {
            await Task.Delay(1000);
            return "Done";
        }

    }
}
