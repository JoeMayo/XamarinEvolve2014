using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientAndWebApi
{
	public partial class MainPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        async void RestButtonClickedAsync (object sender, EventArgs e)
        {
            string valuesUrl = "http://192.168.0.10/DemoWebApi/api/Values";
            var client = new HttpClient();

            ResponseLabel.Text = await client.GetStringAsync (valuesUrl);
        }
    }
}
