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

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("DefaultITAEngineManager Tests")]
	public class DefaultITAEngineManagerTest
	{
		[Test, DescriptionAttribute("Test basic construction. Assert verifies object creation ")]
		public void BasicCtorCase ()
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			Assert.IsNotNull(iem);				
		}
		
		[Test, DescriptionAttribute("API exercise adds a skill.")]
		public void TestAddSkill()
		{
			/* this test is useless except as a check that the api doesn't change */
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			iem.AddSkill("1", false);
		}

		[Test, DescriptionAttribute("API exercise associates an asset with a skill.")]
		public void TestAssociateAssetWithSkill()
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			iem.AssociateAssetWithSkill("1", "AssetType", "id_1", 1, 0xDEADBEEF, 0x0000BEEF);
		}
		
		[Test, DescriptionAttribute("API exercise associate an assessment with a skill.")]
		public void TestAssociateAssessmentProblemWithSkill() 
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			iem.AssociateAssessmentProblemWithSkill("1", "groupID", "unique_id", 1, 0, 0xDEADBEEF);
		}
		
		[Test, DescriptionAttribute("API exercise associates a problem with an assessment.")]
		public void TestAssicateProblemWithAsset()
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			iem.AssociateProblemWithAssets("id", new string [] {"asset1", "asset2"});
		}
		
		[Test, DescriptionAttribute("Test the creation of an AssessmentBuilder. Assert verifies the Assessment isn't null.")]
		public void TestCreateAssessmentBuilder() 
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			AssessmentBuilder assessBuild = iem.CreateAssessmentBuilder();
			Assert.IsNotNull(assessBuild);
		}
		
		[Test, DescriptionAttribute("Test the creation of an AssetRecommendationEngine. Asser verfieis the RecommendationEngine is not null")]
		public void TestCreateRecommendationEngine() 
		{
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			AssetRecommendationEngine are = iem.CreateAssetRecommendationEngine();
			Assert.IsNotNull(are);
		}
		
		[Test, DescriptionAttribute("Test the state saving data calls. Assert verifies the StoreData calls work.")]
		public void TestStoreData()
		{
			MockDataStorageDelegate mdsd = new MockDataStorageDelegate(); 
			DefaultITAEngineManager iem = new DefaultITAEngineManager();
			iem.AssociateAssessmentProblemWithSkill("1", "groupID", "1", 0, 1, 0xDEADBEEF);
			iem.StoreData(mdsd);
			Assert.IsTrue(mdsd.countProbData.Equals(1));
			Assert.IsTrue(mdsd.countAssetData.Equals(0));
			Assert.IsTrue(mdsd.countSkillClear.Equals(0));
		}
		
		internal class MockDataStorageDelegate : DataStorageDelegate 
		{
			public int countProbData = 0;
			public int countAssetData = 0;
			public int countSkillClear = 0;
			
			public void UpdateProblemData(string problemId, int timesCorrect, int timesAttempted, long lastAttempted) 
			{
				countProbData++;
			}
		
			public void UpdateAssetData(string assetId, int timesAccessed, long firstAccessed, long lastAccessed)
			{
				countAssetData++;
			}
		
			public void UpdateSkillClearanceState(string skillId, bool isCompleted)
			{
				countSkillClear++;
			}

		}
	}
}

