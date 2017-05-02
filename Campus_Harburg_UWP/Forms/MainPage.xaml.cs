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
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Campus_Harburg_Core;
using Campus_Harburg_Windows;

namespace Campus_Harburg_UWP
{
    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            API_InternalFile_UWP file = new API_InternalFile_UWP();
            settings set = new settings();
            Settings_Config cfg = await set.readCollection(file);
            switch (cfg.start)
            {
                case 0:
                    {
                        this.MyFrame.Navigate(typeof(frm_start));
                    }
                    break;
                case 1:
                    {
                        
                    }
                    break;
                case 2:
                    {
                        this.MyFrame.Navigate(typeof(frm_mensa));
                    }
                    break;
                case 3:
                    {
                        this.MyFrame.Navigate(typeof(room_search));
                    }
                    break;

                default:
                    {
                        this.MyFrame.Navigate(typeof(frm_start));

                    }
                    break;
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            base.OnNavigatedTo(e);

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                HardwareButtons.BackPressed += MainPage_BackRequestedH;
            }
        }

        private void MainPage_BackRequestedH(object sender, BackPressedEventArgs a)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
                a.Handled = true;
            }
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Home.IsSelected)
            {
                label_head.Text = "Start";
                MyFrame.Navigate(typeof(frm_start));

            }
            else if (ls_pruef.IsSelected)
            {

                Object[] parameters = new Object[2];
                parameters[0] = "";

                MyFrame.Navigate(typeof(frm_exams),parameters);
                label_head.Text = "Prüfungen";

            }
            else if (ls_links.IsSelected)
            {

                MyFrame.Navigate(typeof(frm_links));
                label_head.Text = "Links";

            }
            else if (ls_about.IsSelected)
            {

               MyFrame.Navigate(typeof(frm_about));
                label_head.Text = "Info über...";

            }
            else if (ls_plan.IsSelected)
            {

               

            }
            else if (ls_room.IsSelected)
            {

                MyFrame.Navigate(typeof(room_search));
                label_head.Text = "Raumsuche";

            }

            else if (ls_termin.IsSelected)
            {

                MyFrame.Navigate(typeof(Forms.frm_times));
                label_head.Text = "Semesterzeiten";

            }
            else if (ls_mensa.IsSelected)
            {

                MyFrame.Navigate(typeof(frm_mensa));
                label_head.Text = "Mensa";

            }
            
            else if (ls_settings.IsSelected)
            {

                MyFrame.Navigate(typeof(Forms.frm_settings));
                label_head.Text = "Einstellungen";

            }

            MySplitView.IsPaneOpen = false;
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var q = e.SourcePageType;


            if (q == typeof(frm_room_view))
            {

                label_head.Text = "Freie Räume";
            }
            if (q == typeof(frm_mensa))
            {

                label_head.Text = "Mensa";
            }
            if (q == typeof(room_search))
            {

                label_head.Text = "Raumsuche";
            }
            if (q == typeof(frm_start))
            {

                label_head.Text = "Start";
            }
            if (q == typeof(Forms.frm_times))
            {

                label_head.Text = "Semesterzeiten";
            }
            if (q == typeof(Forms.frm_settings))
            {

                label_head.Text = "Einstellungen";
            }
        }

    }
}
