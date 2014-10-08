using System;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncAndXamarinForms
{
	public class RestApiClient
	{
		public async Task<string> CallApiAsync(
			IProgress<ProgressReport> progress, 
			CancellationToken cancelToken)
		{
			for (int i = 0; i < 100; i++) 
			{
				await DoComplexAlgorithmAsync ();

				cancelToken.ThrowIfCancellationRequested ();

				progress.Report (
					new ProgressReport 
					{
						Percent = i + 1,
						Description = string.Format(
							"Only {0} steps to go.", 100 - (i + 1))
					});
			}

			return "Operation Complete";
		}

		public async Task DoComplexAlgorithmAsync ()
		{
			await Task.Delay (100);
		}
	}
}