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
	[TestFixture, DescriptionAttribute("SkillsDataManger Tests") ]
	public class SkillsDataManagerTest
	{
		[Test, DescriptionAttribute("Add skills and check count. The assertion verifies the skill count.")]
		public void TestAddSkill () {
			SkillsDataManager dataManager = new SkillsDataManager();
			dataManager.AddSkill("Skill1", false);
			dataManager.AddSkill("Skill2", false);
			Assert.AreEqual(2, dataManager.GetSkillCount());
		}
		
		[Test, DescriptionAttribute("Test add skill, change completion status, and verify new status. The assertions verify the skill completion values.")]
		public void TestSetSkillComplete () {
			SkillsDataManager dataManager = new SkillsDataManager();
			dataManager.AddSkill("Skill1", false);
			dataManager.AddSkill("Skill2", false);
			Assert.IsFalse(dataManager.IsSkillComplete("Skill1"));
			Assert.IsFalse(dataManager.IsSkillComplete("Skill2"));
			dataManager.SetSkillComplete("Skill1", true);
			Assert.IsTrue(dataManager.IsSkillComplete("Skill1"));
			Assert.IsFalse(dataManager.IsSkillComplete("Skill2"));
		}
		
		[Test, DescriptionAttribute("Test the vist method works. The assertions count the number of data items.")]
		public void TestVisit () {
			SkillsDataManager dataManager = new SkillsDataManager();
			dataManager.AddSkill("Skill1", false);
			dataManager.AddSkill("Skill2", false);
			
			int callCount = 0;
			dataManager.Visit(delegate(string skill, bool complete){
				callCount += 1;
			});
			
			Assert.AreEqual(2, callCount);
		}
		
		[Test, DescriptionAttribute("Test the ability to retrieve skill state data. The assertions count the data returned")]
		public void TestStoreData () {
			SkillsDataManager dataManager = new SkillsDataManager();
			dataManager.AddSkill("Skill1", false);
			dataManager.AddSkill("Skill2", false);
				
			SkillsMockDataStorageDelegate storage = new SkillsMockDataStorageDelegate();
			dataManager.StoreData(storage);
			Assert.AreEqual(2, storage.callCount);
		}
	
	}
	
	internal class SkillsMockDataStorageDelegate : DataStorageDelegate {
		
		public int callCount = 0;
		
		public void UpdateProblemData(string problemId, int timesCorrect, int timesWrong, long lastAttempted){}
		public void UpdateAssetData(string assetId, int timesAccessed, long firstAccessed, long lastAccessed) {}
		public void UpdateSkillClearanceState(string skillId, bool isCompleted){
			callCount += 1;
		}
	}
}

