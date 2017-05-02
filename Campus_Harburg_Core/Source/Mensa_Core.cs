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
using System;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Campus_Harburg_Core
{
    public class Mensa_Core
    {
        //Interne Sammlung an Mensaeinträgen
        public Struct_Mensa_Collection _collection;

        API_InternalFile fileIO;

        //Setzt den zu verwendenden Dateioperator
        public void setFileIO(API_InternalFile fileApi)
        {
            fileIO = fileApi;
        }

        // Initialisiert die Klasse und lädt die gespeicherten Daten
        public async Task<bool> init()
        {
            //Aktiven Datensatz aus Offlinedatei lesen
            _collection = await readCollection();
            return true;
        }

        // aktualsisiert und speichert die lokale Liste
        public async Task<bool> sync()
        {
            //Aktuellen Wochentag bestimmen
            int dayID = (int)DateTime.Now.DayOfWeek;
            //Anzahl der abzurufenden Tage
            int count = 0;

            //Es ist ein Samstag  oder Sonntag, nur die Daten für die kommende
            //Woche herunterladen
            if (dayID == 0)    
            {
                count = 6;
            }
            else if (dayID == 6)
            {
                count = 7;
            }
            //Es ist ein Werktag, verbleibende Tage der aktuellen Woche und komplette
            //nächste Woche herunterladen
            else
            {
                count = 12 - (dayID - 1);
            }
    
            //Neue temproräre Sammlung anlegen
            Struct_Mensa_Collection temp_collection = new Struct_Mensa_Collection();
            //Datum der Atualisierung auf aktuelles Datum setzen
            temp_collection.date_update = DateTime.Now; 
            //Array für Einträge in vorher bestimmter Größe anlegen
            Struct_Mensa_Day[] items = new Struct_Mensa_Day[count];
            //Temporäres Datum anlegen, mit aktuellen Datum initialisieren
            DateTime temp_date = DateTime.Now;
            //Temp. Zähler für erfolgreiche Datenabrufe
            int temp_count_suc = 0;
            //Schleife für jeden abzurufenden Tag durchlaufen
            for (int i = 0; i < count; i++)
            {
                //Eintrag mit heruntergeladenen Daten füllen
                items[i] = await download(temp_date);
                //Größenangabe erhöhen
                temp_collection.count++;
                //Datum dem Eintrag hinzufügen
                items[i].day = temp_date;
                //Datum für Datenabruf erhöhen
                temp_date = temp_date.AddDays(1);   //Einen Tag hinzurechnen
                //Prüfen ob Eintrag gültig
                if (items[i].isOk)
                {
                    //Zähler für erfolgreiche Abrufe erhöhen
                    temp_count_suc++;
                }
            }

            //Einträge dem Temp. Datensatz hinzufügen
            temp_collection.item = items;
            //Datensatz als gültig markieren
            temp_collection.isValid = true;

            //Wenn mehr als ein Datensatz erfolgreich abgerufen werden konnte
            if (temp_count_suc > 0)
            {
                //Temp. Datensatz als aktiven Datensatz übernehmen
                _collection = temp_collection;
                //Datensatz zur Offlinenutzung speichern
                writeCollection(_collection); 
                //Erfolgreiche Aktualisierung zurückgeben
                return true;
            }
            else
            {
                //Nicht erfolgreiche Aktualisierung zurückgeben
                return false;
            }
        }

        //Lädt den Mensaplan für den angegebenen Tag herunter
        public async Task<Struct_Mensa_Day> download(DateTime day)
        {
            //Temporären Eintrag anlegen
            Struct_Mensa_Day temp_item = new Struct_Mensa_Day();
            //Eintrag als ungültig markieren
            temp_item.isOk = false;
            //Prüfen ob eine Internetverbindung besteht
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            
            if (!isInternetConnected)
            {
                //Es besteht keine Verbindung, Abruf abbrechen und bisherigen ungültigen Eintrag 
                //zurückgeben
                return temp_item;
            }

            try
            {
                //Daten über HTTP Client und URL abrufen
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri("http://openmensa.org/api/v2/canteens/129/days/" + day.Year + "-" + day.Month + "-" + day.Day + "/meals"));
                //JSON Antwort konvertieren
                var jsonString = await response.Content.ReadAsStringAsync();
                Struct_Mensa_Food[] temp_meals = JsonConvert.DeserializeObject<Struct_Mensa_Food[]>(jsonString);
                //Heruntergeladene Gerichte dem Eintrag hinzufügen
                temp_item.food = temp_meals;
                //Eintrag als gültig markieren
                temp_item.isOk = true;
            }
            catch (Exception ex)
            {
                //Eine Ausnahme ist aufgetreten, Eintrag als ungültig annehmen
                temp_item.isOk = false;
            }

            //Erstellten Eintrag zurückgeben
            return temp_item;
        }

        //Datensatz speichern
        public async void writeCollection(Struct_Mensa_Collection collection)
        {
            //Struct in JSON konvertieren
            string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            //Datei schreiben
            await fileIO.writeText("mensa.json",json);
        }

        //Datensatz lesen
        public async Task<Struct_Mensa_Collection> readCollection()
        {
            try
            {
                //Datei lesen
                String text = await fileIO.readText("mensa.json");
                //JSON in Struct konvertieren
                Struct_Mensa_Collection list = JsonConvert.DeserializeObject<Struct_Mensa_Collection>(text);
                //Datensatz zurückgeben
                return list;
            }
            catch (FileNotFoundException ex)
            {
                //Als ungültig markierten Datensatz erstellen und zurückgeben
                Struct_Mensa_Collection collection = new Struct_Mensa_Collection();
                collection.count = 0;
                collection.isValid = false;
                return collection;
            }
        }
    }
}