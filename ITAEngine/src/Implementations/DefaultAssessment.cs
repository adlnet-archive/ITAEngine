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
using ITAEngine.Inner;

namespace ITAEngine.Inner
{
	public class DefaultAssessment : Assessment
	{
		
		private const float PASSING_THRESHOLD = 0.8F;
		
		private ProblemDataManager problemDataManager;
		private SkillsDataManager skillDataManager;
		
		private IList<string> skills;
		private IList<string> problems;
		private string challengeProblem;
		private bool belowTimingThreshold = false; 

		private Grader aggregateGrader;

		private Dictionary<string, Grader> dictionary = new Dictionary<string, Grader>();
		
		public DefaultAssessment (ProblemDataManager problemDataManager, SkillsDataManager skillDataManager, 
			IList<string> skills, IList<string> problems, string challengeProblem) {
			
			if(skills.Count != problems.Count) {
				throw new Exception("Skills Count should Equal Problem Count:  Invalid Relationships");	
			}
			
			aggregateGrader = new Grader(problems.Count);
			
			foreach(string skill in skills) {
				if (!dictionary.ContainsKey(skill)) {	
					dictionary.Add(skill, new Grader(countOccurances(skill, skills)));
				}
			}
			
			this.problemDataManager = problemDataManager;
			this.skillDataManager = skillDataManager;
			
			this.skills = skills;
			this.problems = problems;
			this.challengeProblem = challengeProblem;
		}
		
		public IList<string> GetSkills() {
			return new List<string>(skills[0].Split(','));
		}
		
		public IList<string> GetProblemSequence() {
			return problems;
		}
		
		public string GetChallengeProblem() {
			return challengeProblem;
		}
		
		public void MarkProblemStarted(string problemId) {
			//NO-OP	
		}
		
		public void MarkCompleted (string problemId, bool isCorrect, AssessmentActionResponse callback)
		{
			string skills = getSkillForProblem (problemId);
			Grader grader = dictionary [skills];
			grader.increment (isCorrect);
			aggregateGrader.increment (isCorrect);
			problemDataManager.MarkCompleted (problemId, isCorrect);
			if (!belowTimingThreshold && !isCorrect) {
				belowTimingThreshold = problemDataManager.CheckBelowTimingThreshold(problemId);
			}
			if(grader.getTheoreticalWorstScore() >= PASSING_THRESHOLD && !grader.IsCompleted()) {
				foreach (string skill in skills.Split (','))
					skillDataManager.SetSkillComplete(skill, true);	
				grader.setCompleted(true);
			} else if(grader.getTheoreticalBestScore() < PASSING_THRESHOLD) {
				callback.TerminateAssessment(skills, belowTimingThreshold);
			}
			
		}
		
		public void Close(AssessmentCompletionResponse callback) {			
			IList<SkillScoreStructure> skillScores = new List<SkillScoreStructure>();
			foreach (KeyValuePair<string, Grader> pair in dictionary) {
				skillScores.Add(new DefaultSkillScoreStructure(pair.Key, pair.Value.getTheoreticalWorstScore()));
			}
			callback.OnCompletion(aggregateGrader.getTheoreticalWorstScore(), skillScores);
		}
		
		private string getSkillForProblem(string problemId) {
			for(int i = 0; i < problems.Count; i++) {
				if(problemId.Equals(problems[i])) {
					return skills[i];
				}
			}
			
			throw new Exception("No Skill Index Found For Problem: " + problemId);
		}
					
		private static int countOccurances(string skill, IList<string> skills) {
			int count = 0;
			foreach(string s in skills) {		
				if(s.Equals(skill)) {
					count += 1;	
				}
			}
			return count;
		}
		
	}
}

