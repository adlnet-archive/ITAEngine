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



using System;
using System.Collections.Generic;
using UnityEngine;
using ITAEngine;

using TestScriptCommon;
using MorseCode;

namespace Manager
{
	public class ServiceManager
	{
		private static ServiceManager instance = null;
		public DataManager dm = null;
		private int problemSetSize = 3; 
		private int currentAssessmentNum; 
		
		public Translate trans;
		public Assessment assessment;
		public AssetRecommendationEngine are;
		public List<AssetRecomendation> recom; 
		public ActionResponse actResponse; 
		public List<string> probList;
		
		public static ServiceManager GetInstance() 
		{
			if (instance == null)
				instance = new ServiceManager();
			
			return instance;
		}
		
		private ServiceManager ()
		{
			TextAsset data = Resources.Load ("morsecode_data") as TextAsset;
			dm = new DataManager();
			dm.SetXMLStringData(data.text);
			
			trans = new Translate();
			currentAssessmentNum = 1;
		}
		
		public Assessment GetAssessment() 
		{
			actResponse = new ActionResponse();

			are = null; 
			recom = null; 
			
			AssessmentBuilder aBuild = dm.ita.CreateAssessmentBuilder();
			aBuild.AddProblem(problemSetSize, currentAssessmentNum.ToString(), "random");
			assessment = aBuild.Build();
			probList = new List<string>(assessment.GetProblemSequence());
			return assessment;
		}
		
		public void MarkProblemStarted(string probId)
		{
			assessment.MarkProblemStarted(probId);
		}
		
		public bool MarkProblemCompleted(string probId, bool isCorrect)
		{
			assessment.MarkCompleted(probId, isCorrect, actResponse);
			if (actResponse.HasFailed)
				return false;
			else 
				return true; 
		}
		
		public void CloseAssessment()
		{
			CompResp compResp = new CompResp();
			if (assessment != null) {
				assessment.Close(compResp);
				assessment = null;
			}
			probList = null;
			if (actResponse.HasFailed) {
				are = dm.ita.CreateAssetRecommendationEngine();
			} else {
				currentAssessmentNum++; 
				if (currentAssessmentNum == 6)
					currentAssessmentNum = 0;
			}
		}
		
		public IList<AssetRecomendation> GetRecommendationForProblemGroup(string skill)
		{
			if (are == null)
				are = dm.ita.CreateAssetRecommendationEngine();
			return are.GetRecommendationsFor(skill);
		}
	}
}

