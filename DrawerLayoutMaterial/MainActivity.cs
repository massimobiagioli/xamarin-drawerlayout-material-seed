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

			// Inizializza componenti
			this.InitLayout();
			this.InitToolbar();
			this.InitNavigationView();
			this.InitFragment();
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
#pragma warning disable CS0618 // Type or member is obsolete
			drawerLayout.SetDrawerListener(drawerToggle);
#pragma warning restore CS0618 // Type or member is obsolete
			drawerToggle.SyncState();
		}

		private void InitNavigationView()
		{
			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationItemSelected;
		}

		private void InitFragment()
		{
			FragmentHandler.Instance.LoadFragment(FragmentManager, Resource.Id.nav_home);
		}

		private void NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			FragmentHandler.Instance.LoadFragment(FragmentManager, e.MenuItem.ItemId);
			drawerLayout.CloseDrawers();
		}

		protected override void OnResume()
		{
			SupportActionBar.SetTitle(Resource.String.app_name);
			base.OnResume();
		}

		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_menu, menu);
			OptionsMenuHandler.Instance.Init(menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			return OptionsMenuHandler.Instance.Execute(item.ItemId, this);
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
