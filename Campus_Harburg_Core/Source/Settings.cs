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

using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;


namespace Campus_Harburg_Core
{
    public class settings
    {
        public async void writeCollection(API_InternalFile file, Settings_Config collection)
        {
            string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            await file.writeText("settings.json", json);
        }

        public async Task<Settings_Config> readCollection(API_InternalFile file)
        {

            try
            {
               string text = await file.readText("settings.json");
                Settings_Config list = JsonConvert.DeserializeObject<Settings_Config>(text);
                return list;
            }
            catch (FileNotFoundException ex)
            {
                Settings_Config config = new Settings_Config();
                config.start = 0;

                return config;

            }
        }
    }

    public class Settings_Config
    {
        public int start { get; set; }
    }
}

