
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartLiberate
{
	[Activity (Label = "MainActivity")]			
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Button buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);

			buttonRegister.Click += delegate {
				registerGCM ();
			};
			SetContentView (buttonRegister);
			// Create your application here
		}
		private void registerGCM()
		{
			string senders = "866957673705";
			Context context = this.ApplicationContext;
			Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
			intent.SetPackage("com.google.android.gsf");
			intent.PutExtra("app", PendingIntent.GetBroadcast(context, 0, new Intent(), 0));
			intent.PutExtra("sender", senders);
			context.StartService(intent);
		}
	}
}

