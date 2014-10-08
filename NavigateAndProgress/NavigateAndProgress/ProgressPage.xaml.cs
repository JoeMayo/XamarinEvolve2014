using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NavigateAndProgress
{
	public partial class ProgressPage
	{
		public ProgressPage ()
		{
			InitializeComponent ();
		}

        async void ProgressButtonClicked (object sender, EventArgs e)
        {
            for (int i=1; i < 5; i++)
            {
                await AppProgress.ProgressTo (i * .25, 1000, Easing.Linear);
            }
        }

        async void MainPageButtonClicked (object sender, EventArgs e)
        {
            await Navigation.PopAsync ();
        }
    }
}
