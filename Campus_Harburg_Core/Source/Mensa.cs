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
using System.Threading.Tasks;

namespace Campus_Harburg_Core
{
    public class Mensa 
    {

        Mensa_Core core = new Mensa_Core();
        
        int index = 0;
        int limit = 0;
        DateTime currentDay = DateTime.Now;
        DateTime listDay;


        // Initialisieren
        public async Task init(API_InternalFile fi)
        {
            core.setFileIO(fi);
            await core.init();

            //Begrenzung für Index ist die Anzahl der Einträge
            limit = core._collection.count;

            index = calcTimeDif(currentDay, DateTime.Now);

           
            if (index > (limit - 1))
            {
                //Liste hat keinen passenden Eintrag mehr, letzten Eintrag wählen
                index = (limit - 1);
            }
            if (index < 0)
            {
                //Liste ist neuer als Tag (unnormal)
                index = 0;
            }

            //Tag passend zum index bestimmen
            listDay = core._collection.date_update.AddDays(index);
        }
        //Aktualisieren
        public async Task<bool> update()
        {
            DateTime oldTime = core._collection.date_update;
            bool res = await core.sync();      //Daten alutlaisieren
            //listDay = core._collection.date_update;
            //int dt = calcTimeDif(core._collection.date_update, oldTime);

            //Zum Umgehen von Fehlern
            listDay = core._collection.date_update;
            index = 0;
            limit = core._collection.count;

            return res;

        }
        protected int calcTimeDif(DateTime d1,DateTime d2)
        {
            int dif = (d1 - d2).Days;

            if ((d1.Day != d2.Day) && (dif == 0))
            {
                dif = 1;
            }

            return dif;
        }
        public bool nextDay()
        {

            if (index < (limit-1))
            {
                index++;
                listDay = core._collection.item[index].day;
            }
            else
            {
                return false;
            }

            return true;

        }
        public bool prevDay()
        {
            if (index > 0)
            {
                index--;
                listDay = core._collection.item[index].day;
            }
            else
            {
                return false;
            }
            return true;
        }

        //Prüft ob einen Eintrag zurück gegangen werden kann
        public bool canGoBack()
        {
            if(index > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Prüft ob einen Eintrag weiter gegangen werden kann
        public bool canGoForward()
        {
            if (index < (limit - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string buildPriceString(double price)
        {
            return price.ToString("0.00") + " €";

        }
        public List<Struct_Mensa_Display> getCurrentList()
        {

            List<Struct_Mensa_Display> ldi = new List<Struct_Mensa_Display>();    //Lsite zum anzeigen

           if (core._collection.item == null)
           {
            Struct_Mensa_Display di = new Struct_Mensa_Display();
                di.Food = TextContainer.Mensa_Empty_Content;
                di.Description = "";
                di.Category = TextContainer.Mensa_Empty_Title;
                di.Price_Standard = "";
                di.Price_Student = "";
                ldi.Add(di);
                return ldi;
            }

            Struct_Mensa_Day mi = core._collection.item[index];

            if (mi.isOk && mi.food != null)
            {

                foreach(Struct_Mensa_Food fd in mi.food)
                {
                    Struct_Mensa_Display di = new Struct_Mensa_Display();
                    di.Food = fd.name;
                    di.Description = "";
                    di.Category = fd.category;
                    di.Price_Standard = buildPriceString(fd.prices.employees);
                    di.Price_Student = buildPriceString(fd.prices.students);
                    ldi.Add(di);
                }

            }else
            {
                
                Struct_Mensa_Display di = new Struct_Mensa_Display();
                di.Food = "Nicht verfügbar";
                di.Description = "Eventuell hat die Mensa geschlossen.";
                di.Category = "Nicht verfügbar";
                di.Price_Standard = "";
                di.Price_Student = "";
                ldi.Add(di);
            }

            return ldi;
            

        }
        public string getHeaderText(bool longText)
        {

            string[] dayName_long = { "Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag" };
            string[] dayName_short = { "So", "Mo", "Di", "Mi", "Do", "Fr", "Sa" };

            if (longText)
            {
                return "Mensa (" + dayName_short[(int)listDay.DayOfWeek] + " " + listDay.Day + "." + listDay.Month + "." + listDay.Year + ")";
            }
            else
            {
                return "Mensa ("+dayName_short[(int)listDay.DayOfWeek] + " " + listDay.Day + "." + listDay.Month + "." + listDay.Year + ")";
            }

        }

        public string getInfoText()
        {
            return "Mensa Harburg\nDenickestraße 22\n21073 Hamburg\nMontag bis Freitag: 11:00 - 15:00 Uhr\nEssensausgabe bis 30 Minuten vor Schließung";

        }

    }


    


}