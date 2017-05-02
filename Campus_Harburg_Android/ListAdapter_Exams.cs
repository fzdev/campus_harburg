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
using Android.App;
using Android.Views;
using Android.Widget;

namespace Campus_Harburg_Android
{
    public class ListAdapter_Exams : BaseAdapter<Campus_Harburg_Core.Exams_Item>
    {
        List<Campus_Harburg_Core.Exams_Item> items;
        Activity context;
        public ListAdapter_Exams(Activity context, List<Campus_Harburg_Core.Exams_Item> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Campus_Harburg_Core.Exams_Item this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }


        private string subRem(string ins)
        {
            if(ins == "Ö")
            {
                return "O";
            }
            if (ins == "ö")
            {
                return "o";
            }
            if (ins == "Ä")
            {
                return "A";
            }
            if (ins == "ä")
            {
                return "a";
            }
            if (ins == "Ü")
            {
                return "U";
            }
            if (ins == "ü")
            {
                return "u";
            }
            return ins;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListView_Exams, null);
            }

            TextView text_change = view.FindViewById<TextView>(Resource.Id.Text6);

            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Extra;


            if(item.Time == "" && item.Note == "")
            {
                view.FindViewById<TextView>(Resource.Id.Text4).Text = "";
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.Text4).Text = item.Time + ", " + item.Note;
            }

            view.FindViewById<TextView>(Resource.Id.Text5).Visibility = ViewStates.Gone;


            if(item.change == "")
            {
                text_change.Visibility = ViewStates.Gone;
                text_change.Text = "";
            }else
            {
                text_change.Text = item.change;
            }

            view.FindViewById<TextView>(Resource.Id.Text6).Text = item.change;

            TextView separator = view.FindViewById<TextView>(Resource.Id.seperator);

            if (position == 0)
            {
                separator.Visibility = ViewStates.Visible;
                separator.Text = item.Name.Substring(0, 1);
                      
            }

            else if(subRem(item.Name.Substring(0,1)) == subRem(items[position - 1].Name.Substring(0, 1)))
            {
                separator.Visibility = ViewStates.Gone;
                separator.Text = "";
            }
            else
            {
                
                separator.Visibility = ViewStates.Visible;
                separator.Text = item.Name.Substring(0, 1);

            }

            return view;
        }
    }
}