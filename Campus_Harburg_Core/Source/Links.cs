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

using System.Collections.Generic;

namespace Campus_Harburg_Core
{
    public class Links
    {
        List<Links_Item> linkList = new List<Links_Item>();
        public void init()
        {
            Links_Item item1 = new Links_Item();
            item1.name = "E-Learning (Stud.IP)";
            item1.url = "https://e-learning.tu-harburg.de/";
            item1.categorie = "Selbstverwaltung";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Webmail";
            item1.url = "https://webmail.tu-harburg.de/horde/login.php";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Drucken (STAU)";
            item1.url = "https://stau.rz.tu-harburg.de/drucker/studi_3.0";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Intranet";
            item1.url = "https://intranet.tuhh.de/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Selbsbedienungsportal (SOS)";
            item1.url = "https://www.service.tuhh.de/sos/";
            linkList.Add(item1);


            item1 = new Links_Item();
            item1.name = "Bibiliothek";
            item1.url = "https://www.tub.tuhh.de/";
            item1.categorie = "Allgemeines";
            linkList.Add(item1);


            item1 = new Links_Item();
            item1.name = "Studierendenparlament (STUPA)";
            item1.url = "https://www.tuhh.de/stupa/startseite.html";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "AStA";
            item1.url = "http://asta.tu-harburg.de/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Prüfungsordnungen";
            item1.url = "https://www.tuhh.de/tuhh/studium/studieren/pruefungsordnungen.html";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Prüfungstermine";
            item1.url = "http://intranet.tuhh.de/stud/pruefung/index.php3";
            linkList.Add(item1);


            item1 = new Links_Item();
            item1.name = "TalkIng Forum";
            item1.url = "http://forum.tu-talking.de";
            linkList.Add(item1);


            item1 = new Links_Item();
            item1.name = "Fachschaft AIW/GES/MED";
            item1.url = "https://aiw-ges.de/de/";
            item1.categorie = "Fachschaften";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft Bau- und Umweltingenieurswesen";
            item1.url = "https://www.tuhh.de/fsrb/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft ET/IT";
            item1.url = "https://www.fsr-etit.de";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft GTW";
            item1.url = "https://tuhh-gewerbelehrer.de/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft Maschinenbau";
            item1.url = "https://mb-tuhh.de/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft Managment-Wissenschaft-Technologie";
            item1.url = "https://www.tuhh.de/fsr-mwt/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft Schiffbau";
            item1.url = "http://www.hf-latte.de/";
            linkList.Add(item1);

            item1 = new Links_Item();
            item1.name = "Fachschaft Verfahrenstechnik";
            item1.url = "https://www.tuhh.de/fsrv/aktuelle-termine.html";
            linkList.Add(item1);
        }

        public List <Links_Item> getList()
        {
            return linkList;
        }


    }
}
