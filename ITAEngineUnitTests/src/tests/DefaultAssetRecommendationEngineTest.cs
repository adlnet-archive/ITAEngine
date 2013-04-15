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
using NUnit.Framework;
using ITAEngine.Inner;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("DefaultAssetRecommendationEngine Tests")]
	public class DefaultAssetRecommendationEngineTest
	{
		[Test, DescriptionAttribute("Test basic construction. Asserts verify the object was constructed.")]
		public void BasicTest()
		{
			AssetsDataManager adm = new AssetsDataManager();
			ProblemDataManager pdm = new ProblemDataManager();
			SkillsDataManager sdm = new SkillsDataManager();
			GroupLinkageManager glm = new GroupLinkageManager(adm, pdm);
			ProblemLinkageManager plm = new ProblemLinkageManager(adm);
			DefaultAssetRecommendationEngine dare = new DefaultAssetRecommendationEngine(adm, sdm, glm, plm);
			Assert.IsNotNull(dare);
		}
	
		AssetsDataManager adm;
		ProblemDataManager pdm;
		SkillsDataManager sdm;
		GroupLinkageManager glm;
		ProblemLinkageManager plm ;
		string skillIdPrefix = "skillid-";
		string assetTypePrefix = "type-"; 
		string assetIdPrefix = "asset-id-";

		private void createAssetsSkills(int skillCount, int assetTypesCount, int assetCount, out AssetsDataManager adm, out SkillsDataManager sdm) 
		{
			adm = new AssetsDataManager();
			pdm = new ProblemDataManager();
			sdm = new SkillsDataManager();
			glm = new GroupLinkageManager(adm, pdm);
			plm = new ProblemLinkageManager(adm);

			for (int skillId = 0; skillId < skillCount; skillId++) {
				sdm.AddSkill (skillIdPrefix + skillId.ToString(), false);
				for (int assetTypeId = 0; assetTypeId < assetTypesCount; assetTypeId++) {
					for (int assetId = 0; assetId < assetCount; assetId++) {
						adm.AddAsset(skillIdPrefix + skillId, assetTypePrefix + assetTypeId, assetIdPrefix + assetId, 0, 0, 0);
					}
				}
			}
		}
		
		[Test, DescriptionAttribute("Test the basic recommendations. Asserts verify the assets recommended.")]
		public void BasicRecommendationTest() 
		{
			int skillCount = 3; 
			int assetTypeCount = 4;
			int assetCount = 5; 
			
			createAssetsSkills(skillCount, assetTypeCount, assetCount, out adm, out sdm);

			DefaultAssetRecommendationEngine dam = new DefaultAssetRecommendationEngine(adm, sdm, glm, plm);
			
			IList<AssetRecomendation> ar = dam.GetRecommendationsFor(skillIdPrefix + 1);
			Assert.IsNotNull(ar);
			Assert.AreEqual(assetTypeCount*assetCount, ar.Count);
			
			string assetId = ar[0].GetAssetIdentifier();

			for (int i = 0; i < assetTypeCount * assetCount; i++)
			{
				adm.MarkAssetCompleted(ar[i].GetAssetIdentifier());
			}
			
			dam = new DefaultAssetRecommendationEngine(adm, sdm, glm, plm);
			ar = dam.GetRecommendationsFor(skillIdPrefix + 1);
			Assert.AreEqual(assetId, ar[0].GetAssetIdentifier());
		}
		

		public void TestMock()
		{
			
		}
		
		internal class MockAssetHeuristicEngine : IAssetHeuristicEngine
		{
			
			
			public void generateAssetScores(ICollection<string> assetTypes, IList<AssetRecomendation> pastAssets)
			{
				
			}
			
			public int getScoreForType(string AssetType)
			{
				return 0;
			}
		}
		
	}
}
