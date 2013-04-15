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
	/**
	 * 
	 **/
	public class ProblemDataManager
	{
		private List<ProblemRecord> records = new List<ProblemRecord>();
		private StopWatchManager swMgr = new StopWatchManager();

		public void AddProblem(string skillId, string problemGroupIdentifier, 
			      string problemUniqueIdentifier, int timesAttempted, int timesCorrect, long lastAttempted) {
			records.Add(new ProblemRecord(skillId, problemGroupIdentifier, problemUniqueIdentifier, 
						timesAttempted, timesCorrect, lastAttempted));
		}

		public void AddProblem(string skillId, string problemGroupIdentifier, 
		                       string problemUniqueIdentifier, int timesAttempted, int timesCorrect, 
		                       long lastAttempted, long timeThreshold) {
			records.Add(new ProblemRecord(skillId, problemGroupIdentifier, problemUniqueIdentifier, 
			                              timesAttempted, timesCorrect, lastAttempted, timeThreshold));
		}

		public int GetProblemCount() {
			return records.Count;	
		}

		public string GetGroupIdFor (string problemId)
		{
			string groupId = null; 
			foreach (ProblemRecord rec in records) {
				if(problemId.Equals(rec.problemUniqueIdentifier)) {
					groupId = rec.problemGroupIdentifier;
				}
			}
			return groupId;
		}

		//All problems from group then give the n that have been used least
		public IList<string> GetProblemsFromGroup(int problemCount, string problemGroupId) {
			List<ProblemRecord> problems = getGroupProblem(problemGroupId);
			problems.Sort(new ProblemRecordComparer());
			
			IList<string> list = new List<string>();
			foreach(ProblemRecord rec in problems.GetRange(0, problemCount)) {
				list.Add(rec.problemUniqueIdentifier);
			}
			return list;
		}
		
		public string GetSkillForGroupId(string problemGroupId) {
			foreach(ProblemRecord rec in records) {
				if(problemGroupId.Equals(rec.problemGroupIdentifier)) {
					return rec.skillId;
				}
			}
			return null;
		}

		public bool CheckBelowTimingThreshold (string problemId)
		{
			foreach (ProblemRecord rec in records) {
				if(problemId.Equals(rec.problemUniqueIdentifier)) {
					return rec.timeElapsed < rec.timeThreshold;
				}
			}
			return false;
		}

		public void MarkStarted (string problemId)
		{
			foreach (ProblemRecord rec in records) {
				if(problemId.Equals(rec.problemUniqueIdentifier)) {
					swMgr.NewStopWatch(rec.problemUniqueIdentifier);
				}
			}
		}

		public void MarkCompleted(string problemId, bool isCorrect) {
			foreach(ProblemRecord rec in records) {
				if(problemId.Equals(rec.problemUniqueIdentifier)) {
					rec.timeElapsed = swMgr.GetElapsed(rec.problemUniqueIdentifier);
					rec.lastAttempted = GetCurrentMilli();
					rec.timesAttempted += 1;
					if(isCorrect) {
						rec.timesCorrect += 1;	
					}
				}
			}
		}
		
		public void StoreData(DataStorageDelegate storeDelegate) {
			foreach(ProblemRecord rec in records) {
				storeDelegate.UpdateProblemData(rec.problemUniqueIdentifier, 
					rec.timesCorrect, rec.timesAttempted, rec.lastAttempted);	
			}
		}
		
		private List<ProblemRecord> getGroupProblem(string groupId) {
			List<ProblemRecord> list = new List<ProblemRecord>();
			foreach(ProblemRecord rec in records) {
				if(rec.problemGroupIdentifier.Equals(groupId)) {
					list.Add(rec);
				}
			}
			return list;	
		}
		
		private static long GetCurrentMilli() {
        	DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0,0,DateTimeKind.Utc);
        	TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
        	return (long) javaSpan.TotalMilliseconds;
    	}
	
		internal class ProblemRecord {
			
			internal string skillId; 
			internal string problemGroupIdentifier;
		    internal string problemUniqueIdentifier; 
			internal int timesAttempted;
			internal int timesCorrect;
			internal long lastAttempted;
			internal long timeThreshold;
			internal long timeElapsed = 0; 
			
			public ProblemRecord(string skillId, string problemGroupIdentifier, 
				      string problemUniqueIdentifier, int timesAttempted, int timesCorrect, long lastAttempted) {
				this.skillId = skillId;
				this.problemGroupIdentifier = problemGroupIdentifier;
				this.problemUniqueIdentifier = problemUniqueIdentifier;
				this.timesAttempted = timesAttempted;
				this.timesCorrect = timesCorrect;
				this.lastAttempted = lastAttempted;
			}

			public ProblemRecord(string skillId, string problemGroupIdentifier, 
			                     string problemUniqueIdentifier, int timesAttempted, int timesCorrect, 
			                     long lastAttempted, long timeThreshold) {
				this.skillId = skillId;
				this.problemGroupIdentifier = problemGroupIdentifier;
				this.problemUniqueIdentifier = problemUniqueIdentifier;
				this.timesAttempted = timesAttempted;
				this.timesCorrect = timesCorrect;
				this.lastAttempted = lastAttempted;
				this.timeThreshold = timeThreshold;
			}

						
		}
		
		internal class ProblemRecordComparer : Comparer<ProblemRecord> {					
			public override int Compare (ProblemRecord x, ProblemRecord y) {
				if(x.timesAttempted == y.timesAttempted) {
					return (int)(x.lastAttempted - y.lastAttempted);	
				}
				return x.timesAttempted - y.timesAttempted;	
			}	
		}
		
	}
	
}

