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



using NUnit.Framework;
using System;
using ITAEngine.Inner;
using System.Collections.Generic;

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("DefaultAssessmentBuilder Tests")]
	public class DefaultAssessmentBuilderTest
	{
		[Test, DescriptionAttribute("Test construction.")]
		public void TestCase()
		{
			ProblemDataManager pdm = new ProblemDataManager();
			SkillsDataManager sdm = new SkillsDataManager();

			DefaultAssessmentBuilder dab = new DefaultAssessmentBuilder(pdm, sdm);
			Assert.IsNotNull(dab);
		}
		
		[Test, DescriptionAttribute("Test the creation of a basic assessment.")]
		public void TestBasicAssessment()
		{
			SkillsDataManager sdm = new SkillsDataManager();
			sdm.AddSkill("s1", false);

			ProblemDataManager pdm = new ProblemDataManager();
			pdm.AddProblem("s1", "grp1", "s1g1-1", 0, 0, 0);

			DefaultAssessmentBuilder dab = new DefaultAssessmentBuilder(pdm, sdm);
			dab.AddProblem(1, "grp1", "randomGrp");
			
			Assessment assess = dab.Build();
			Assert.IsNotNull(assess);
			
			IList<string> probIds = assess.GetProblemSequence(); 
			Assert.AreEqual(probIds.Count, 1);
			Assert.AreEqual(probIds[0], "s1g1-1");
		}

		public void TestRandomize(int problemSetSize, int totalProblems, bool isCorrect)
		{
			string s1 = "s1";
			string g1 = "g1";
			string sgp = s1 + g1 + "-"; 
				
			SkillsDataManager sdm = new SkillsDataManager();
			sdm.AddSkill(s1, false);

			ProblemDataManager pdm = new ProblemDataManager();
			for (int i = 0; i < totalProblems; i++)
				pdm.AddProblem(s1, g1, sgp + i.ToString(), 0, 0, 0);

			//Hold the completed problems for later comparison
			IList<string> completedList = new List<string>();

			for (int i = 0; i < totalProblems / problemSetSize; i++) {
				DefaultAssessmentBuilder dab = new DefaultAssessmentBuilder(pdm,sdm);
				dab.AddProblem (problemSetSize, g1, "randgrp321");
				Assessment assess = dab.Build();
				IList<string> probs = assess.GetProblemSequence();
				foreach(string prob in probs) {
					pdm.MarkCompleted(prob, isCorrect);
					Assert.IsFalse(completedList.Contains(prob), prob + ": Already in List"); 
					completedList.Add(prob);
				}
			}
		}
		
		[Test, DescriptionAttribute("Test an all true assessment with a set size of 1 with a total of 6 possible problems.")]
		public void TestAllTrueRandomize_1_6() 
		{
			int problemSetSize = 1; 
			int totalProblems = 6;
			bool answeredCorrect = true; 
			TestRandomize(problemSetSize, totalProblems, answeredCorrect);
		}

		[Test, DescriptionAttribute("Test an all true assessment with a set size of 2 with a total of 6 possible problems.")]
		public void TestAllTrueRandomize_2_6() 
		{
			int problemSetSize = 2; 
			int totalProblems = 6;
			bool answeredCorrect = true; 
			TestRandomize(problemSetSize, totalProblems, answeredCorrect);
		}

		[Test, DescriptionAttribute("Test an all false assessment with a set size of 2 with a total of 6 possible problems.")]
		public void TestAllFalseRandomize_2_6() 
		{
			int problemSetSize = 2; 
			int totalProblems = 6;
			bool answeredCorrect = false; 
			TestRandomize(problemSetSize, totalProblems, answeredCorrect);
		}
		
		[Test, DescriptionAttribute("Test an all true assessment with a set size of 3 with a total of 26 possible problems.")]
		public void TestAllTrueRandomize_3_26() 
		{
			int problemSetSize = 3; 
			int totalProblems = 26;
			bool answeredCorrect = true; 
			TestRandomize(problemSetSize, totalProblems, answeredCorrect);
		}

		[Test, DescriptionAttribute("Test an all false assessment with a set size of 3 with a total of 26 possible problems.")]
		public void TestAllFalseRandomize_3_26() 
		{
			int problemSetSize = 3; 
			int totalProblems = 26;
			bool answeredCorrect = false; 
			TestRandomize(problemSetSize, totalProblems, answeredCorrect);
		}
	}
}

