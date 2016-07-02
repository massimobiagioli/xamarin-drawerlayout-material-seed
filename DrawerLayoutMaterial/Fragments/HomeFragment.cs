using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace DrawerLayoutMaterial
{
	public class HomeFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.home_layout, container, false);

			var btnHandshake = (Button)view.FindViewById(Resource.Id.btnHandshake);
			btnHandshake.Click += (sender, e) =>
			{
				var txtHandshake = (TextView)view.FindViewById(Resource.Id.txtHandshake);
				txtHandshake.Text = this.Handshake();
			};

			return view;
		}

		/// <summary>
		/// Call Web Service
		/// </summary>
		public string Handshake()
		{
			var client = new RestClient("http://designer-maxbiag.rhcloud.com");
			var request = new RestRequest("/api/handshake", Method.GET);
			IRestResponse response = client.Execute(request);
			return response.Content;
		}

	}
}

