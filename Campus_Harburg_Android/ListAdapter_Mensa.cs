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
using Android.App;
using Android.Views;
using Android.Widget;

using Campus_Harburg_Core;

namespace Campus_Harburg_Android
{
    public class HomeScreenAdapter : BaseAdapter<Struct_Mensa_Display>
    {
        List<Struct_Mensa_Display> items;
        Activity context;
        public HomeScreenAdapter(Activity context, List<Struct_Mensa_Display> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Struct_Mensa_Display this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.ListView_Mensa, null);
            }
                
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Category;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Food;
           view.FindViewById<TextView>(Resource.Id.Text4).Text = item.Price_Student;
           view.FindViewById<TextView>(Resource.Id.Text5).Text = item.Price_Standard;

            

            return view;
        }
    }
}