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
	public class AssetsDataManager
	{
		
		private HashSet<string> types = new HashSet<string>();
		private List<AssetRecord> records = new List<AssetRecord>();

		public void AddAsset(string skillId, string assetType, string assetIdentifier, int timesAccessed, long firstAccessed, long lastAccessed) {
			types.Add(assetType);
			records.Add(new AssetRecord(skillId, assetType, assetIdentifier, timesAccessed, firstAccessed, lastAccessed));
		}
		
		public int GetAssetCount() {
			return records.Count;	
		}
		
		public void MarkAssetStarted(string assetIdentifier) {
			//NO-OP
		}
		
		public void MarkAssetCompleted(string assetIdentifier) {
			AssetRecord record = findRecordById(assetIdentifier);
			long currentTime = GetCurrentMilli();
			if(record.GetFirstAccessed() <= 0) {
				record.SetFirstAccessed(currentTime);
			}
			
			record.SetTimesUsed(record.GetTimesUsed() + 1);
			record.SetLastAccessed(currentTime);
		}
		
		public IList<string> GetAssetsTypes() {
			return new List<string>(types);
		}
		
		public IList<AssetRecomendation> GetAssetsFor(string skillIndex) {
			IList<AssetRecomendation> assets = new List<AssetRecomendation>();
			foreach(AssetRecord rec in records) {
				if(rec.GetSkillId().Equals(skillIndex)) {
					assets.Add(rec);	
				}
			}
			return assets;
		}

		public AssetRecomendation GetAssetRecFor (string assetId)
		{
			return findRecordById(assetId);
		}

		public int GetAssetTimesAccessedFor (string assetId)
		{
			int timesAccessed = 0;
			AssetRecord rec = findRecordById(assetId);
			if (rec != null)
				timesAccessed = rec.GetTimesUsed();
			return timesAccessed; 
		}

		public void StoreData(DataStorageDelegate storeDelegate) {
			foreach(AssetRecord rec in records) {
				storeDelegate.UpdateAssetData(rec.GetAssetIdentifier(), rec.GetTimesUsed(), 
					rec.GetFirstAccessed(), rec.GetLastAccessed());	
			}
		}
		
		private AssetRecord findRecordById(string assetId) {
			foreach(AssetRecord rec in records) {
				if(rec.GetAssetIdentifier().Equals(assetId)) {
					return rec;	
				}
			}
			
			return null;
		}
		
		public static long GetCurrentMilli() {
        	DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0,0,DateTimeKind.Utc);
        	TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
        	return (long) javaSpan.TotalMilliseconds;
    	}
		
		internal class AssetRecord : AssetRecomendation {
			private string skillId;
			private string assetType;
			private string assetIdentifier;
			private int timesAccessed;
			private long firstAccessed;
			private long lastAccessed;
			
			public AssetRecord(string skillId, string assetType, string assetIdentifier, 
						int timesAccessed, long firstAccessed, long lastAccessed) {
				this.skillId = skillId;
				this.assetType = assetType;
				this.assetIdentifier = assetIdentifier;
				this.timesAccessed = timesAccessed;
				this.firstAccessed = firstAccessed;
				this.lastAccessed = lastAccessed;
			}
			
			public string GetSkillId() { return skillId; }
			public string GetAssetIdentifier() { return assetIdentifier; }
			public string GetAssetType() { return assetType; }
			public int GetTimesUsed() { return timesAccessed; }
			public long GetFirstAccessed() { return firstAccessed; }
			public long GetLastAccessed() { return lastAccessed; }
			
			public void SetFirstAccessed(long time) {
				this.firstAccessed = time;	
			}
			
			public void SetLastAccessed(long time) {
				this.lastAccessed = time;	
			}
			
			public void SetTimesUsed(int t) {
				timesAccessed = t;
			}
		}
		
	}
	
}

