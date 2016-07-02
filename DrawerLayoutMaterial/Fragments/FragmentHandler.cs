using System;
using System.Collections.Generic;
using Android.App;

namespace DrawerLayoutMaterial
{
	/// <summary>
	/// Gestore Fragments
	/// </summary>
	public class FragmentHandler
	{
		private static FragmentHandler instance;

		/// <summary>
		/// Dizionario Fragments
		/// </summary>
		private static readonly Dictionary<int, Type> fragmentMap = new Dictionary<int, Type>
		{
			{ Resource.Id.nav_home, typeof(HomeFragment) },
			{ Resource.Id.nav_settings, typeof(SettingsFragment) }
		};

		private FragmentHandler()
		{
		}

		private Fragment NewFragment(int resourceId)
		{
			Type t;
			bool result = fragmentMap.TryGetValue(resourceId, out t);
			if (!result)
			{
				return null;
			}
			return (Fragment)Activator.CreateInstance(t);
		}

		/// <summary>
		/// Carica un nuovo fragment nel container
		/// </summary>
		/// <param name="manager">Proprietà FragmentManager dell'Activity</param>
		/// <param name="resourceId">ID Risorsa</param>
		public void LoadFragment(FragmentManager manager, int resourceId)
		{
			Fragment toLoad = this.NewFragment(resourceId);
			if (toLoad != null)
			{
				var ft = manager.BeginTransaction();
				ft.AddToBackStack(null);
				ft.Add(Resource.Id.fragment_content, toLoad);
				ft.Commit();
			}
		}

		public static FragmentHandler Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new FragmentHandler();
				}
				return instance;
			}
		}

		public static Dictionary<int, Type> FragmentMap
		{
			get
			{
				return fragmentMap;
			}
		}

	}
}

