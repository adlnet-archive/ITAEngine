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
	public class ActionResponse : AssessmentActionResponse
	{
		private bool hasFailed = false;
		private string skill = ""; 
		private bool belowThreashold = false; 
		private bool hasFinished = false; 
		public bool BelowThreashold { get { return belowThreashold; } } 
		public bool HasFailed { get { return hasFailed; } }
		public bool HasFinished { get { return hasFinished; } }
		public string Skill { get { return skill; } }
		
		public void TerminateAssessment(string skillId, bool belowThreshold)
		{
			skill = skillId;
			hasFailed = true;
			hasFinished = true; 
		}
		
		public void CompleteAssessment()
		{
			hasFinished = true;
		}
	}

	public class CompResp : AssessmentCompletionResponse 
	{
		private  float score; 
		public float TotalScore { get { return score; } }
		public List<SkillScoreStructure> listSSS; 
		
		public CompResp ()
		{
			listSSS = null;
			score = 0;
		}
		
		public void OnCompletion(float totalScore, IList<SkillScoreStructure> listSSS)
		{
			score = totalScore;
			this.listSSS = new List<SkillScoreStructure>(listSSS);
		}
	}
}

