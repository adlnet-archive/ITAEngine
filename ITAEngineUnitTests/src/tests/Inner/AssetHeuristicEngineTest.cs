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



//-----------------------------------------------------------------------
// <copyright company="AT&amp;LT">
//     Copyright (c) AT&amp;LT LLC. All rights reserved.
// </copyright>
// <author>Peter Franza</author>
// <email>pfranza@atltgames.com</email>
//-----------------------------------------------------------------------

using NUnit.Framework;
using System;
using ITAEngine.Inner;
using System.Collections.Generic;
using ITAEngine;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("AssetHeuristicEngine Tests")]
	public class AssetHeuristicEngineTest
	{
		[Test, DescriptionAttribute("Tests the scoring of tutoring assets by creating a mock " +
			"recommendation engine that controls the data presented to the Heuristics engine.  " +
			"The assertions verify that the proper ordering of the assets.")]
		public void TestCase ()
		{
			List<string> typeList = new List<string>();
				typeList.Add("Type1");
				typeList.Add("Type2");
				typeList.Add("Type3");
				typeList.Add("Type4");
			
			List<AssetRecomendation> assetList = new List<AssetRecomendation>();
				assetList.Add(new MockAssetRecomendation("Type1", 1));
				assetList.Add(new MockAssetRecomendation("Type2", 2));
				assetList.Add(new MockAssetRecomendation("Type3", 3));
				assetList.Add(new MockAssetRecomendation("Type4", 4));
			
			AssetHeuristicEngine engine = new AssetHeuristicEngine(typeList, assetList);
			Assert.Greater(engine.getScoreForType("Type1"), engine.getScoreForType("Type2"));
			Assert.Greater(engine.getScoreForType("Type2"), engine.getScoreForType("Type3"));
			Assert.Greater(engine.getScoreForType("Type3"), engine.getScoreForType("Type4"));
			
		}

		[Test, DescriptionAttribute("Test with last used times when assets have been used in a " +
			"different order. Assertions test the proper ordering of assets.")]
		public void TestCaseShuffled ()
		{
			List<string> typeList = new List<string>();
				typeList.Add("Type1");
				typeList.Add("Type2");
				typeList.Add("Type3");
				typeList.Add("Type4");
			
			List<AssetRecomendation> assetList = new List<AssetRecomendation>();
				assetList.Add(new MockAssetRecomendation("Type1", 3));
				assetList.Add(new MockAssetRecomendation("Type2", 4));
				assetList.Add(new MockAssetRecomendation("Type3", 1));
				assetList.Add(new MockAssetRecomendation("Type4", 2));
				assetList.Add(new MockAssetRecomendation("Type1", 10));
				assetList.Add(new MockAssetRecomendation("Type2", 20));
				assetList.Add(new MockAssetRecomendation("Type3", 30));
				assetList.Add(new MockAssetRecomendation("Type4", 40));
				assetList.Add(new MockAssetRecomendation("Type1", 50));
				assetList.Add(new MockAssetRecomendation("Type2", 60));
				assetList.Add(new MockAssetRecomendation("Type3", 70));
				assetList.Add(new MockAssetRecomendation("Type4", 80));
			
			AssetHeuristicEngine engine = new AssetHeuristicEngine(typeList, assetList);
			Assert.Greater(engine.getScoreForType("Type3"), engine.getScoreForType("Type4"));
			Assert.Greater(engine.getScoreForType("Type4"), engine.getScoreForType("Type1"));
			Assert.Greater(engine.getScoreForType("Type1"), engine.getScoreForType("Type2"));
			
		}

		internal class MockAssetRecomendation : AssetRecomendation {
			
			private string type;
			private long firstUsed;
			
			public MockAssetRecomendation(string type, long firstUsed) {
				this.type = type;
				this.firstUsed = firstUsed;
			}
			
			public string GetAssetIdentifier(){return "";}
			public string GetAssetType(){return type;}
			public int GetTimesUsed(){return 0;}
			public long GetFirstAccessed(){return firstUsed;}
			public long GetLastAccessed(){return 0L;}
		}
	}
}

