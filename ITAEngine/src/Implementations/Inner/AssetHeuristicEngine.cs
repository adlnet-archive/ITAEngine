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
	public class AssetHeuristicEngine : IAssetHeuristicEngine
	{
		
		private List<TypeHeuristic> weightedTypeList = new List<TypeHeuristic>();
		
		public AssetHeuristicEngine(ICollection<string> assetTypes, IList<AssetRecomendation> pastAssets) {
			 generateAssetScores(assetTypes, pastAssets);	
		}
		
		public void generateAssetScores(ICollection<string> assetTypes, IList<AssetRecomendation> pastAssets) {
			Dictionary<string, long> typeScoresMap = new Dictionary<string, long>();
			foreach(string a in assetTypes) {
				typeScoresMap.Add(a, int.MaxValue);	
			}
			
			foreach(AssetRecomendation asset in pastAssets) {
				typeScoresMap[asset.GetAssetType()] = Math.Min(typeScoresMap[asset.GetAssetType()], asset.GetFirstAccessed());
			}
			
			foreach (KeyValuePair<string, long> pair in typeScoresMap) {
				weightedTypeList.Add(new TypeHeuristic(pair.Key, pair.Value));
			}
			
			weightedTypeList.Sort(new TypeHeuristicComparer());
		}
		
		public int getScoreForType(string assetType) {
			for(int i = 0; i < weightedTypeList.Count; i++) {
				TypeHeuristic obj = weightedTypeList[i];
				if(obj.type.Equals(assetType)) {
					return i;	
				}
			}
			return 0;
		}
		
		internal class TypeHeuristic {
			public string type;
			public long firstAccessTime;
			
			public TypeHeuristic(string type, long firstAccessTime) {
				this.type = type;
				this.firstAccessTime = firstAccessTime;
			}
		}
		
		internal class TypeHeuristicComparer : Comparer<TypeHeuristic> {					
			public override int Compare (TypeHeuristic x, TypeHeuristic y) {
				return (int)(y.firstAccessTime - x.firstAccessTime);	
			}	
		}
	}
}

