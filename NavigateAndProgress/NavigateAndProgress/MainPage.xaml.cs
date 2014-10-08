using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigateAndProgress
{
	public partial class MainPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        async void ButtonClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new ProgressPage());
        }
	}
}
