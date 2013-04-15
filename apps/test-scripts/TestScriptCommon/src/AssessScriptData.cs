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

namespace TestScriptCommon
{
	public class AssessScriptData
	{
		public int problemCount = 0;
		public bool useMarkAll = false;
		public bool markAll = false;
		public bool expectSuccess = false;
		public string groupId;
		public string verifySkills; 
		public List<bool> markProblems;
		public bool checkForProblemDupes = false; 
		public TutorScriptData tutorData; 

		public AssessScriptData()
		{
			markProblems = new List<bool>();
		}
	}

	public class TutorScriptData
	{
		public List<string> verifyOrder;
		public List<string> chooseType; 

		public TutorScriptData ()
		{
			verifyOrder = null;
			chooseType = null;
		}

		public void AddTypeChoice(string types)
		{
			chooseType = new List<string>(types.Split(','));
		}

		public void AddTypeOrder (string order)
		{
			verifyOrder = new List<string> (order.Split (','));
		}
	}
}

