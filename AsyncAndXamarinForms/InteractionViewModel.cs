using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading;
using System.Windows.Input;
using System.Threading.Tasks;

namespace AsyncAndXamarinForms
{
	public class InteractionViewModel : INotifyPropertyChanged
	{
		CancellationTokenSource cancelTokenSrc;

		public InteractionViewModel ()
		{
			ProgressText = "Ready...";

			StartCommand = new Command (StartAsync);
			StopCommand = new Command (StopAsync);
		}

		public ICommand StartCommand { protected set; get; }
		public async void StartAsync ()
		{
			cancelTokenSrc = new CancellationTokenSource ();
			var progress = 
				new Progress<ProgressReport> (report => 
					{
						ProgressPercent = report.Percent/100d;
						ProgressText = report.Description;
					});
			var client = new RestApiClient ();

			try 
			{
				ProgressText = await client.CallApiAsync (progress, cancelTokenSrc.Token);
			} 
			catch (OperationCanceledException ex) 
			{
				IsCancelling = false;
				ProgressText = ex.Message;
			}
		}

		public ICommand StopCommand { protected set; get; }
		public async void StopAsync ()
		{
			try
			{
				IsCancelling = true;
				await Task.Delay (2000);
				cancelTokenSrc.Cancel ();
			}
			finally 
			{
				IsCancelling = false;
			}
		}

		double progressPercent;
		public double ProgressPercent 
		{
			get { return progressPercent; }
			set 
			{
				if (progressPercent == value)
					return;

				progressPercent = value;
				NotifyPropertyChanged ();
			}
		}

		string progressText;
		public string ProgressText 
		{
			get { return progressText; }
			set 
			{
				if (progressText == value)
					return;

				progressText = value;
				NotifyPropertyChanged ();
			}
		}

		bool isCancelling;
		public bool IsCancelling 
		{
			get { return isCancelling; }
			set 
			{
				if (isCancelling == value)
					return;

				isCancelling = value;
				NotifyPropertyChanged ();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

