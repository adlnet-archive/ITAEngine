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
using MorseCode;

namespace MorseCode
{
	public class Translate
	{
		private MorseKeyTable mkt; 
		
		public Translate ()
		{
			mkt = new MorseKeyTable();
		}
		
		public string AlphaToMorse(string line)
		{
			string morseLine = "";
			string [] words =  line.Split(new char [] {' '}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string word in words)
			{
				foreach (char alpha in word)
				{
					morseLine += mkt.GetMorse(alpha.ToString());
					morseLine += " ";
				}
				morseLine += "      "; 
			}
			
			return morseLine.Trim ();
		}
		
		public string MorseToAlpha(string line)
		{
			string alpha = "";
			foreach (string word in SplitMorseWords(line))
			{
				foreach (string morse in word.Split(new char [] {' '}, StringSplitOptions.RemoveEmptyEntries))
				{
					alpha += mkt.GetAlpha(morse);
				}
				alpha += " "; 
			}
			
			return alpha.Trim();
		}
		
		private string[] SplitMorseWords(string line)
		{
			string wordSpace = "       ";
			return line.Split(new string [] { wordSpace }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}

