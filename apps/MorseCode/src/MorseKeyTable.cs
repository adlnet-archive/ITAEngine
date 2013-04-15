// Copyright (c) December 2012
// All Rights Reserved
// Advanced Training & Learning Technology, LLC
// 4445 Corporation Lane, Suite 207
// Virginia Beach, VA 23462
// admin@atlt-llc.com

// Parts of this software package was produced for the United States Government 
// under PM TRASYS Contract # M67854-12-C-8088.  

// Advanced Training & Learning Technology, LLC is providing this code under the
// GNU Lesser General Public License, version 2.1 or greater, while reserving 
// the right to distribute under another license. A copy of the LGPL license 
// should have been provided with this software distribution.  If not, a copy 
// of this license can be found at 
// http://www.gnu.org/licenses/lgpl-2.1.html.



using System;
using System.Collections.Generic;

namespace MorseCode
{
	public class MorseKeyTable
	{
		Dictionary<string, string> alphaKeys;
		Dictionary<string, string> morseKeys;
		
		public MorseKeyTable ()
		{
			alphaKeys = new Dictionary<string, string>();
			morseKeys = new Dictionary<string, string>();
			LoadAlphaKeys();
			LoadMorseKeys();
		}
		
		private void LoadAlphaKeys()
		{
			alphaKeys.Add ("A", ".-");
			alphaKeys.Add ("E", ".");
			alphaKeys.Add ("R", ".-.");
			alphaKeys.Add ("S", "...");
			alphaKeys.Add ("T", "-");
			alphaKeys.Add ("L", ".-..");
			alphaKeys.Add ("N", "-.");
			alphaKeys.Add ("I", "..");
			alphaKeys.Add ("O", "---");
		}

		private void LoadMorseKeys()
		{
			foreach (string alpha in alphaKeys.Keys)
			{
				morseKeys.Add(alphaKeys[alpha], alpha);
			}
		}
		
		public string GetMorse(string alphaChar)
		{
			if (!alphaKeys.ContainsKey(alphaChar.ToUpper()))
				return "0";
			else
				return alphaKeys[alphaChar.ToUpper()];
		}

		public string GetAlpha(string morseString)
		{
			if(!morseKeys.ContainsKey(morseString))
				return "0";
			else 
				return morseKeys[morseString];
		}
	}
}

