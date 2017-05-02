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
using Android.Graphics;

namespace Campus_Harburg_Android
{
    public class ListAdapter_Times : BaseAdapter<Campus_Harburg_Core.Times_Item>
    {
        List<Campus_Harburg_Core.Times_Item> items;
        Activity context;
        public ListAdapter_Times(Activity context, List<Campus_Harburg_Core.Times_Item> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Campus_Harburg_Core.Times_Item this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListView_Times, null);
            }

            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.title;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.time_complete;
            view.FindViewById<TextView>(Resource.Id.Text3).Text = item.time_start;
            view.FindViewById<TextView>(Resource.Id.Text4).Text = item.time_end;
            view.FindViewById<TextView>(Resource.Id.Text5).Text = item.time_holidays;

            if(position % 2 == 0)
            {
                view.FindViewById<TextView>(Resource.Id.Text1).SetTextColor(Color.ParseColor("#ff5722"));
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.Text1).SetTextColor(Color.ParseColor("#03a9f4"));
            }
            return view;
        }
    }
}