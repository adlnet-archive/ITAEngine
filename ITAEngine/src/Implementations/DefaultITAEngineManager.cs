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

using System;

namespace ITAEngine.Inner
{
	public class DefaultITAEngineManager : ITAEngineManager
	{
		
		private SkillsDataManager skillsDataManager = new SkillsDataManager();
		private ProblemDataManager problemDataManager = new ProblemDataManager();
		private AssetsDataManager assetsDataManager = new AssetsDataManager();
		private ProblemLinkageManager problemLinkageManager;
		private GroupLinkageManager groupLinkageManager;
		
		public DefaultITAEngineManager ()
		{
			problemLinkageManager = new ProblemLinkageManager(assetsDataManager);
			groupLinkageManager = new GroupLinkageManager(assetsDataManager, problemDataManager);
		}

		public void AddSkill(string skillId, bool isComplete){
			skillsDataManager.AddSkill(skillId, isComplete);
		}
			
		public void AssociateAssetWithSkill(string skillId, string assetType, string assetIdentifier, int timesAccessed, long firstAccessed, long lastAccessed){
			assetsDataManager.AddAsset(skillId, assetType, assetIdentifier, timesAccessed, firstAccessed, lastAccessed);
		}
			
		public void AssociateAssessmentProblemWithSkill(string skillId, string problemGroupIdentifier, 
			      string problemUniqueIdentifier, int timesAttempted, int timesCorrect, long lastAttempted){
			problemDataManager.AddProblem(skillId, problemGroupIdentifier, problemUniqueIdentifier, timesAttempted, timesCorrect, lastAttempted);
		}

		public void AssociateAssessmentProblemWithSkill(string skillId, string problemGroupIdentifier, 
		                                                string problemUniqueIdentifier, int timesAttempted, 
		                                                int timesCorrect, long lastAttempted, long timeThreshold){
			problemDataManager.AddProblem(skillId, problemGroupIdentifier, problemUniqueIdentifier, timesAttempted, 
			                              timesCorrect, lastAttempted, timeThreshold);
		}

		public AssessmentBuilder CreateAssessmentBuilder(){
			return new DefaultAssessmentBuilder(problemDataManager, skillsDataManager);
		}
			
		public AssetRecommendationEngine CreateAssetRecommendationEngine(){
			return new DefaultAssetRecommendationEngine(assetsDataManager, skillsDataManager, 
			                                            groupLinkageManager, problemLinkageManager);
		}
		
		public void AssociateProblemWithAssets(string problemId, string [] assets)
		{
			problemLinkageManager.AddProblemLinkage(problemId, assets);
		}
		
		public void AssociateProblemGroupWithAssets(string groupId, string [] assets)
		{
			groupLinkageManager.addAssets(groupId, assets);
		}

		public void StoreData(DataStorageDelegate storeDelegate) {
			skillsDataManager.StoreData(storeDelegate);
			problemDataManager.StoreData(storeDelegate);
			assetsDataManager.StoreData(storeDelegate);
		}
		
	}
}

