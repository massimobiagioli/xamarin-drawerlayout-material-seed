using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace DrawerLayoutMaterial
{
	/// <summary>
	/// Gestore opzioni di menu
	/// </summary>
	public class OptionsMenuHandler
	{
		private static OptionsMenuHandler instance;

		/// <summary>
		/// Dizionario delle funzioni associate ai pulsanti di menu
		/// </summary>
		private static readonly Dictionary<int, Func<Activity, bool>> optionsMenuMap = new Dictionary<int, Func<Activity, bool>>
		{
			{ 
				Resource.Id.action_refresh, (activity) => 
				{
					Toast.MakeText(activity, "Refresh!", ToastLength.Short).Show();
					return true;
				} 
			}
		};

		private OptionsMenuHandler()
		{
		}

		/// <summary>
		/// Inizializza pulsanti toolbar
		/// </summary>
		/// <param name="menu">Toolbar menu</param>
		public void Init(Android.Views.IMenu menu)
		{
			if (menu != null)
			{
				menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
			}
		}

		/// <summary>
		/// Esecuzione funzione associata alla voce di menu
		/// </summary>
		/// <param name="resourceId">ID pulsante toolbar</param>
		/// <param name="activity">Activity</param>
		public bool Execute(int resourceId, Activity activity)
		{
			Func<Activity, bool> fn;
			bool result = OptionsMenuMap.TryGetValue(resourceId, out fn);
			if (result)
			{
				return fn(activity);
			}
			else
			{
				return false;
			}
		}

		public static OptionsMenuHandler Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new OptionsMenuHandler();
				}
				return instance;
			}
		}

		public static Dictionary<int, Func<Activity, bool>> OptionsMenuMap
		{
			get
			{
				return optionsMenuMap;
			}
		}
	}
}

