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
using ITAEngine;

namespace MorseCode
{
	public class ActionResponse : AssessmentActionResponse
	{
		bool hasfailed;
		bool hasfinished; 
		string skillId; 
		bool belowThreshold; 
		public bool HasFinished { get { return hasfinished; } }

		public ActionResponse ()
		{
			hasfailed = false; 
			skillId = "0";
			belowThreshold = false;
		}
		
		public bool HasFailed ()
		{
			return hasfailed;
		}
		
		public string GetSkillId()
		{
			return skillId;
		}
		public void TerminateAssessment(string skillId, bool timingBelowThreshold)
		{
			this.skillId = skillId;
			hasfinished = true; 
			hasfailed = true; 
			timingBelowThreshold = belowThreshold;
		}
		
		public void CompleteAssessment()
		{
			hasfinished = true; 
		}
	}
}

