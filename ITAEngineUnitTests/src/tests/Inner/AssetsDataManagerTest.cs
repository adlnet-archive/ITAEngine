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

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("AssetsDataManager Tests")]
	public class AssetsDataManagerTest
	{
		[Test, DescriptionAttribute("Test adding an asset.  The assertion verifies by getting the asset count.")]
		public void TestAddAsset () {
			AssetsDataManager dataManager = new AssetsDataManager();
			dataManager.AddAsset("Skill", "Type", "ID0", 0, 0, 0);
			Assert.AreEqual(1, dataManager.GetAssetCount());
		}
		
		[Test, DescriptionAttribute("Test getting assets for a skill.  The assertion counts the number of assets for the skill.")]
		public void TestGetAssetsFor () {
			AssetsDataManager dataManager = new AssetsDataManager();
			dataManager.AddAsset("Skill", "Type", "ID0", 0, 0, 0);
			IList<AssetRecomendation>  assets = dataManager.GetAssetsFor("Skill");
			Assert.AreEqual(1, assets.Count);
		}
		
		[Test, DescriptionAttribute("Test marking assets as being completed. Assertions verify the data submitted to MarkCompleted.")]
		public void TestMarkAssetCompleted () {
			AssetsDataManager dataManager = new AssetsDataManager();
			dataManager.AddAsset("Skill", "Type", "ID0", 0, 0, 0);
			IList<AssetRecomendation>  assets = dataManager.GetAssetsFor("Skill");
			Assert.AreEqual(0, assets[0].GetTimesUsed());
			Assert.AreEqual(0, assets[0].GetLastAccessed());
			Assert.AreEqual(0, assets[0].GetFirstAccessed());
			
			dataManager.MarkAssetCompleted("ID0");
			Assert.AreEqual(1, assets[0].GetTimesUsed());
			Assert.Greater(assets[0].GetLastAccessed(), 0);
			Assert.Greater(assets[0].GetFirstAccessed(), 0);
			Assert.AreEqual(assets[0].GetLastAccessed(), assets[0].GetFirstAccessed());
			
			dataManager.MarkAssetCompleted("ID0");
			Assert.AreEqual(2, assets[0].GetTimesUsed());
			Assert.GreaterOrEqual(assets[0].GetLastAccessed(), assets[0].GetFirstAccessed());
		}
		
		[Test, DescriptionAttribute("Test getting the asset types. Assertions verify the asset types.")]
		public void TestGetAssetsTypes () {
			AssetsDataManager dataManager = new AssetsDataManager();
			dataManager.AddAsset("Skill", "Type", "ID0", 0, 0, 0);
			dataManager.AddAsset("Skill", "Type2", "ID1", 0, 0, 0);
			Assert.AreEqual(2, dataManager.GetAssetsTypes().Count);
			Assert.AreEqual("Type", dataManager.GetAssetsTypes()[0]);
			Assert.AreEqual("Type2", dataManager.GetAssetsTypes()[1]);
		}

		[Test, DescriptionAttribute("Test calling the state storage code. Assertions verify the number of data items returned. ")]
		public void TestStoreData () {
			AssetsDataManager dataManager = new AssetsDataManager();
			dataManager.AddAsset("Skill", "Type", "ID0", 0, 0, 0);
			dataManager.AddAsset("Skill", "Type2", "ID1", 0, 0, 0);
			MockDataStorageDelegate storage = new MockDataStorageDelegate();
			dataManager.StoreData(storage);
			Assert.AreEqual(2, storage.callCount);
		}
	}
	
	internal class MockDataStorageDelegate : DataStorageDelegate {
		
		public int callCount = 0;
		
		public void UpdateProblemData(string problemId, int timesCorrect, int timesWrong, long lastAttempted){}
		public void UpdateSkillClearanceState(string skillId, bool isCompleted){}
		public void UpdateAssetData(string assetId, int timesAccessed, long firstAccessed, long lastAccessed) {
			callCount += 1;
		}

	}
}

