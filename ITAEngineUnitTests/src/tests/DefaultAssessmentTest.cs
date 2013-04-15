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
using System.Collections.Generic;
using ITAEngine.Inner;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("DefaultAssessment Tests")]
	public class DefaultAssessmentTest
	{
		[Test, DescriptionAttribute("Test construction. Asserts verify the skills and problem sequence counts.")]
		public void TestConstruct ()
		{
			List<string> mockSkills = new List<string>();
				mockSkills.Add("1,2");
				mockSkills.Add("1,2");
			
			List<string> mockproblems = new List<string>();
				mockproblems.Add("A");
				mockproblems.Add("B");
			
			DefaultAssessment assessment = new DefaultAssessment(new ProblemDataManager(), new SkillsDataManager(), 
						mockSkills, mockproblems, "C");
			Assert.IsNotNull(assessment);
			
			Assert.AreEqual(2, assessment.GetSkills().Count);
			Assert.AreEqual(2, assessment.GetProblemSequence().Count);
		}
		
		[Test, DescriptionAttribute("Test failed assessment. Asserts verify the failed assessment and skills associated with it.")]
		public void TestFailedAssessment ()
		{
			List<string> mockSkills = new List<string>();
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
			
			List<string> mockproblems = new List<string>();
				mockproblems.Add("A");
				mockproblems.Add("B");
				mockproblems.Add("C");
				mockproblems.Add("D");
				mockproblems.Add("E");
			
			DefaultAssessment assessment = new DefaultAssessment(new ProblemDataManager(), new SkillsDataManager(), mockSkills, mockproblems, "G");
			Assert.IsNotNull(assessment);
			
			assessment.MarkProblemStarted("A");
			MockAssessmentActionResponse response = new MockAssessmentActionResponse();
			assessment.MarkCompleted("A", false, response);
			response.reset();
			
			assessment.MarkCompleted("B", false, response);
			Assert.AreEqual("1", response.terminatedSkill);
			response.reset();
			
		}
		
		[Test, DescriptionAttribute("Test two skills with failed assessment. Asserts verify the skills are failed.")]
		public void TestTwoSkillsFailedAssessment ()
		{
			List<string> mockSkills = new List<string>();
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
			
			List<string> mockproblems = new List<string>();
				mockproblems.Add("A");
				mockproblems.Add("B");
				mockproblems.Add("C");
				mockproblems.Add("D");
				mockproblems.Add("E");
				mockproblems.Add("AA");
				mockproblems.Add("BB");
				mockproblems.Add("CC");
				mockproblems.Add("DD");
				mockproblems.Add("EE");
			
			DefaultAssessment assessment = new DefaultAssessment(new ProblemDataManager(), new SkillsDataManager(), mockSkills, mockproblems, "G");
			Assert.IsNotNull(assessment);
			
			assessment.MarkProblemStarted("A");
			MockAssessmentActionResponse response = new MockAssessmentActionResponse();
			assessment.MarkCompleted("A", true, response);
			assessment.MarkCompleted("B", true, response);
			assessment.MarkCompleted("C", true, response);
			assessment.MarkCompleted("D", true, response);
			assessment.MarkCompleted("E", true, response);
			
			assessment.MarkCompleted("AA", true, response);
			assessment.MarkCompleted("BB", false, response);
			assessment.MarkCompleted("CC", false, response);
			Assert.AreEqual("2", response.terminatedSkill);
			
		}
		
		[Test, DescriptionAttribute("Test the closing of an assessment.  Asserts verify the scores.")]
		public void TestCloseAssessment ()
		{
			List<string> mockSkills = new List<string>();
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("1");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
				mockSkills.Add("2");
			
			List<string> mockproblems = new List<string>();
				mockproblems.Add("A");
				mockproblems.Add("B");
				mockproblems.Add("C");
				mockproblems.Add("D");
				mockproblems.Add("E");
				mockproblems.Add("AA");
				mockproblems.Add("BB");
				mockproblems.Add("CC");
				mockproblems.Add("DD");
				mockproblems.Add("EE");;
			
			DefaultAssessment assessment = new DefaultAssessment(new ProblemDataManager(), new SkillsDataManager(), mockSkills, mockproblems, "G");
			Assert.IsNotNull(assessment);
			
			MockAssessmentActionResponse response = new MockAssessmentActionResponse();
			assessment.MarkCompleted("A", true, response);
			assessment.MarkCompleted("B", true, response);
			assessment.MarkCompleted("C", true, response);
			assessment.MarkCompleted("D", true, response);
			assessment.MarkCompleted("E", true, response);
			assessment.MarkCompleted("AA", true, response);
			assessment.MarkCompleted("BB", true, response);
			assessment.MarkCompleted("CC", true, response);
			assessment.MarkCompleted("DD", false, response);
			assessment.MarkCompleted("EE", false, response);
			
			MockAssessmentCompletionResponse mock = new MockAssessmentCompletionResponse();		
			assessment.Close(mock);
			Assert.AreEqual(2, mock.skillScores.Count);
			Assert.AreEqual(1F, mock.skillScores[0].GetScore());
			Assert.AreEqual(0.6F, mock.skillScores[1].GetScore());
			Assert.AreEqual(0.8F, mock.total);
			
		}
		
	internal class MockAssessmentCompletionResponse : AssessmentCompletionResponse {
		public float total;
		public IList<SkillScoreStructure> skillScores;
		public void OnCompletion(float totalScore, IList<SkillScoreStructure> skillScores) {
			this.total = totalScore;
			this.skillScores = skillScores;
		}
	}
				
	internal class MockAssessmentActionResponse : AssessmentActionResponse 
		{
		
		public string terminatedSkill = null;
		public bool isCompleted = false;
			public bool belowTimeThreshold = false; 
		public void TerminateAssessment(string skillid, bool belowTimeThreshold) {
			terminatedSkill = skillid;
		}
		
		public void CompleteAssessment() {
			isCompleted = true;
		}
			
		public void reset() {
			terminatedSkill = null;
			isCompleted = false;
		}
		}
				
	}

}


