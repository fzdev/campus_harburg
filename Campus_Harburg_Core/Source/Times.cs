﻿/*
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

using System.Collections.Generic;

namespace Campus_Harburg_Core
{
    public class Times
    {
        //Liste aller Semester für die Daten verfügbar sind
        List<Times_Item> lst = new List<Times_Item>();

        //Liste generieren und zurückgeben
        public List<Times_Item>  getList()
        {
           
            Times_Item i1 = new Times_Item();
            

            //Sommersemester 2017
            i1 = new Times_Item();
            i1.title = "Sommersemester 2017";
            i1.time_start = "Erster Vorlesungstag 3. April 2016";
            i1.time_end = "Letzter Vorlesungstag 15. Juli 2016";
            i1.time_complete = "1. April bis 30. September 2017";
            i1.time_holidays = "Pfingstferien 4. bis 11. Juni 2017";
            lst.Add(i1);

            //Wintersemester 2017/2018
            i1 = new Times_Item();
            i1.title = "Wintersemester 2016/2017";
            i1.time_start = "Erster Vorlesungstag 16. Oktober 2017";
            i1.time_end = "Letzter Vorlesungstag 3. Februar 2016";
            i1.time_complete = "1. Oktober 2016 bis 31. März 2017";
            i1.time_holidays = "Weihnachtsferien 25. Dezember 2016 bis 6. Januar 2017";
            lst.Add(i1);

            //Sommersemester 2018
            i1 = new Times_Item();
            i1.title = "Sommersemester 2018";
            i1.time_start = "Erster Vorlesungstag 3. April 2016";
            i1.time_end = "Letzter Vorlesungstag 16. Juli 2016";
            i1.time_complete = "1. April bis 30. September 2017";
            i1.time_holidays = "Pfingstferien 21. bis 26. Mai 2017";
            lst.Add(i1);

            return lst;
        }
    }

    

}