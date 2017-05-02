/*
    Campus Harburg
    Copyright © 2016-2017 Frank Zimdars
    
    This file is part of "Campus Harburg".

    "Campus Harburg" is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    "Campus Harburg" is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see<http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von "Campus Harburg".

    "Campus Harburg" ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    "Campus Harburg" wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben.Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Campus_Harburg_Core;


namespace Campus_Harburg_Android
{
    [Activity(Label = "Einstellungen")]
    public class Activity_Settings : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        List<Struct_Mensa_Display> tableItems = new List<Struct_Mensa_Display>();


        protected override void OnResume()
        {
            base.OnResume();
            NavigationView navigationView;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(Resource.Id.nav_config);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(Resource.Style.MyTheme);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            
            SetContentView(Resource.Layout.Form_Settings);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();


            
            var adapter = ArrayAdapter.CreateFromResource(this,
                    Resource.Array.list_landingPage, Android.Resource.Layout.SimpleSpinnerItem);
           
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinner = (Spinner)FindViewById(Resource.Id.spinner1);
            spinner.Adapter = adapter;

        }


        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {

                case (Resource.Id.nav_start):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Start));
                        StartActivity(activity2);
                    }
                    break;

                case (Resource.Id.nav_about):
                    {
                        var activity2 = new Intent(this, typeof(Activity_About));
                        StartActivity(activity2);
                    }
                    break;
                case (Resource.Id.nav_exams):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Exams));
                        StartActivity(activity2);
                    }
                    break;
                case (Resource.Id.nav_times):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Times));
                        StartActivity(activity2);
                    }
                    break;
                case (Resource.Id.nav_mensa):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Mensa));
                        StartActivity(activity2);
                    }
                    break;
                case (Resource.Id.nav_links):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Links));
                        StartActivity(activity2);
                    }
                    break;
                case (Resource.Id.nav_news):
                    {
                        var activity2 = new Intent(this, typeof(Activity_News));
                        StartActivity(activity2);
                    }
                    break;
            }

            
            drawerLayout.CloseDrawers();
        }


    }
}