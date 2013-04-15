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
	public class MorseKeyTableTests
	{
		[Test()]
		public void MorseKeyTableBasicTest ()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.IsNotNull(mkt);
		}
		
		[Test()]
		public void TestA()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual(".-", mkt.GetMorse("A"));
			Assert.AreEqual("A", mkt.GetAlpha(".-"));
		}
		
		[Test()]
		public void TestE()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual(".", mkt.GetMorse("e"));
			Assert.AreEqual("E", mkt.GetAlpha("."));
		}
		
		[Test()]
		public void TestR()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual(".-.", mkt.GetMorse("R"));
			Assert.AreEqual("R", mkt.GetAlpha(".-."));
		}
		
		[Test()]
		public void TestS()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual("...", mkt.GetMorse("S"));
			Assert.AreEqual("S", mkt.GetAlpha("..."));
		}
		
		[Test()]
		public void TestT()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual("-", mkt.GetMorse("T"));
			Assert.AreEqual("T", mkt.GetAlpha("-"));
		}

		[Test()]
		public void TestL()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual(".-..", mkt.GetMorse("L"));
			Assert.AreEqual("L", mkt.GetAlpha(".-.."));
		}
		
		[Test()]
		public void TestN()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual("-.", mkt.GetMorse("N"));
			Assert.AreEqual("N", mkt.GetAlpha("-."));
		}
		
		[Test()]
		public void TestI()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual("..", mkt.GetMorse("I"));
			Assert.AreEqual ("I", mkt.GetAlpha(".."));
		}

		[Test()]
		public void TestO()
		{
			MorseKeyTable mkt = new MorseKeyTable();
			Assert.AreEqual("---", mkt.GetMorse("O"));
			Assert.AreEqual ("O", mkt.GetAlpha("---"));
		}


	}
}

