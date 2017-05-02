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
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Campus_Harburg_Core;
using Campus_Harburg_Windows;
using Windows.UI.Xaml.Data;

namespace Campus_Harburg_UWP
{

    public sealed partial class frm_exams : Page
    {

        String lastLetter = "";
        API_InternalFile_UWP file = new API_InternalFile_UWP();
        Exams_P exams = new Exams_P();
        Exams_Collection collection = new Exams_Collection();

        public frm_exams()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await init();
            showData(exams.getDisplayListByName());
            testData();
        }

        private async Task init()
        {
            await exams.init(file);
        }

        private async void testData()
        {
            var yesCommand = new UICommand("Ja", async cmd => { await updateData(); });
            var noCommand = new UICommand("Nein", cmd => {  });

            if (exams.isOld())
            {
                var dialog = new MessageDialog(TextContainer.Exams_TooOld, "Hinweis");

                dialog.Options = MessageDialogOptions.None;
                dialog.Commands.Add(yesCommand);

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 0;

                if (noCommand != null)
                {
                    dialog.Commands.Add(noCommand);
                    dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
                }

                await dialog.ShowAsync();
            }

            if (exams.isEmpty())
            {
                var dialog = new MessageDialog(TextContainer.Exams_Empty, "Hinweis");


                dialog.Options = MessageDialogOptions.None;
                dialog.Commands.Add(yesCommand);

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 0;

                if (noCommand != null)
                {
                    dialog.Commands.Add(noCommand);
                    dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
                }

                await dialog.ShowAsync();
            }
        }

        private  void showData(Exams_Collection col)
        {
            progressRing.IsActive = true;
            Display.Items.Clear();
            collection = col;

            DItem_Exams q = new DItem_Exams();
            q.color = "#8B0000";
            q.margin = "0 0 50 0";
            q.title = collection.list_title;
            q.vis1 = false;
            q.color1 = "#FFFFFF";
            Display.Items.Add(q);


            foreach (Exams_Item item in collection.list)
            {
                addItem(item);
            }

            progressRing.IsActive = false;
        }

        private async Task updateData()
        {
            progressRing.IsActive = true;
            await exams.update();
            showData(exams.getDisplayListByName());
        }

        private void searchData(String text)
        {
            Exams_Collection coll;
            if (text != "")
            {
                coll = exams.search(text);
            }
            else
            {
                coll = exams.getDisplayListByName();
            }

            showData(coll);

        }

        private void addItem(Exams_Item item)
        {
            DItem_Exams _ditem_exams = new DItem_Exams();
            _ditem_exams.title = item.Name;
            _ditem_exams.time = item.Time + "," + item.Note;
            _ditem_exams.note = item.Extra;
            _ditem_exams.place = item.change;
            _ditem_exams.margin = "0 0 0 0";
            _ditem_exams.vis1 = true;
            _ditem_exams.color1 = "#000000";
            string letter = _ditem_exams.title.Substring(0, 1);

            if(lastLetter != letter)
            {
                DItem_Exams q = new DItem_Exams();
                q.color = "#e64a19";
                q.margin = "0 0 50 0";
                lastLetter = letter;
                q.title = " "+ letter;
                q.vis1 = false;
                q.color1 = "#FFFFFF";
                Display.Items.Add(q);
            }
            else
            {
                
            }
            Display.Items.Add(_ditem_exams);
        }

        private async void bar1_Click(object sender, RoutedEventArgs e)
        {
            var dialog1 = new ContentDialog1();
            var result = await dialog1.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                var text = dialog1.Text;
                if(text == null)
                {
                    text = "";
                }
                searchData(text);
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await updateData();
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            String text = "Titel: " + exams.getDisplayList().list_title + "\nHeruntergeladen: " + exams.getDisplayList().list_data + "\nEinträge: " + exams.getDisplayList().list.Count.ToString();

            var dialog = new MessageDialog(text, "Info");
            await dialog.ShowAsync();
        }
    }

    public class DItem_Exams
    {
        //Fach das angezeigt wird
        public string title { get; set; }

        //Raum der angezeigt wird
        public string place { get; set; }

        //Uhrzeit die angezeigt wird
        public string time { get; set; }

        //Hinweis der angezeigt wird
        public string note { get; set; }

        public string margin { get; set; }
        public string color { get; set; }
        public string color1 { get; set; }
        public bool vis1 { get; set; }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public BooleanToVisibilityConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is Visibility && (Visibility)value == Visibility.Visible);
        }
    }

}
