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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Campus_Harburg_Core;


namespace Campus_Harburg_UWP
{

    public sealed partial class room_search : Page
    {
        private String[] gText = { "A+-+SBC1", "D+-+SBC4", "E+-+SBC3", "H+-+SBC5", "I+-+DE22", "K+-+DE15", "L+-+DE17", "M+-+ES42", "N+-+ES40", "O+-+ES38", "Ch4+-+HS28" };

        Campus_Harburg_Core.Rooms_Finder finder = new Campus_Harburg_Core.Rooms_Finder();

        public room_search()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            for(int i = 1;i< 41; i++)
            {

                gui_cap_min.Items.Add(i.ToString());
                gui_cap_max.Items.Add(i.ToString());
            }


            gui_cap_max.Items.Add("47");
            gui_cap_max.Items.Add("50");
            gui_cap_max.Items.Add("80");
            gui_cap_max.Items.Add("98");
            gui_cap_max.Items.Add("100");
            gui_cap_max.Items.Add("135");
            gui_cap_max.Items.Add("170");
            gui_cap_max.Items.Add("302");
            gui_cap_max.Items.Add("680");


            gui_cap_max.SelectedIndex = 39;
            gui_cap_min.SelectedIndex = 0;
            gui_pool.IsChecked = true;
            
            DateTime day = DateTime.Today;
            gui_time_start.SelectedIndex = DateTime.Now.Hour;
            gui_time_end.SelectedIndex = (DateTime.Now.Hour + 2)%24;
            gui_dur.SelectedIndex = 1;

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            startSearch();
        }
    
        async void startSearch()
        {
            Struct_SearchConfig_Room rcfg = new Struct_SearchConfig_Room();
            rcfg.cap_min = Int16.Parse(gui_cap_min.SelectedValue.ToString());
            rcfg.cap_max = Int16.Parse(gui_cap_max.SelectedValue.ToString());
            rcfg.date_day = gui_date.Date.LocalDateTime.Day;
            rcfg.date_month = gui_date.Date.LocalDateTime.Month;
            rcfg.date_year = gui_date.Date.LocalDateTime.Year;

            rcfg.time_start = Int16.Parse(gui_time_start.SelectedIndex.ToString());
            rcfg.time_end = Int16.Parse(gui_time_end.SelectedIndex.ToString());
            rcfg.duration = (gui_dur.SelectedIndex + 1);

            if (rcfg.time_end == 0)
            {
                rcfg.time_end = 24;
            }
            if (rcfg.time_end <= rcfg.time_start)
            {

                MessageDialog dialog = new MessageDialog("Der gewählte Zeitraum ist ungültig. Die Endzeit muss später als die Startzeit sein!", "Ungültige Eingabe");
                await dialog.ShowAsync();
                return;

            }

            int sel = gui_dur.SelectedIndex;
            int dh = sel;


            if (rcfg.time_end - rcfg.time_start <= dh)
            {

                MessageDialog dialog = new MessageDialog("Der Zeitraum ist zu kurz für die gewählte Dauer", "Ungültige Eingabe");
                await dialog.ShowAsync();
                return;

            }

            if (gui_pool.IsChecked == true)
            {
                rcfg.withPool = false;
            }
            else
            {
                rcfg.withPool = true;
            }


            Object[] parameters = new Object[5];
            
            Room_Result res = await finder.getResult(rcfg);

            parameters[0] = finder.dataToString(res.list);
            this.Frame.Navigate(typeof(frm_room_view), parameters);

        }
    }
}
