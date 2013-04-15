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

namespace ITAEngine.Inner
{
	public class ProblemLinkageManager
	{
		Dictionary<string, ProblemLinkageRecord> plm; 
		AssetsDataManager adm; 

		public ProblemLinkageManager(AssetsDataManager adm)
		{
			plm = new Dictionary<string, ProblemLinkageRecord>();
			this.adm = adm; 
		}
		
		public void AddProblemLinkage(string problemId, string [] assets)
		{
			if (!plm.ContainsKey(problemId))
				plm.Add(problemId, new ProblemLinkageRecord(problemId, assets));
			else
				plm[problemId].AddAssets(assets);
		}

		public void AddProblemLinkage(string problemId, string asset)
		{
			if (!plm.ContainsKey(problemId))
				plm.Add(problemId, new ProblemLinkageRecord(problemId, asset));
			else
				plm[problemId].AddAsset(asset);
		}

		public string [] GetAssets(string problemId)
		{
			if(!plm.ContainsKey(problemId))
				return null;
			else 
				return plm[problemId].assets.ToArray();
		}

		public List<string> GetAssetsSortedByTimesAccessd (string problemId)
		{
			List<AssetTuple> list = new List<AssetTuple> ();

			foreach (string assetid in plm[problemId].assets) {
				list.Add (new AssetTuple (assetid, adm.GetAssetTimesAccessedFor (assetid)));
			}

			list.Sort (TimeComparer);
			List<string> assetList = new List<string> ();
			foreach (AssetTuple atuple in list) {
				assetList.Add (atuple.id);
			}
			return assetList; 
		}

		private static int TimeComparer (AssetTuple x, AssetTuple y)
		{
			return x.timesAccess - y.timesAccess;
		}

		internal class ProblemLinkageRecord {
			internal string problemId;
			internal List<string> assets;
			
			public ProblemLinkageRecord (string problemId, string [] assets) 
			{
				this.problemId = problemId;
				this.assets = new List<string>(assets);
			}
			
			public ProblemLinkageRecord (string problemId, string asset) 
			{
				this.problemId = problemId;
				this.assets = new List<string>();
				assets.Add (asset);
				
			}

			public void AddAsset(string asset)
			{
				assets.Add(asset);
			}
			
			public void AddAssets(string [] assets)
			{
				foreach(string id in assets)
				{
					this.assets.Add(id);
				}
			}
		}

		internal class AssetTuple
		{
			internal string id; 
			internal int timesAccess = 0;
		 	public AssetTuple(string id, int times)
			{
				this.id = id;
				this.timesAccess = times;
			}
		}
	}
}

