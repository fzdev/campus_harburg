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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Campus_Harburg_Core;

namespace Campus_Harburg_UWP
{
    public sealed partial class frm_room_view : Page
    {
        Room_Result res;
        Rooms_Finder finder = new Rooms_Finder();

        public frm_room_view()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Object[] passedParameter = e.Parameter as Object[];

            String sres = passedParameter[0] as String;
            List<Struct_Room> list = finder.dataToList(sres);
            list.Sort((s1, s2) => s1.building.CompareTo(s2.building));

            int pos = 0;


            foreach(Struct_Room item in list)
            {
                DItem_Rooms xi = new DItem_Rooms();
                xi.building = "Gebäude " + item.building;
                xi.room = item.name;
                xi.places =  item.cap + " Plätze";
                xi.time = item.time;
                xi.margin = "0,0,0,0";
                xi.vis = "Visible";
                xi.fcolor = "#00000";
                xi.vis2 = "Collapsed";

                if (pos == 0)
                {

                    DItem_Rooms xi2 = new DItem_Rooms();
                    xi2.building = "Gebäude " + item.building;
                    xi2.room = "";
                    xi2.places = "";
                    xi2.time = "";
                    xi2.margin = "0,0,0,0";
                    xi2.color = "#f44336";
                    xi2.vis = "Collapsed";
                    xi2.vis2 = "Visible";
                    xi2.fcolor = "#FFFFFF";
                    Display.Items.Add(xi2);
                }

                else if (item.building.Substring(0, 1) != list[pos - 1].building.Substring(0, 1))
                {
                    
                    DItem_Rooms xi2 = new DItem_Rooms();
                    xi2.building = "Gebäude " + item.building;
                    xi2.room = "";
                    xi2.places = "";
                    xi2.time = "";
                    xi2.color = "#f44336";
                    xi2.fcolor = "#FFFFFF";
                    xi2.margin = "0,0,0,0";
                    xi2.vis = "Collapsed";
                    xi2.vis2 = "Visible";
                    Display.Items.Add(xi2);

                }

                pos++;

                Display.Items.Add(xi);
            }


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(room_search));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog(sender.ToString() + "*" + e.ToString(), "Ungültige Eingabe");
            await dialog.ShowAsync();
        }
    }

    struct DItem_Rooms
    {
        public string building { get; set; }
        public string room { get; set; }

        public string places { get; set; }

        public string time { get; set; }

        public string color { get; set; }

        public string margin { get; set; }

        public string vis { get; set; }

        public string fcolor { get; set; }
        public string vis2 { get; set; }
    }

}
