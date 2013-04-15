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
using System.Collections.Generic;

namespace ITAEngine.Inner
{
	public class DefaultAssetRecommendationEngine : AssetRecommendationEngine
	{
		
		private AssetsDataManager assetsDataManager;
		private SkillsDataManager skillsDataManager;
		private GroupLinkageManager groupLinkageManager;
		private ProblemLinkageManager problemLinkageManager;
		private IAssetHeuristicEngine iahe; 

		public DefaultAssetRecommendationEngine (AssetsDataManager assetsDataManager, SkillsDataManager skillsDataManager,
		                                         GroupLinkageManager glm, ProblemLinkageManager plm)	{
			this.assetsDataManager = assetsDataManager;
			this.skillsDataManager = skillsDataManager;
			this.groupLinkageManager = glm; 
			this.problemLinkageManager = plm;
			iahe = null; 
		}
		
		public IList<AssetRecomendation> GetRecommendationsFor(string skillIndex) {
			ICollection<string> assetTypes = assetsDataManager.GetAssetsTypes();
			Dictionary<string, int> typeScoresMap = new Dictionary<string, int>();
			
			foreach(string type in assetTypes) {
				typeScoresMap.Add(type, 0);	
			}
			
			skillsDataManager.Visit(delegate(string skillId, bool isComplete){
				if(isComplete) {
					IAssetHeuristicEngine engine = getAssetHeuristicEngine(assetTypes, assetsDataManager.GetAssetsFor(skillId));
					foreach(string type in assetTypes) {
						typeScoresMap[type] += engine.getScoreForType(type);	
					}
				}
			});
			
			IList<AssetRecomendation> unsortedList = assetsDataManager.GetAssetsFor(skillIndex);
			List<ComparableAssetRecomendation> comparableList = new List<ComparableAssetRecomendation>();
			foreach(AssetRecomendation ar in unsortedList) {
				comparableList.Add(new ComparableAssetRecomendation(ar, typeScoresMap[ar.GetAssetType()]));	
			}
			
			comparableList.Sort(new ComparableAssetRecomendationComparer());
			
			IList<AssetRecomendation> arList = new List<AssetRecomendation>();
			foreach(AssetRecomendation ar in comparableList) 
				arList.Add(ar);
			
			return arList;
		}

		public IList<AssetRecomendation> GetRecommendationsForProblem (string problemId)
		{
			List<AssetRecomendation> arList = null; 

			arList = GetLinkageList(problemLinkageManager.GetAssetsSortedByTimesAccessd (problemId));
			if (arList != null)
				return arList;

			arList = GetLinkageList(groupLinkageManager.GetAssetsSortedByTimesAccessd(problemId));
			return arList; 
		}

		private List<AssetRecomendation> GetLinkageList(List<string>assetList)
		{
			List<AssetRecomendation> arList = null; 

			foreach (string asset in assetList) {
				arList = new List<AssetRecomendation>();
				arList.Add (assetsDataManager.GetAssetRecFor (asset));
			}
			return arList;
		}

		private IAssetHeuristicEngine getAssetHeuristicEngine(ICollection<string> assetTypes, IList<AssetRecomendation> pastAssets) {
			if (iahe == null) {
				iahe = new AssetHeuristicEngine(assetTypes, pastAssets); 
			} else {
				iahe.generateAssetScores(assetTypes, pastAssets);
			}
			return iahe;	
		}

		public void setHeuristicsEngine(IAssetHeuristicEngine iahe) { 
			this.iahe = iahe;	
		}
		
		public void MarkAssetStarted(string assetIdentifier) {
			assetsDataManager.MarkAssetStarted(assetIdentifier);
		}

		public void MarkAssetCompleted(string assetIdentifier) {
			assetsDataManager.MarkAssetCompleted(assetIdentifier);
		}
	}
	
	internal class ComparableAssetRecomendation : AssetRecomendation {
		
		private AssetRecomendation wrapped;
		private int typeScore;
		
		public ComparableAssetRecomendation(AssetRecomendation wrapped, int typeScore) {			
			this.wrapped = wrapped;
			this.typeScore = typeScore;
		}
			
		public string GetAssetIdentifier() { return wrapped.GetAssetIdentifier(); }
		public string GetAssetType() { return wrapped.GetAssetType(); }
		public int GetTimesUsed() { return wrapped.GetTimesUsed(); }
		public long GetFirstAccessed() { return wrapped.GetFirstAccessed(); }
		public long GetLastAccessed() { return wrapped.GetLastAccessed(); }
		public int GetTypeScore() { return typeScore; }

	}
	
	//Sort the recommended assets by type and last access date
	internal class ComparableAssetRecomendationComparer : Comparer<ComparableAssetRecomendation> {					
		public override int Compare (ComparableAssetRecomendation x, ComparableAssetRecomendation y) {
			if(x.GetTypeScore() != y.GetTypeScore()) {
				return x.GetTypeScore() - y.GetTypeScore();
			}
			return (int)(y.GetLastAccessed() - x.GetLastAccessed());	
		}	
	}
}

