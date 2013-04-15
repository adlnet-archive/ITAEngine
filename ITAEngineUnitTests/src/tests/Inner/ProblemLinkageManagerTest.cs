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
using ITAEngine.Inner;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("ProblemLinkageManager Tests")]
	public class ProblemLinkageManagerTest
	{
		[Test, DescriptionAttribute("Test construction. Assert that the object is not null.")]
		public void BasicTest()
		{
			AssetsDataManager alm = new AssetsDataManager();
			ProblemLinkageManager plm = new ProblemLinkageManager(alm);
			Assert.IsNotNull(plm);
		}
		
		[Test, DescriptionAttribute("Test adding new linkages. The assertion verifies that the linkage exists")]
		public void AddLinkage()
		{
			AssetsDataManager alm = new AssetsDataManager();
			ProblemLinkageManager plm = new ProblemLinkageManager(alm);
			plm.AddProblemLinkage("id", new string [] {"asset1"});
			Assert.AreEqual("asset1", plm.GetAssets("id")[0]);
		}
		
		[Test, DescriptionAttribute("Test adding multiple assets to one problem. Assertions check the  values of the assets for the problem id.")]
		public void AddMultipleProblemLinks()
		{
			AssetsDataManager alm = new AssetsDataManager();
			ProblemLinkageManager plm = new ProblemLinkageManager(alm);
			
			plm.AddProblemLinkage("id1", "asset1");
			plm.AddProblemLinkage("id2", new string [] { "asset2", "asset3"});
			
			Assert.AreEqual("asset1", plm.GetAssets("id1")[0]);
			Assert.AreEqual("asset2", plm.GetAssets("id2")[0]);
			Assert.AreEqual("asset3", plm.GetAssets("id2")[1]);
		}
	}
}

