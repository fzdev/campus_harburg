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

using System;
using System.IO;
using System.Threading.Tasks;
using Campus_Harburg_Core;

namespace Campus_Harburg_Android
{
    class InternalFile  : Campus_Harburg_Core.API_InternalFile
    {

        async Task API_InternalFile.writeText(string filename, string text)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string settingsPath = Path.Combine(path, filename);

            StreamWriter stream = System.IO.File.CreateText(settingsPath);
            stream.Write(text);
            stream.Close();
            return;
        }

         async Task<string> API_InternalFile.readText(string filename)
        {
           
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string settingsPath = Path.Combine(path, filename);

                StreamReader reader = System.IO.File.OpenText(settingsPath);
                string text = reader.ReadToEnd();
                reader.Close();
                return text;
            
            
        }
    }

}