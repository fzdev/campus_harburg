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

namespace Campus_Harburg_Core
{
    public class TextContainer
    {
        //Info über: Hinweise
        public static string About_Notes = "Diese Anwendung bezieht die Daten von frei zugänglichen Internetseiten. " +
            "Als Datenquelle für die Kategorie 'Mensa' dient OpenMensa (openmensa.org). Die Daten der Kategorien " +
            "'Räume', 'Prüfungen' und 'Semesterzeiten' stammen von der Internetseite der Technischen Universtität " +
            "Hamburg (tuhh.de).\nBei der App handelt es sich um keine offizielle App der genannten Webseiten und " +
            "Organisationen, noch steht diese mit ihnen in Verbindung. Für die Richtigkeit der angezeigten Daten " +
            "kann keine Gewähr übernommen werden. Insbesondere bei wichtigen Daten empfielt sich das Nachschauen" +
            " der Daten auf den entsprechenden Seiten!";
        //Info über: Datenschutz
        public static string About_Privacy = "Es werden keine persönlichen Daten gesammelt oder übertragen. " +
            "Eingegebene Daten werden im Rahmen der Abfrage an die entsprechenden Webserver übermittelt und können " +
            "dort auch gespeichert werden. Hierbei kann auch die IP gespeichert werden.";
        //Info über: Versionsnummer
        public static string About_Version = "1.49";
        //Info über: Builddatum
        public static string About_Date = "27.12.17";

        // Meldung wenn Liste der Prüfungen älter als 30 Tage
        public static string Exams_TooOld = "Die Liste der Prüfungen ist älter als 30 Tage.\nSoll sie jetzt aktualisiert " +
            "werden?\nDas Herunterladen und Verarbeiten der Daten kann einen Moment in Anspruch nehmen.";
        // Meldung wenn Liste der Prüfungen leer ist
        public static string Exams_Empty = "Es existiert noch keine lokale Kopie. Um das Modul 'Prüfungen' nutzen zu können, " +
            "muss diese angelegt werden.\nSoll diese jetzt angelegt werden?\n\nDas Herunterladen und Verarbeiten der Daten kann " +
            "einen Moment dauert. Die Liste kann auch manuell mit Klick auf 'Aktualisieren' angelegt und aktualisiert werden.";

        public static string Mensa_Empty_Title = "Keine Daten";
        public static string Mensa_Empty_Content = "Es sind keine Daten zum Anzeigen vorhanden.";

    }
}
