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
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.Threading.Tasks;

using Campus_Harburg_Core;
using Campus_Harburg_Windows;

namespace Campus_Harburg_UWP.Forms
{
    public sealed partial class frm_settings : Page
    {

        API_InternalFile_UWP file = new API_InternalFile_UWP();

        public frm_settings()
        {
            this.InitializeComponent();
            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int _start = comboBox.SelectedIndex;
            settings st = new Campus_Harburg_Core.settings();

            Settings_Config cfg = new Settings_Config();
            cfg.start = _start;
            st.writeCollection(file,cfg);
        }

        private async void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            settings st = new settings();
            Settings_Config cfg = await st.readCollection(file);

            comboBox.SelectedIndex = cfg.start;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var yesCommand = new UICommand("Ja", async cmd => { await deleteConfig(); });
            var noCommand = new UICommand("Nein", cmd => { });

                var dialog = new MessageDialog("Sollen alle lokalen Daten gelöscht werden?\nDas Löschen der Daten kann nicht rückgängig gemacht werden!", "Löschen bestätigen");

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

        private async Task deleteConfig()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            try
            {
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("mensa.json");
                await sampleFile.DeleteAsync();
                sampleFile = await storageFolder.GetFileAsync("exams.json");
                await sampleFile.DeleteAsync();
                sampleFile = await storageFolder.GetFileAsync("settings.json");
                await sampleFile.DeleteAsync();

            }
            catch (FileNotFoundException ex)
            {


            }

        }


    }
}
