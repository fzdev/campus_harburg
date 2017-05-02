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

namespace Campus_Harburg_Core
{
    // Mensa Datensatz, bestehent aus mehreren Tageseinträgen
    public struct Struct_Mensa_Collection
    {
        //sammlung von Tageseinträgen
        public Struct_Mensa_Day[] item;
        // Datum der letzten Aktualisierung
        public DateTime date_update;
        // Gibt an ob der Datensatz gültig ist
        public bool isValid;
        //Anzahl der Einträge
        public int count;          
    }

    //Mensa Datensatz, Eintrag für einen einzelnen Tag
    public struct Struct_Mensa_Day
    {
        //Gibt an ob der Datensatz gültig ist
        public bool isOk { get; set; } 
        //Gibt an ob der Datensatz angezeigt werden soll
        public bool isAvaiable { get; set; }

        public DateTime day;                        
        public Struct_Mensa_Food[] food { get; set; }      //Essenseinträge

    }

    public class Struct_Mensa_Food
    {
        public string name { get; set; }
        public string category { get; set; }
        public Struct_Mensa_Price prices { get; set; }
        public List<string> notes { get; set; }
    }

    //Bildet den Mensapreis ab
    public class Struct_Mensa_Price
    {
        //Preis für Studenten
        public double students { get; set; }
        //Preis für Angestellte
        public double employees { get; set; }
        //Preis für Schüler (in Harburg nicht verwendet)
        public object pupils { get; set; }
        //Preis für externe (in Harburg nicht verwendet)
        public object others { get; set; }
    }

}
