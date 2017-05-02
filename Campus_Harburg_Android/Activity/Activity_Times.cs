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



using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System.Collections.Generic;
using Campus_Harburg_Core;

namespace Campus_Harburg_Android
{
    [Activity(Label = "Semesterzeiten", Icon = "@drawable/icon")]
    public class Activity_Times : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        Times ti = new Times();
        List<Times_Item> lst = null;

        Android.Support.V7.Widget.Toolbar toolbar;

        ListView listView;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnResume()
        {
            base.OnResume();
            NavigationView navigationView;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(Resource.Id.nav_times);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(Resource.Style.MyTheme);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
           
            SetContentView(Resource.Layout.Form_Times);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

           
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


           
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            navigationView.SetCheckedItem(Resource.Id.nav_times);

           
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            listView = FindViewById<ListView>(Resource.Id.List);

            lst = ti.getList();
           
            listView.Adapter = new ListAdapter_Times(this, lst);

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

                case (Resource.Id.nav_rooms):
                    {
                        var activity2 = new Intent(this, typeof(Activity_Room_Search));
                        StartActivity(activity2);
                    }
                    break;
            }

          
            drawerLayout.CloseDrawers();
        }
    }
}
