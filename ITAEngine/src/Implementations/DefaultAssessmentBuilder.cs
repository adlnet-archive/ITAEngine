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
	public class DefaultAssessmentBuilder : AssessmentBuilder
	{
		
		private ProblemDataManager problemDataManager;
		private SkillsDataManager skillsDataManager;

		private List<ProblemTuple> problems = new List<ProblemTuple>();
		
		private string challengeProblemId = null;

		
		public DefaultAssessmentBuilder (ProblemDataManager problemDataManager, SkillsDataManager skillsDataManager)	{
			this.problemDataManager = problemDataManager;
			this.skillsDataManager = skillsDataManager;
		}

		public AssessmentBuilder AddProblem(int problemCount, string problemGroupId, string randomizationGroup) {
			string skill = problemDataManager.GetSkillForGroupId(problemGroupId);
			if(!skillsDataManager.IsSkillComplete(skill)) {
				IList<string> probs =  problemDataManager.GetProblemsFromGroup(problemCount, problemGroupId);
				foreach(string pid in probs) {
					problems.Add(new ProblemTuple(skill, pid, randomizationGroup));
				}
			}
			return this;
		}

		public AssessmentBuilder SetChallenge(string challengeProblemId){
			this.challengeProblemId = challengeProblemId;
			return this;
		}
	
		public Assessment Build(){
			randomizeProblems();
			IList<string> skillsArray = new List<string>();
			IList<string> problemArray = new List<string>();
			foreach(ProblemTuple pt in problems) {
				skillsArray.Add(pt.GetSkill());
				problemArray.Add(pt.GetProblem());
			}
			return new DefaultAssessment(problemDataManager, skillsDataManager, skillsArray, problemArray, challengeProblemId);
		}
		
		private void randomizeProblems() {
			Random rng = new Random();
			foreach(ProblemTuple pt in problems) {
				pt.SetSubRandomKey(rng.Next(100));	
			}
			problems.Sort(new ProblemTupleComparer());
		}
	}
	
	internal class ProblemTuple {
		private string skill;
		private string problem;
		private string randomization;
		private int subRandomKey;
		
		public ProblemTuple(string skill, string problem, string randomization) {
			this.skill = skill;
			this.problem = problem;
			this.randomization = randomization;
		}
		
		public string GetSkill() {
			return skill;	
		}
		
		public string GetProblem() {
			return problem;	
		}
		
		public string getRandomizationKey() {
			return randomization + "-" + subRandomKey;	
		}
		
		public void SetSubRandomKey(int key) {
			subRandomKey = key;	
		}
	}
	
	internal class ProblemTupleComparer : Comparer<ProblemTuple> {					
		public override int Compare (ProblemTuple x, ProblemTuple y) {
			return x.getRandomizationKey().CompareTo(y.getRandomizationKey());
		}	
	}
}

