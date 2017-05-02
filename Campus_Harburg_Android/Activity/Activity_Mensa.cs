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
	[Activity(Label = "Mensa")]
	public class Activity_Mensa : AppCompatActivity
    {
        //Android GUI
		DrawerLayout drawerLayout;
        Android.Support.V7.Widget.Toolbar toolbar;
        ListView listView;
        NavigationView navigationView;

        //Mensa Schnittstelle
        Mensa _mensa = new Mensa();
        // Liste für die Mensa Gerichte
		List<Struct_Mensa_Display> tableItems = new List<Struct_Mensa_Display>();
        

         //Gültigkeit dewr Version prüfen
        // DateTime today = DateTime.Now;

        

        public override bool OnCreateOptionsMenu(IMenu menu)
		{
			
			MenuInflater.Inflate(Resource.Menu.top_mensa, menu);
			 return base.OnCreateOptionsMenu(menu);
		}

        protected override void OnResume()
        {
            base.OnResume();
            NavigationView navigationView;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(Resource.Id.nav_mensa);
        }

        //Wird beim Erzeugen der Activity aufgerufen
        protected override async  void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

		   SetTheme(Resource.Style.MyTheme);
		   Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			
			SetContentView(Resource.Layout.Form_Mensa);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			
			toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			
		
			navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
			navigationView.SetCheckedItem(Resource.Id.nav_mensa);

			
			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
			drawerLayout.SetDrawerListener(drawerToggle);
			drawerToggle.SyncState();

			InternalFile fi = new InternalFile();
			

			this.Title = "Bitte warten...";

            listView = FindViewById<ListView> (Resource.Id.List);

            await _mensa.init(fi);

			tableItems = _mensa.getCurrentList();

            //Tabelle an listView Adapter zur Anzeige übergeben
			listView.Adapter = new HomeScreenAdapter(this, tableItems);
			

            TouchListener tl = new TouchListener();
            tl.TA = TouchActionTest;

            listView.SetOnTouchListener(tl);

			this.Title = _mensa.getHeaderText(false);

		    bool uok = await _mensa.update();

            //Neue Liste dem Adapter zur Anzeige übergeben
			listView.Adapter = new HomeScreenAdapter(this, _mensa.getCurrentList());
            //Aktualisierung der Darstellung erzzwingen
			listView.InvalidateViews();

           //Aktualisieren der Daten fehlgeschlagen
			if(uok == false)
			{
				Android.Widget.Toast.MakeText(this, "Automatische Aktualisierung fehlgeschlagen! Bestehende Daten werden verwendet.", Android.Widget.ToastLength.Short).Show();
			}
			
			this.Title = _mensa.getHeaderText(false);
			
		}

        public void onRestoreInstanceState(Bundle savedInstanceState)
        {
           
           base.OnRestoreInstanceState(savedInstanceState);

           
            navigationView.SetCheckedItem(Resource.Id.nav_mensa);
        }

        void TouchActionTest(int x)
        {
            if (x==1)
            {
                loadNextDay();
            }else
            {
                loadPrevDay();
            }

        }

        void loadNextDay()
        {
            if (!_mensa.nextDay())
            {
                Android.Widget.Toast.MakeText(this, "Weitere Daten stehen nicht zur Verfügung!", Android.Widget.ToastLength.Short).Show();
            }

            listView.Adapter = new HomeScreenAdapter(this, _mensa.getCurrentList());
            listView.InvalidateViews();
            this.Title = _mensa.getHeaderText(true);

        }

        void loadPrevDay()
        {
            if (!_mensa.prevDay())
            {
                Android.Widget.Toast.MakeText(this, "Weitere Daten stehen nicht zur Verfügung!", Android.Widget.ToastLength.Short).Show();
            }

            listView.Adapter = new HomeScreenAdapter(this, _mensa.getCurrentList());
            listView.InvalidateViews();
            this.Title = _mensa.getHeaderText(true);

        }

        //Wird aufgerufen wenn ein Element im Optionsmenü ausgewählt wird
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
		   //Button 'Aktualisieren' wurde gedrückt
			if (item.ItemId == Resource.Id.action_update)
			{
				updSync();
				return true;
			}

            //Button 'Info' wurde gedrückt
			if (item.ItemId == Resource.Id.action_info)
			{
                showOpeningTimes();
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

        //Zeigt die Öffnungszeiten der Mensa per Toast Nachricht an
        private void showOpeningTimes()
        {
            
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Info");
            alert.SetMessage("Mensa Harburg\nDenickestraße 22\n21073 Hamburg\nMontag bis Freitag: 11:00 - 15:00 Uhr\nEssensausgabe bis 30 Minuten vor Schließung");

            alert.SetNegativeButton("OK", (senderAlert, args) =>
            {

            });

            Dialog dialog = alert.Create();
            dialog.Show();


        }

        private async void updSync()
		{
            //Wartedialog erzeugen
            var dlg = ProgressDialog.Show(this, "", "Daten werden heruntergeladen...\nBitte warten...", true);
            //Mensadaten laden
            bool uok = await _mensa.update();
            //Heruntergeladene Liste in Listview laden
			listView.Adapter = new HomeScreenAdapter(this, _mensa.getCurrentList());
            //Aktualsieren der Listview erzwingen
			listView.InvalidateViews();

			if (uok == true)
			{
				Android.Widget.Toast.MakeText(this, "Daten erfolgreich aktualisiert", Android.Widget.ToastLength.Short).Show();
			}
			else
			{
				Android.Widget.Toast.MakeText(this, "Aktualisierung fehlgeschlagen. Bestehende Daten werden verwendet.", Android.Widget.ToastLength.Short).Show();

			}
			this.Title = _mensa.getHeaderText(false);
            dlg.Dismiss();
		}

        //Navigationsleiste
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



    class TouchListener :  Java.Lang.Object , View.IOnTouchListener
    {
        private float _viewX;

        public delegate void action(int x);

        public action TA;
        

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _viewX = e.RawX;
                    break;
                case MotionEventActions.Up:
                    {
                        v.Layout(0, v.Top, v.Width, v.Bottom);


                        var n = e.RawX;

                        if (Math.Abs(_viewX - n) > 250)
                        {
                            if (_viewX < n)
                            {
                                TA(0);
                            }
                            else
                            {
                                TA(1);
                            }

                        }
                    }
                    break;
                case MotionEventActions.Move:
                    {

                        if (Math.Abs(_viewX - e.RawX) > 100)
                        {

                            int offset = 100;
                            if(_viewX > e.RawX)
                            {
                                offset *= -1;
                            }

                            var left = (int)(e.RawX - _viewX) - offset ;
                            var right = (int)(left + v.Width);

                            v.Layout(left, v.Top, right, v.Bottom);
                         
                        }else
                        {
                            v.Layout(0, v.Top, v.Width, v.Bottom);

                        }



                    }
                    break;
            }
            return false;
        }
    }

}
 