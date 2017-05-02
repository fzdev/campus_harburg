/*
    Campus Harburg
    Copyright � 2016-2017 Frank Zimdars
    
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

    "Campus Harburg" ist Freie Software: Sie k�nnen es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder sp�teren
    ver�ffentlichten Version, weiterverbreiten und/oder modifizieren.

    "Campus Harburg" wird in der Hoffnung, dass es n�tzlich sein wird, aber
    OHNE JEDE GEW�HRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gew�hrleistung der MARKTF�HIGKEIT oder EIGNUNG F�R EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License f�r weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben.Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;

namespace Campus_Harburg_Android
{
    [Activity(Label = "Campus Harburg", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/SplashTheme")]
    public class MainActivity : Activity
    {
        private bool start = true;
        protected override void OnResume()
        {
            base.OnResume();
            if (!start)
            {
                Finish();
            }
            start = false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.SplashTheme);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            base.OnCreate(savedInstanceState);

            var activity2 = new Intent(this, typeof(Activity_Mensa));
            StartActivity(activity2);
        }
    }
}