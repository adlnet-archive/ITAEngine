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
using ITAEngine;
using ITAEngine.Inner;

namespace TestScriptCommon
{
	public class DataManager
	{
		public ProblemData problemData  = new ProblemData();
		public SkillData skillData = new SkillData();
		public AssetData assetData = new AssetData();
		public TestPlan plan = new TestPlan();
		public ITAEngineManager ita = new DefaultITAEngineManager();
		private string data; 

		public DataManager (string dataFile)
		{
			problemData  = new ProblemData();
			skillData = new SkillData();
			assetData = new AssetData();
			ita = new DefaultITAEngineManager();
			data = XMLDataLoader.GetStringFromXML(dataFile);

			LoadData();
		}

		public DataManager ()
		{
			problemData  = new ProblemData();
			skillData = new SkillData();
			assetData = new AssetData();
			ita = new DefaultITAEngineManager();
			
		}

		public void SetXMLStringData (string xmlstring)
		{
			data = xmlstring;
			LoadData ();
		}

		private void LoadData ()
		{
			GetProblemData();
			GetSkillAssetData();
			GetTestPlanData();
		}


		private void GetProblemData()
		{
			XMLDataLoader.ProblemDataLoad(data, problemData);
			
			foreach (string id in problemData.pd.Keys)
				ita.AssociateAssessmentProblemWithSkill(problemData.pd[id].tags, problemData.pd[id].groupId, id, 0, 0, 0);
		}

		private void GetSkillAssetData ()
		{
			XMLDataLoader.AssetDataLoad (data, skillData, assetData);

			foreach (string id in skillData.sd.Keys) {
				ita.AddSkill (id, false);
			}

			foreach (string id in assetData.ad.Keys)
			{
				ita.AssociateAssetWithSkill(assetData.ad[id].skillId, assetData.ad[id].type, id, 0, 0, 0);
			}
		}

		private void AssociateProblemWithAssest ()
		{
		}

		private void GetTestPlanData()
		{
			XMLDataLoader.TestPlanLoad(data, plan);
		}
	}
}

