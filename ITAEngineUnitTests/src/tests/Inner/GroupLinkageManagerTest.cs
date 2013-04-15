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
using ITAEngine.Inner;
using NUnit.Framework;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("GroupLinkageManager Tests")]
	public class GroupLinkageManagerTest
	{
		[Test, DescriptionAttribute("Test basic construction of the class.")]
		public void BasicCtorTest() {
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			Assert.IsNotNull(glm);
		}
		
		[Test, DescriptionAttribute("Test for null return of empty group linkages.")]
		public void NullGroupId() {
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			Assert.IsNull(glm.getAssets("groupId"));
		}
		
		[Test, DescriptionAttribute("Test the addition of a group. The assertion test the group id matches the value passed." )]
		public void AddGroupIdTest() {
			string groupId = "groupId";
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			glm.addGroupId(groupId);
			Assert.AreEqual(0, glm.getAssets(groupId).Count);
		}
		
		[Test, DescriptionAttribute("Test adding an asset to a group. The assertion verifies the counts.")]
		public void  AddAssetTest() {
			string groupId = "groupId";
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			glm.addAssets (groupId, new string[] {"asset1"});
			Assert.AreEqual(1, glm.getAssets(groupId).Count);
		}
		
		[Test, DescriptionAttribute("Test adding an asset to an existing empty group. The assertion verifies the asset passed.")]
		public void AddAssetsToExistingEmptyList(){
			string groupId = "groupId";
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			glm.addGroupId(groupId);
			glm.addAssets(groupId, new string [] { "assets" });
			Assert.AreEqual("assets", glm.getAssets(groupId)[0]);
		}

		[Test, DescriptionAttribute("Test the addition of duplicate assets. The assertions test for no duplicates.")]
		public void AddDuplicateAssetToExistingList(){
			string groupId = "groupId";
			string [] asset = new string [] {"asset1"};
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			glm.addAssets(groupId, asset);
			Assert.AreEqual(1, glm.getAssets(groupId).Count);
			
			glm.addAssets(groupId, asset);
			Assert.AreEqual(1, glm.getAssets(groupId).Count);
		}

		[Test, DescriptionAttribute("Test adding multiple assets to an existing group. The assertions verify the addtion of multiple assets.")]
		public void AddMultipleAssetsToExistingList(){
			string groupId = "groupId";
			string [] assets = new string [] {"asset1"};
			AssetsDataManager alm = new AssetsDataManager();
			ProblemDataManager plm = new ProblemDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(alm, plm);
			glm.addAssets(groupId, assets);
			Assert.AreEqual(1, glm.getAssets(groupId).Count);
			
			assets = new string[] { "asset2", "asset3", "asset4"};
			glm.addAssets(groupId, assets);
			
			foreach(string asset in assets) {
				Assert.IsTrue(glm.getAssets(groupId).Contains (asset));
			}
			Assert.IsTrue(glm.getAssets(groupId).Contains ("asset1"));
		}
	}
}
