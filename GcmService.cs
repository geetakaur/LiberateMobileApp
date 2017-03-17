
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
	[Service]
	public class GcmService :  IntentService
	{
		static PowerManager.WakeLock sWakeLock;
		static object LOCK = new object();

	public	static void RunIntentInService(Context context, Intent intent)
		{
			lock (LOCK)
			{
				if (sWakeLock == null)
				{
					// This is called from BroadcastReceiver, there is no init.
					var pm = PowerManager.FromContext(context);
					sWakeLock = pm.NewWakeLock(
						WakeLockFlags.Partial, "My WakeLock Tag");
				}
			}

			sWakeLock.Acquire();
			intent.SetClass(context, typeof(MyIntentService));
			context.StartService(intent);
		}

		protected override void OnHandleIntent(Intent intent)
		{
			try
			{
				Context context = this.ApplicationContext;
				string action = intent.Action;

				if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
				{
					HandleRegistration(context, intent);
				}
				else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
				{
					HandleMessage(context, intent);
				}
			}
			finally
			{
				lock (LOCK)
				{
					//Sanity check for null as this is a public method
					if (sWakeLock != null)
						sWakeLock.Release();
				}
			}
		}

		private void HandleMessage(Context context,Intent intent)
		{
			string message = intent.GetStringExtra("message");
			// do something with the score
			Toast.MakeText (context, "Received message: "+ message, ToastLength.Long).Show ();
		}
		private void HandleRegistration(Context context,Intent intent)
		{
			string registrationId = intent.GetStringExtra("registration_id");
			Toast.MakeText (context, "Received REgistration Id: "+ registrationId, ToastLength.Long).Show ();
			// do something with the score
		}
	}
}

