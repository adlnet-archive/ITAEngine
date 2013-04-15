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



// Copyright 2012 Advanced Training & Learning Technology, LLC
// Author: James Hubbard   jhubbard@atlt-llc.com
//
using System;
using System.Collections.Generic;
using ITAEngine;

namespace TestScriptCommon
{
	public class AssessExecute
	{
		private ITAEngineManager ita;
		private TestPlan planData; 
		private AssessScriptData currentScript; 
		private bool success = false;
		ActionResponse actResp;
		private List<string> seenProblems; 
		private bool hasDuplicateProblems = false; 
		private bool verifyTutorFailed = false;
		public bool Success { get { return success; } }

		public AssessExecute (ITAEngineManager ita, TestPlan planData)
		{
			this.ita = ita;
			this.planData = planData;
		}

		public bool RunScript ()
		{
			Console.WriteLine("***************************************************");
			Console.WriteLine("Beginning Test Execution: " + planData.name);
			seenProblems = new List<string>();
			bool result = false; 
			foreach (AssessScriptData script in planData.scripts) {
				actResp = new ActionResponse ();
				currentScript = script;
				if ( !(result = DoAssessment(ita.CreateAssessmentBuilder())) )
				    break;
			}
			if (result)
				Console.Write("SUCCESS: ");
			else
				Console.Write("FAILED: ");
			Console.WriteLine("Test Named : " + planData.name);
			Console.WriteLine("***************************************************");
			return result;
		}

		public bool  DoAssessment (AssessmentBuilder assessBuild)
		{
			Console.WriteLine ("---------------------------------------------------");
			Console.WriteLine ("Beginning Assessment");
			assessBuild.AddProblem (currentScript.problemCount, currentScript.groupId, "random");
			Assessment assess = assessBuild.Build ();
			MarkProblems (assess);
			bool skillsVerifyFailed = VerifySkillListFailed (assess);
			if (actResp.HasFailed && !currentScript.expectSuccess)
				success = true;
			else if (!actResp.HasFailed && currentScript.expectSuccess)
				success = true;

			if (hasDuplicateProblems || skillsVerifyFailed || verifyTutorFailed)
				success = false;

			CompResp compResp = new CompResp ();

			assess.Close (compResp);

			Console.WriteLine ("Total Score: " + compResp.TotalScore);
			foreach (SkillScoreStructure skillScore in compResp.listSSS) {
				Console.WriteLine("\tSkill id: " + skillScore.GetSkillId() + " score: " + skillScore.GetScore());
			}

			Console.WriteLine("Assessment Completed");

			return success;
		}

		public void MarkProblems (Assessment assess)
		{
			if (currentScript.useMarkAll)
				MarkAll(assess);
		}

		private void MarkAll (Assessment assess)
		{
			IList<string> probList = assess.GetProblemSequence ();
			Console.Write ("Problem Sequence: ");
			foreach (string prob in probList) {
				Console.Write (prob + " ");
			}

			Console.Write("\nMarking Problems " + currentScript.markAll.ToString() + " : " );
			foreach (string prob in probList) {
				Console.Write(prob + " ");
				assess.MarkProblemStarted (prob);
				assess.MarkCompleted (prob, currentScript.markAll , actResp);
				CheckProblemDuplicates(assess, prob);
				if (actResp.HasFailed) {
					if (currentScript.tutorData != null) {
						RunTutor(assess.GetSkills());
					}
					break;
				}
			}
			Console.WriteLine("\n");
		}

		private bool VerifySkillListFailed (Assessment assess)
		{
			if (currentScript.verifySkills == null)
				return false; 

			List<string> list = new List<string> (assess.GetSkills ());
			List<string> skills = new List<string>(currentScript.verifySkills.Split(','));
			for (int i = 0; i < list.Count; i++) {
				if (list[i]  != skills[i]) {
					Console.WriteLine ("FAILURE:  Skill List for assessment doesn't match.");
					PrintSkillLists(list);
					return true;
				}
			}
			return false;
		}

		private void PrintSkillLists (List<string> actual)
		{
			Console.WriteLine("***** Actual Skill List: ");
			for (int i = 0; i < actual.Count; i++) {
				Console.WriteLine ("\t" + actual [i]);
			}
			Console.WriteLine("*************************");
		}

		private bool CheckProblemDuplicates (Assessment assesss, string problemId)
		{
			if (!currentScript.checkForProblemDupes)
				return false;

			if (seenProblems.Contains (problemId)) {
				hasDuplicateProblems = true;
				Console.WriteLine("\nFound duplicate problem in set: " + problemId);
				Console.WriteLine ("Problems already seen. " );
				foreach(string prob in seenProblems) {
					Console.Write (prob + " ");
				}
				Console.WriteLine("");
			} else {
				seenProblems.Add (problemId);
			}
			return hasDuplicateProblems;
		}

		private void RunTutor (IList<string> skillList)
		{
			string [] skills = skillList [0].Split (',');
			if (currentScript.tutorData.chooseType != null) {
				MarkTutorType (skills);
			}

			if (currentScript.tutorData.verifyOrder != null) {
				TutorListOrderVerifyFailed(skills);
			}
		}

		private void MarkTutorType(string [] skills)
		{
			Console.Write("\nMarking Tutor Assets: ");
			AssetRecommendationEngine ae = ita.CreateAssetRecommendationEngine();
			foreach ( string skill in skills) {
				List<AssetRecomendation> arList = new List<AssetRecomendation>(ae.GetRecommendationsFor(skill));
				foreach (string type in currentScript.tutorData.chooseType) {
					foreach (AssetRecomendation ar in arList) {
						if (type == ar.GetAssetType()) {
							Console.Write(type + "," + ar.GetAssetIdentifier() + " " );
							ae.MarkAssetStarted(ar.GetAssetIdentifier());
							ae.MarkAssetCompleted(ar.GetAssetIdentifier());
						}
					}
				}
			}
			Console.Write ("\n");
		}

		private bool TutorListOrderVerifyFailed (string[] skills)
		{
			Console.Write ("\nVerifying Tutor Asset Order: ");
			AssetRecommendationEngine ae = ita.CreateAssetRecommendationEngine ();
			foreach (string skill in skills) {
				List<AssetRecomendation> arList = new List<AssetRecomendation>(ae.GetRecommendationsFor(skill));
				foreach(string type in currentScript.tutorData.verifyOrder) {
					Console.Write(type + "," + arList[0].GetAssetIdentifier() + " " );
					if (type != arList[0].GetAssetType()) {
						Console.WriteLine ("\nAsset type doesn't match order. \n" +
							"\t Expected type: " + type + 
						    " Actual: " + arList[0].GetAssetType() 
						    + " id: " + arList[0].GetAssetIdentifier() );
						verifyTutorFailed = true; 
						return verifyTutorFailed;
					}
				}
			}
			return verifyTutorFailed; 
		}
	}
}

