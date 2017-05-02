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
    [Activity(Label = "Räume")]
    public class Activity_Room_Search : AppCompatActivity
    {

        DrawerLayout drawerLayout;
        TextView _dateDisplay;
        Button _dateSelectButton;
        ProgressDialog dlg;

        List<Struct_Mensa_Display> tableItems = new List<Struct_Mensa_Display>();
        ListView listView;

        DateTime selectedDay = DateTime.Now;


        protected override void OnResume()
        {
            base.OnResume();
            NavigationView navigationView;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(Resource.Id.nav_rooms);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetTheme(Resource.Style.MyTheme);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

         
            SetContentView(Resource.Layout.From_Room_Search);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            navigationView.SetCheckedItem(Resource.Id.nav_rooms);

          
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            Spinner spinner = (Spinner)FindViewById(Resource.Id.spinner1);
            Spinner spinner2 = (Spinner)FindViewById(Resource.Id.spinner2);
            Spinner spinner3 = (Spinner)FindViewById(Resource.Id.spinner3);
            Spinner spinner4 = (Spinner)FindViewById(Resource.Id.spinner4);
            Spinner spinner5 = (Spinner)FindViewById(Resource.Id.spinner5);

            
            var adapter = ArrayAdapter.CreateFromResource(this,
                    Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);
         
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);



            var adapter2 = ArrayAdapter.CreateFromResource(this,
                    Resource.Array.array2, Android.Resource.Layout.SimpleSpinnerItem);
            
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var adapter3 = ArrayAdapter.CreateFromResource(this,
                    Resource.Array.array3, Android.Resource.Layout.SimpleSpinnerItem);
            
            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            
            spinner.Adapter = adapter;

            
            spinner.Adapter =  adapter;
            spinner2.Adapter = adapter;
            spinner3.Adapter = adapter2;
            spinner4.Adapter = adapter3;
            spinner5.Adapter = adapter3;


            if(DateTime.Now.Hour >= 23)
            {
                spinner.SetSelection(22);

                spinner2.SetSelection(24);

            }
            else
            {
                spinner.SetSelection(DateTime.Now.Hour);

                spinner2.SetSelection(DateTime.Now.Hour+2);

            }

            spinner5.SetSelection(42);



            _dateDisplay = FindViewById<TextView>(Resource.Id.text_date);
             _dateSelectButton = FindViewById<Button>(Resource.Id.button_date);
             _dateSelectButton.Click += DateSelect_OnClick;
            _dateDisplay.Text = selectedDay.ToShortDateString();


            Button button1 = FindViewById<Button>(Resource.Id.button_ok);

            button1.Click += OnOKButton;

            // Create your application here
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _dateDisplay.Text = time.ToShortDateString();
                selectedDay = time;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }


        int getSpinnerTextInt(Spinner spinner)
        {
            String text = spinner.SelectedItem.ToString();
            return Convert.ToInt16(text);

        }

        int getSpinnerIndex(Spinner spinner)
        {
            return spinner.SelectedItemPosition;

        }

        void msgBox(String title, String text)
        {

            new Android.Support.V7.App.AlertDialog.Builder(this)

                .SetNegativeButton("OK", (sender, args) =>
                {
                    
                })
                .SetMessage(text)
                .SetTitle(title)
                .Show();

        }


        async void OnOKButton(object sender, EventArgs eventArgs)
        {
            

            Rooms_Finder rf = new Rooms_Finder();
            Struct_SearchConfig_Room cfg = new Struct_SearchConfig_Room();
            Boolean isError = false;

            Spinner t_start = (Spinner)FindViewById(Resource.Id.spinner1);
            Spinner t_end = (Spinner)FindViewById(Resource.Id.spinner2);
            Spinner t_dur = (Spinner)FindViewById(Resource.Id.spinner3);
            Spinner t_cmin = (Spinner)FindViewById(Resource.Id.spinner4);
            Spinner t_cmax = (Spinner)FindViewById(Resource.Id.spinner5);
            CheckBox t_pool = (CheckBox)FindViewById(Resource.Id.checkBox);




            cfg.cap_min = getSpinnerTextInt(t_cmin);



            cfg.cap_max = getSpinnerTextInt(t_cmax) ;

            cfg.duration = getSpinnerIndex(t_dur) + 1 ;
            cfg.date_day = selectedDay.Day;
            cfg.date_month = selectedDay.Month;
            cfg.date_year = selectedDay.Year;
            cfg.time_end = getSpinnerIndex(t_end);
            cfg.time_start = getSpinnerIndex(t_start) ;

            if (t_pool.Checked)
            {
                cfg.withPool = true;

            }
            else
            {
                cfg.withPool = false;
            }

            


            if(cfg.time_start == 24)
            {
                isError = true;
                msgBox("Ungültige Eingabe", "Die Startzeit muss vor 24 Uhr liegen.");
                
            }
            if(cfg.time_start > cfg.time_end)
            {
                isError = true;
                msgBox("Ungültige Eingabe", "Die Startzeit muss vor der Endzeit liegen.");

            }
            if (cfg.cap_min > cfg.cap_max)
            {
                isError = true;
                msgBox("Ungültige Eingabe", "Die minimale Anzahl an Plätzen darf nicht größer sein als das Maximum.");

            }
            if (cfg.duration > (cfg.time_end - cfg.time_start) )
            {
                isError = true;
                msgBox("Ungültige Eingabe", "Der gewünschte Zeitraum ist für die Dauer zu kurz.");

            }
            if(cfg.time_end == 0)
            {
                isError = true;
                msgBox("Ungültige Eingabe", "Die Endzeit darf nicht 0 Uhr sein.--> 24 Uhr");

            }



            if (!isError)
            {
                dlg = ProgressDialog.Show(this, "Bitte warten", "Daten werden abgefragt...", true);

                Room_Result res = await rf.getResult(cfg);

                List<Campus_Harburg_Core.Struct_Room> list = res.list;

                if (!res.ok)
                {
                    msgBox("Abfrage fehlgeschlagen", "Die Abfrage der Daten vom Server ist fehlgeschlagen. Möglicherweise ist kein Raum mit gewünschten Kriterien verfügbar oder die Internetverbindung wurde unterbrochen");

                }
                else
                {
                        var activity2 = new Intent(this, typeof(Activity_Room_List));
                        activity2.PutExtra("Data", rf.dataToString(list));
                        StartActivity(activity2);


                }

            }

            dlg.Dismiss();

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



    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
       
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,this,currently.Year,currently.Month-1, currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
           
            DateTime selectedDate = new DateTime(year, monthOfYear +1 , dayOfMonth);
            
            _dateSelectedHandler(selectedDate);
        }
    }
}