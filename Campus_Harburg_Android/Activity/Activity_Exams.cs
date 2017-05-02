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


using System;
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
    [Activity(Label = "Prüfungen", Icon = "@drawable/icon")]
    public class Activity_Exams : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        InternalFile fio = new InternalFile();
        Exams_P ep = new Exams_P();

        Android.Support.V7.Widget.Toolbar toolbar;

        List<Campus_Harburg_Core.Exams_Item> tableItems = new List<Campus_Harburg_Core.Exams_Item>();
        ListView listView;

        ProgressDialog dlg;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_exams, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnResume()
        {
            base.OnResume();
            NavigationView navigationView;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(Resource.Id.nav_exams);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetTheme(Resource.Style.MyTheme);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
          
            SetContentView(Resource.Layout.Form_Exams);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            
            SetSupportActionBar(toolbar);
            Android.Support.V7.App.ActionBar actionBar = this.SupportActionBar;
          
          
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            navigationView.SetCheckedItem(Resource.Id.nav_exams);

           
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            listView = FindViewById<ListView>(Resource.Id.List);

            Campus_Harburg_Core.Exams_Item xi = new Campus_Harburg_Core.Exams_Item();
            xi.Name = "Laden...";
            xi.Note = "Daten werden verarbeitet";
            xi.Time = "";

            Campus_Harburg_Core.Exams_P exa = new Campus_Harburg_Core.Exams_P();
  
            ep.init(fio);
            
            Exams_Collection exi = ep.getDisplayList();
            exi.list.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

            tableItems.Add(xi);

            listView.Adapter = new ListAdapter_Exams(this, exi.list);

           

            int dayDif = (DateTime.Now - exi.list_time_download).Days;

            if (dayDif >= 30 && exi.list.Count >= 2)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Info");
                alert.SetMessage(TextContainer.Exams_TooOld);


                alert.SetPositiveButton("Ja", (senderAlert, args) =>
                {
                    update();
                });
                alert.SetNegativeButton("Nein", (senderAlert, args) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }else if(exi.list.Count < 2)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Info");
                alert.SetMessage(TextContainer.Exams_Empty);

                alert.SetPositiveButton("Ja", (senderAlert, args) =>
                {
                    update();
                });
                alert.SetNegativeButton("Nein", (senderAlert, args) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();

            }
        }

        public async void update()
        {
            dlg = ProgressDialog.Show(this, "", "Aktualisierung wird ausgeführt...\nBitte warten", true);
            bool status = await ep.update();


            if (status == false)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Fehler");
                alert.SetMessage("Die Liste konnte leider nicht heruntergeladen werden.");

                alert.SetNegativeButton("OK", (senderAlert, args) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();
                dlg.Dismiss();

                return;

            }

            Exams_Collection exi = ep.getDisplayList();
            exi.list.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

            listView.Adapter = new ListAdapter_Exams(this, exi.list);
            listView.InvalidateViews();

            dlg.Dismiss();
        }

        public void search()
        {
            showInputDialog();

            Exams_Collection exi = ep.getDisplayList();

            listView.Adapter = new ListAdapter_Exams(this, exi.list);
            listView.InvalidateViews();
        }

        public override bool  OnOptionsItemSelected(IMenuItem item)
        {

            if (item.ItemId == Resource.Id.action_update)
            {

                update();

                return true;
            }
            if (item.ItemId == Resource.Id.action_search)
            {
                search();

                return true;
            }
            if (item.ItemId == Resource.Id.action_info)
            {
                
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Info Datensatz");
                alert.SetMessage("Titel: " + ep.getDisplayList().list_title + "\nHeruntergeladen: " + ep.getDisplayList().list_data + "\nEinträge: " + ep.getDisplayList().list.Count.ToString());

                alert.SetNegativeButton("OK", (senderAlert, args) =>
                {
                    
                });

                Dialog dialog = alert.Create();
                dialog.Show();

                return true;
            }




            return base.OnOptionsItemSelected(item);
        }

        protected void showInputDialog()
        {
           
           
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View promptView = layoutInflater.Inflate(Resource.Layout.Dialog_SearchExam, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(promptView);

            EditText editText = (EditText)promptView.FindViewById(Resource.Id.edittext);
            editText.RequestFocus();
           
            alertDialogBuilder.SetCancelable(true);

            alertDialogBuilder.SetPositiveButton("OK", (sender, args) =>
            {
                String t = editText.Text;

                Exams_Collection exi = ep.search(editText.Text);

                listView.Adapter = new ListAdapter_Exams(this, exi.list);
                listView.InvalidateViews();
            });
            alertDialogBuilder.SetNegativeButton("Abbrechen", (sender, args) =>
            {
                
            });

           
            Android.Support.V7.App.AlertDialog alert = alertDialogBuilder.Create();
            alert.Show();
	}


        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.List)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle("Aktion");
                var menuItems = Resources.GetStringArray(Resource.Array.context_exams);
                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            var menuItems = Resources.GetStringArray(Resource.Array.context_exams);
            var menuItemName = menuItems[menuItemIndex];
           

            Toast.MakeText(this, string.Format("Selected {0} for item {1}", menuItemName, menuItemIndex), ToastLength.Short).Show();
            return true;
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
