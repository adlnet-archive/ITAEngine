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
	/*
	 * This class contains the linkage between problem groups and instructional assets.
	 * 
	 *  groupIds and assets are unique identifiers to point to specific items.  
	 * */
	
	public class GroupLinkageManager
	{
		private IDictionary<string, GroupLinkageRecord> groupLinks;
		private AssetsDataManager adm;
		private ProblemDataManager pdm;
		/*
		 * Create the basic class.  
		 **/
		
		public GroupLinkageManager(AssetsDataManager adm, ProblemDataManager pdm)
		{
			groupLinks = new Dictionary<string, GroupLinkageRecord>();
			this.adm = adm;
			this.pdm = pdm;
		}
		
		/*
		 * Add a groupId without adding assets.
		 **/
		public void addGroupId(string groupId) 
		{
			if (!groupLinks.ContainsKey(groupId))
				groupLinks.Add(groupId, new GroupLinkageRecord(groupId));
		}
		
		/*
		 * Add assets to the specified groupId
		 * */
		public void addAssets(string groupId, IEnumerable<string> assets)
		{
			if (!groupLinks.ContainsKey(groupId)) {
				groupLinks.Add(groupId, new GroupLinkageRecord(groupId, assets));
			} else {
				groupLinks[groupId].addAssets(assets);
			}
		}

		/*
		 * Returns a readonly list of assests for the groupId or null.
		 * 
		 **/
		public IList<string> getAssets(string groupId)
		{
			if (!groupLinks.ContainsKey(groupId))
				return null;
			return groupLinks[groupId].assets.AsReadOnly();
		}

		public List<string> GetAssetsSortedByTimesAccessd (string problemId)
		{
			List<AssetTuple> list = new List<AssetTuple> ();
			string groupId = pdm.GetGroupIdFor(problemId);
			foreach (string assetid in groupLinks[groupId].assets) {
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

	
		internal class GroupLinkageRecord 
		{
			internal string groupId;
			internal List<string> assets;

			/* 
			 * Create a group linkage record with empty asset list.
			 * */
			public GroupLinkageRecord(string groupId) {
				this.groupId = groupId;
				assets = new List<string>();
			}
			
			/*
			 * Create linkage record with assets by passing array of assets.
			 **/
			public GroupLinkageRecord(string groupId, IEnumerable<string> assetList) {
				this.groupId = groupId;
				assets = new List<string>(assetList);
			}
			
			/*
			 * Add asset ids to the list of assets
			 **/
			public void addAssets(IEnumerable<string> asset) { 
				if (assets.Count == 0) { 
					assets.AddRange(asset);
				} else { 
					foreach(string assetid in asset) {
						if (!assets.Contains(assetid))
							assets.Add(assetid);
					}		
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

