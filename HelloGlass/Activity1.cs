using Android.App;
using Android.Glass.App;
using Android.OS;
using Android.Views;
using LinqToTwitter;
using System;
using System.Linq;

namespace HelloGlass
{
    [Activity(Label = "HelloGlass", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Card card1 = new Card(this);

            var auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "abcdefg",
                    ConsumerSecret = "hijklmn",
                },
            };
            await auth.AuthorizeAsync();

            var ctx = new TwitterContext(auth);

            var tweet =
                await
                (from twt in ctx.Status
                 where twt.Type == StatusType.Show &&
                       twt.ID == 421551271137378304ul
                 select twt)
                .SingleOrDefaultAsync();

            card1.SetText(tweet.Text);
            card1.SetFootnote(
                "@" + tweet.User.ScreenNameResponse +
                " - " + tweet.CreatedAt.ToShortDateString());
            View card1View = card1.ToView();

            SetContentView(card1View);
        }
    }
}

