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

using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Campus_Harburg_Core
{
    public class Exams_Core
    {
       public API_InternalFile fileIO { get;  set; }

        //Liest die Klausurenliste aus einer Datei
        public async Task<Exams_Collection> readList()
        {
            try
            {
                string text = await fileIO.readText("exams.json");
                Exams_Collection list = JsonConvert.DeserializeObject<Exams_Collection>(text);
                return list;
            }
            catch (Exception ex)
            {
                Exams_Collection nl = new Exams_Collection();
                nl.list_data = "** E-01 **";
                nl.list_title = "Fehler";

                List<Exams_Item> li = new List<Exams_Item>();
                Exams_Item xi = new Exams_Item();
                xi.Name = "Aktualisierung notwendig";
                xi.Place = "";
                xi.Time = "";
                xi.Extra = "";
                xi.change = "Es sind keine Offlinedaten verfügbar. Zum Abrufen der Daten auf 'Aktualisieren' drücken.";
                li.Add(xi);
                nl.list = li;
                return nl;
            }
        }

        //Schreib die Klausurenliste in eine Datei
        public async void writeList(Exams_Collection list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            await fileIO.writeText("exams.json", json);
        }

        public async Task<Exams_Collection> downloadList()
        {
            Exams_Collection ei = new Exams_Collection();
            ei.list_data = "** E-02 **";
            ei.list_title = "Fehler";

            List<Exams_Item> li = new List<Exams_Item>(1000);
            ei.list = li;

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var cookieContainer = new CookieContainer();
                handler.CookieContainer = cookieContainer;

                HttpClient client = new HttpClient(handler);

                HttpResponseMessage response = await client.GetAsync(new Uri("https://intranet.tuhh.de/stud/pruefung/index.php3"));

                var htxt = await response.Content.ReadAsStringAsync();

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

               
                htmlDoc.OptionFixNestedTags = true;

                //HTML aus String laden
                htmlDoc.LoadHtml(htxt); 

                
                if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
                {
                    

                }
                else
                {

                    foreach (HtmlNode cols in htmlDoc.DocumentNode.Descendants("h2"))
                    {

                        String rawString = WebTools.removeHTML(cols.InnerText);

                        Char delimiter = ':';
                        String[] substrings = rawString.Split(delimiter);
                        ei.list_title = substrings[1];

                    }

                    foreach (HtmlNode table in htmlDoc.DocumentNode.Descendants("tbody"))
                    {
                        foreach (HtmlNode row in table.Descendants("tr"))
                        {
                            List<string> facts = new List<string>();
                            foreach (HtmlNode cell in row.Descendants("td"))
                            {
                                facts.Add(cell.InnerHtml);
                            }

                            if (facts.Count >= 4)
                            {
                                Exams_Item x = new Exams_Item();

                                String blank = facts[2];
                                String[] split= blank.Split(new string[] { "<br>" }, StringSplitOptions.None);

                                x.Time = WebTools.removeHTML(facts[1]);
                                x.Name = WebTools.removeHTML(facts[0]);
                                x.Extra = WebTools.removeHTML(split[0]);
                                x.Note = WebTools.removeHTML(facts[3]);

                                if(split.Length > 1)
                                {
                                    x.change = WebTools.removeHTML(split[1]);
                                }
                                else
                                {
                                    x.change = "";
                                }
                                li.Add(x);
                            }
                        }
                    }

                    DateTime now = DateTime.Today;
                    String dateString = now.Day + "." + now.Month + "." + now.Year;

                    ei.list_data = dateString;
                    ei.list_time_download = DateTime.Today;

                }

            }
            catch
            {

            }

            return ei;
        }


    }
}
