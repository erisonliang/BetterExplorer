// BExplorer.Shell - A Windows Shell library for .Net.
// Copyright (C) 2007-2009 Steven J. Kirk
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either 
// version 2 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public 
// License along with this program; if not, write to the Free 
// Software Foundation, Inc., 51 Franklin Street, Fifth Floor,  
// Boston, MA 2110-1301, USA.
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BExplorer.Shell {

	/// <summary>Represents items/filters in a FileFilterComboBox</summary>
	internal class FilterItem {
		public string Caption, Filter;

		public FilterItem(string caption, string filter) {
			Caption = caption;
			Filter = filter;
		}

		public bool Contains(string filter) {
			return Filter.Split(',').Any(x => x.Trim() == filter);
			/*
			string[] filters = Filter.Split(',');
			foreach (string s in filters) {
				if (filter == s.Trim()) return true;
			}
			return false;
			*/
		}

		public override string ToString() {
			string filterString = string.Format(" ({0})", Filter);

			if (Caption.EndsWith(filterString)) {
				return Caption;
			}
			else {
				return Caption + filterString;
			}
		}

		/*
		[Obsolete("Not Used", true)]
		public static FilterItem[] ParseFilterString(string filterString) {
			int dummy;
			return ParseFilterString(filterString, string.Empty, out dummy);
		}
		*/

		/// <summary>
		/// Takes a string (representing a list of filters like: "txt|All files|") and converts it into a FilterItem[]
		/// </summary>
		/// <param name="filterString">The string representing a list of filters like: "txt|All files|"</param>
		/// <param name="existing">Not Sure</param>
		/// <param name="existingIndex">Not Sure</param>
		/// <returns></returns>
		public static FilterItem[] ParseFilterString(string filterString, string existing, out int existingIndex) {
			//TODO: Find out why we have existingIndex
			var result = new List<FilterItem>();
			existingIndex = -1;

			string[] items = filterString != string.Empty ? filterString.Split('|') : new string[0];

			if ((items.Length % 2) != 0) {
				throw new ArgumentException(
					@"Filter string you provided is not valid. The filter string must contain a description of the filter, 
					followed by the vertical bar (|) and the filter pattern. The strings for different filtering options must also be 
					separated by the vertical bar. Example: " + "\"Text files|*.txt|All files|*.*\""
				);
			}

			for (int n = 0; n < items.Length; n += 2) {
				FilterItem item = new FilterItem(items[n], items[n + 1]);
				result.Add(item);
				if (item.Filter == existing) existingIndex = result.Count - 1;
			}

			return result.ToArray();
		}
	}
}
