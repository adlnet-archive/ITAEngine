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
	public class SkillsDataManager
	{
		
		public delegate void ProcessSkillDelegate(string skillId, bool isComplete);
		
		private List<SkillRecord> records = new List<SkillRecord>();
		
		public void AddSkill(string skillId, bool isComplete){
			records.Add(new SkillRecord(skillId, isComplete));
		}
		
		public int GetSkillCount() {
			return records.Count;	
		}
		
		public bool IsSkillComplete(string skillId) {
			foreach(SkillRecord rec in records) {
				if(rec.skillId.Equals(skillId)) {
					return rec.isComplete;	
				}
			}
			return false;	
		}
		
		public void SetSkillComplete(string skillId, bool isComplete) {
			foreach(SkillRecord rec in records) {
				if(rec.skillId.Equals(skillId)) {
					rec.isComplete = isComplete;
					return;
				}
			}
		}
		
		public void Visit(ProcessSkillDelegate callback) {
			foreach(SkillRecord rec in records) {
				callback(rec.skillId, rec.isComplete);	
			}
		}
		
		public void StoreData(DataStorageDelegate storeDelegate) {
			foreach(SkillRecord rec in records) {
				storeDelegate.UpdateSkillClearanceState(rec.skillId, rec.isComplete);	
			}
		}
	}
	
	internal class SkillRecord {
		
		internal string skillId;
		internal bool isComplete;
		
		public SkillRecord(string skillId, bool isComplete) {
			this.skillId = skillId;
			this.isComplete = isComplete;
		}
				
	}
}

