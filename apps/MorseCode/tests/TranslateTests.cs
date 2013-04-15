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
using NUnit.Framework;

namespace MorseCode
{
	[TestFixture()]
	public class TranslateTests
	{
		[Test()]
		public void TranslateBasicTests ()
		{
			Translate trans = new Translate();
			Assert.IsNotNull(trans);
		}

		[Test()]
		public void AlphaToMorseBasicCharTest()
		{
			Translate trans = new Translate();
			
			Assert.AreEqual (".", trans.AlphaToMorse("E"));
		}
		
		[Test()]
		public void MorseToAlphaBasicWordTest()
		{
			Translate trans = new Translate();
			string morseLine = "... --- ...";
			Assert.AreEqual ("SOS", trans.MorseToAlpha(morseLine));
		}
		
		[Test()]
		public void MorseToAlphaBasicSentenceTest()
		{
			Translate trans = new Translate();
			string morseLine = "... --- ...       ... . .-.. .-..       .- -       .-. .. .-.. .";
			Assert.AreEqual ("SOS SELL AT RILE", trans.MorseToAlpha(morseLine));
		}
		
		[Test()]
		public void MorseToAlphaBadSpacesTest()
		{
			//Need seven spaces to indicate new word. 
			string morseLine = "--- ..    .       ... ...";
			Translate trans = new Translate();

			Assert.AreEqual ("OIE SS", trans.MorseToAlpha(morseLine));
		}
		
		[Test()]
		public void AlphaToMorseSimpleWordTest()
		{
			Translate trans = new Translate();
			
			string alphaLine = "SOS";
			Assert.AreEqual("... --- ...", trans.AlphaToMorse(alphaLine));
		}
		
		[Test()]
		public void AlphaToMorseSimpleSentenceTest()
		{
			Translate trans = new Translate();
			string alphaLine = "SOS AT RELINE";
			string morseLine = "... --- ...       .- -       .-. . .-.. .. -. .";
			Assert.AreEqual(morseLine, trans.AlphaToMorse(alphaLine));
		}
	}
}

