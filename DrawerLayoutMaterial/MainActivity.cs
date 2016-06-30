using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.App;

namespace DrawerLayoutMaterial
{
	[Activity(Label = "DrawerLayoutMaterial", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : AppCompatActivity
	{
		DrawerLayout drawerLayout;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Init Layout
			this.InitLayout();

			// Init toolbar
			this.InitToolbar();

			// Init NavigationView
			this.InitNavigationView();

			// Init Fragment
			var fragment = new HomeFragment();
			this.LoadFragment(fragment);
		}

		private void InitLayout()
		{
			SetContentView(Resource.Layout.main);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
		}

		private void InitToolbar()
		{
			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetTitle(Resource.String.app_name);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);

			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
			drawerLayout.SetDrawerListener(drawerToggle);
			drawerToggle.SyncState();
		}

		private void InitNavigationView()
		{
			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationItemSelected;
		}

		private void LoadFragment(Fragment toLoad)
		{
			var ft = FragmentManager.BeginTransaction();
			ft.AddToBackStack(null);
			ft.Add(Resource.Id.fragment_content, toLoad);
			ft.Commit();
		}

		protected override void OnResume()
		{
			SupportActionBar.SetTitle(Resource.String.app_name);
			base.OnResume();
		}

		private void NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			switch (e.MenuItem.ItemId)
			{
				case (Resource.Id.nav_home):
					Toast.MakeText(this, "Home selected!", ToastLength.Short).Show();
					break;
				case (Resource.Id.nav_settings):
					Toast.MakeText(this, "Settings selected!", ToastLength.Short).Show();
					break;
			}

			drawerLayout.CloseDrawers();
		}

		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_menu, menu);
			if (menu != null)
			{
				menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
			}
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.action_refresh:
					Toast.MakeText(this, "Refresh!", ToastLength.Short).Show();
					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}

		public override void OnBackPressed()
		{
			if (FragmentManager.BackStackEntryCount != 0)
			{
				FragmentManager.PopBackStack();
			}
			else 
			{
				base.OnBackPressed();
			}
		}

	}
}
