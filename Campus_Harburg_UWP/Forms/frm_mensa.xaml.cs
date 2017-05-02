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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Campus_Harburg_Core;
using Campus_Harburg_Windows;
using System.Collections.Generic;
using Windows.UI.Popups;


namespace Campus_Harburg_UWP
{

    public sealed partial class frm_mensa : Page
    {
        DateTime today = DateTime.Today;
        DateTime day = DateTime.Today;
        int index = 0;
        int limit = 0;

        Mensa mensa = new Mensa();
        API_InternalFile_UWP file = new API_InternalFile_UWP();

        public frm_mensa()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Initialisierung der Oberfläche und Aktualisierung der Daten
            progressRing.IsActive = true;
            await mensa.init(file);
            await mensa.update();
            progressRing.IsActive = false;
            List<Struct_Mensa_Display> list = mensa.getCurrentList();
            displayItems(list,0);
            checkActions();
            gui_title.Text= mensa.getHeaderText(true);
        }

        private void displayItems(List<Struct_Mensa_Display> items,int _index)
        {
            Display.Items.Clear();

            if (items.Count == 0)
            {
                DItem_Mensa x = new DItem_Mensa();
                x.title = "Fehler";
                x.price_student = "";
                x.price_normal = "";
                x.cat = "Es stehen keine Daten zur Verfügung!";

                Display.Items.Add(x);
                return;
            }

             for (int j = 0; j < items.Count; j++)
             {
                DItem_Mensa x = new DItem_Mensa();
                x.title = items[j].Food;
                x.price_student = items[j].Price_Student;
                x.price_normal = items[j].Price_Standard;
                x.cat = items[j].Category;
                
                Display.Items.Add(x);
             }

        }

        struct DItem_Mensa
        {
            public string title { get; set; }
            public string cat { get; set; }
            public string notes { get; set; }
            public string price_student { get; set; }
            public string price_normal { get; set; }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            mensa.nextDay();
            displayItems(mensa.getCurrentList(), 0);
            gui_title.Text = mensa.getHeaderText(true);
            checkActions();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            mensa.prevDay();
            displayItems(mensa.getCurrentList(), 0);
            gui_title.Text = mensa.getHeaderText(true);
            checkActions();
        }

        private void checkActions()
        {
            if (mensa.canGoBack())
            {
                gui_back.IsEnabled = true;
            }
            else
            {
                gui_back.IsEnabled = false;
            }

            if (mensa.canGoForward())
            {
                gui_forward.IsEnabled = true;
            }
            else
            {
                gui_forward.IsEnabled = false;
            }
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog(mensa.getInfoText(), "Info");
            await dialog.ShowAsync();
        }
    }
}
