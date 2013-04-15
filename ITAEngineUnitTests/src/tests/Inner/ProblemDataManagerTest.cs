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
	[TestFixture, DescriptionAttribute("ProblemDataManager Tests")]
	public class ProblemDataManagerTest
	{
		[Test, DescriptionAttribute("Test adding a single problem. The assertion verifies the count.")]
		public void TestAddProblem () {
			ProblemDataManager dataManager = new ProblemDataManager();
			dataManager.AddProblem("Skill1", "Group1", "Problem1", 0, 0, 0);
			Assert.AreEqual(1, dataManager.GetProblemCount());
		}
		
		[Test, DescriptionAttribute("Test retrieving problems from a problem group. Assertions test that the problems retrieved belong to the group specified.")]
		public void TestGetProblemsFromGroup () {
			ProblemDataManager dataManager = new ProblemDataManager();
			dataManager.AddProblem("Skill1", "Group1", "Problem1", 1, 0, 1);
			dataManager.AddProblem("Skill1", "Group2", "Problem2", 0, 0, 0);
			dataManager.AddProblem("Skill1", "Group1", "Problem3", 1, 0, 2);
			Assert.AreEqual(3, dataManager.GetProblemCount());
			
			Assert.AreEqual(1, dataManager.GetProblemsFromGroup(1, "Group1").Count);
			Assert.AreEqual(2, dataManager.GetProblemsFromGroup(2, "Group1").Count);
			
			Assert.AreEqual("Problem1", dataManager.GetProblemsFromGroup(2, "Group1")[0]);
			Assert.AreEqual("Problem3", dataManager.GetProblemsFromGroup(2, "Group1")[1]);
			
			dataManager.MarkCompleted("Problem1", true);
			
			Assert.AreEqual(2, dataManager.GetProblemsFromGroup(2, "Group1").Count);
			Assert.AreEqual("Problem3", dataManager.GetProblemsFromGroup(2, "Group1")[0]);
			Assert.AreEqual("Problem1", dataManager.GetProblemsFromGroup(2, "Group1")[1]);
		}
		
		[Test, DescriptionAttribute("Test retrieving skills for a group id. The assertion verifies that the skill belongs to the group.")]
		public void TestGetSkillForGroupId () {
			ProblemDataManager dataManager = new ProblemDataManager();
			dataManager.AddProblem("Skill1", "Group1", "Problem1", 0, 0, 0);
			Assert.AreEqual("Skill1", dataManager.GetSkillForGroupId("Group1"));
		}
		
		[Test, DescriptionAttribute("Test marking a problem complete. The assertions verify the data after marking problem complete.  ")]
		public void TestMarkComplete () {
			ProblemDataManager dataManager = new ProblemDataManager();
			dataManager.AddProblem("Skill1", "Group1", "Problem1", 0, 0, 0);
			dataManager.MarkCompleted("Problem1", true);
			dataManager.MarkCompleted("Problem1", false);
			ProblemsMockDataStorageDelegate storage = new ProblemsMockDataStorageDelegate();
			dataManager.StoreData(storage);
			
			Assert.AreEqual(1, storage.callCount);
			Assert.AreEqual(2, storage.timesAttempt);
			Assert.AreEqual(1, storage.correct);
		}
		
		internal class ProblemsMockDataStorageDelegate : DataStorageDelegate {
		
			public int callCount = 0;
			public int correct;
			public int timesAttempt;
			
			public void UpdateProblemData(string problemId, int timesCorrect, int timesAttempted, long lastAttempted){
				callCount += 1;
				correct = timesCorrect;
				timesAttempt = timesAttempted;				
			}
			public void UpdateAssetData(string assetId, int timesAccessed, long firstAccessed, long lastAccessed) {}
			public void UpdateSkillClearanceState(string skillId, bool isCompleted){}
		}
	}
}

