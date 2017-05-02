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
using System.Net.Http;
using System.Threading.Tasks;


namespace Campus_Harburg_Core
{

    public class Rooms_Finder
    {
        List<Struct_Room> list = new List<Struct_Room>();
        bool resultOK = false;

        public String dataToString(List<Struct_Room> listX)
        {
            return JsonConvert.SerializeObject(listX, Formatting.Indented);
        }

        public List<Struct_Room> dataToList(String text)
        {
            return JsonConvert.DeserializeObject<List<Struct_Room>>(text);
        }

        public async Task<Room_Result> getResult(Struct_SearchConfig_Room cfg)
        {
            await post(cfg);

            Room_Result res = new Room_Result();
            res.list = this.list;
            res.ok = this.resultOK;

            return res;
        }

        //Raumliste mit HTTP-POST abfragen
        private async  Task post(Struct_SearchConfig_Room cfg)
        {
            try
            {
                Helper h = new Helper();

                String date = h.buildNumber(cfg.date_day) + "." + h.buildNumber(cfg.date_month) + "." + cfg.date_year;

                //Daten zur Übertragung vorbereiten
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                pairs.Add("godatum", date);
                pairs.Add("von", cfg.time_start.ToString());
                pairs.Add("bis", cfg.time_end.ToString());
                pairs.Add("dstunde", cfg.duration.ToString());
                pairs.Add("lehrkapmin", cfg.cap_min.ToString());
                pairs.Add("lehrkapmax", cfg.cap_max.ToString());
                
                if (cfg.withPool)
                {
                    //Wenn Pool gewünscht keine Variable setzen
                }else
                {
                    pairs.Add("pool", "1");
                }
                
                pairs.Add("abgeschickt", " Suche starten ");

                var content = new FormUrlEncodedContent(pairs);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(new Uri("https://intranet.tuhh.de/allgemein/raum/freiraum.php"), content);
                var htxt = await response.Content.ReadAsStringAsync();
                readData(htxt);

            }
            catch (Exception ex)
            {

            }  
        }

        private void readData(String htxt)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                htmlDoc.OptionFixNestedTags = true;
                htmlDoc.LoadHtml(htxt); 

                int count = 0;

                foreach (HtmlNode table in htmlDoc.DocumentNode.Descendants("table"))
                {

                    foreach (HtmlNode row in table.Descendants("tr"))
                    {
                        List<string> facts = new List<string>();
                        foreach (HtmlNode cell in row.Descendants("td"))
                        {
                            facts.Add(cell.InnerText);
                        }

                        if (facts.Count >= 5)
                        {

                            Struct_Room ri = new Struct_Room();

                            ri.building = facts[0];
                            ri.name = facts[1];
                            ri.cap = facts[2];
                            ri.time = facts[3] + " - " + facts[4];

                            list.Add(ri);
                            count++;
                        }
                    }
                }


                if (count == 0)
                {
                    resultOK = false;
                }else
                {
                    resultOK = true;
                }
            }
            catch
            {


            }
        }

        }

    class Helper
    {
        public String buildNumber(int number)
        {
            if (number < 10)
            {
                return "0" + number.ToString();
            }
            else
            {
                return number.ToString();
            }
        }

    }

    public struct Room_Result
    {
        public List<Struct_Room> list { get; set; }
        public bool ok { get; set; }

    }
}
