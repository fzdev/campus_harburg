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
    public class Exams_P
    {

        protected Exams_Core ex = new Exams_Core();
        protected Exams_Collection info = null;

        //Initialisiert die Liste mit den gespeicherten Werten
        public async Task init(API_InternalFile fio)
        {
            ex.fileIO = fio;
            info = await ex.readList();
        }

        public async  Task<bool> update()
        {
            Exams_Collection tempList = await ex.downloadList();

            if(tempList.list.Count != 0)
            {
                ex.writeList(tempList);
                info = tempList;
                return true;
            }else
            {
                return false;
            }
        }

        //Gibt die vollständige Liste zurück
        public Exams_Collection getDisplayList()
        {
            info.list_title_sec = "";
            return info;
        }

        //Gibt die vollständige Liste sortiert nach Namen zurück
        public Exams_Collection getDisplayListByName()
        {
            Exams_Collection exi = info;
            exi.list.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
            info.list_title_sec = "";
            return exi;
        }

        //Prüft ob die Liste älter als 30 Tage ist
        public bool isOld()
        {
            int dayDif = (DateTime.Now - this.info.list_time_download).Days;
            if (dayDif >= 30 && info.list.Count >= 2)
            {
                return true;
            }else
            {
                return false;
            }
        }

        //Prüft ob die Liste leer ist
        public bool isEmpty()
        {
            if (info.list.Count < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Durchsicht die Liste nach einem bestimmten Titel und gibt die gefilterte Liste zurück
        public Exams_Collection search(String text) {

            List<Exams_Item> list = info.list;
            Exams_Collection copy = new Exams_Collection();

            copy.list = new List<Exams_Item>();
            copy.list_data = "";
            copy.list_title = "Suchergebnisse";
            copy.list_title_sec = "für " + text;
            
            foreach(Exams_Item item in list)
            {
                string searchWithinThis = item.Name;
                searchWithinThis = searchWithinThis.ToUpper();
                string searchForThis = text;
                searchForThis = searchForThis.ToUpper();
                int firstCharacter = searchWithinThis.IndexOf(searchForThis);

                if (firstCharacter != -1)
                {
                    copy.list.Add(item);
                }
            }

            return copy;
        }
    }
}

