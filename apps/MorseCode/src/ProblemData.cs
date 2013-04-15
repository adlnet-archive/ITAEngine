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

namespace MorseCode
{
	public class ProblemData
	{
		public Dictionary<string, ProblemRec> pd;
		
		public ProblemData ()
		{
			pd = new Dictionary<string, ProblemRec>();
		}
		
		public void AddProblem(string id, string groupId, string tags, bool isChallenge, string statement)
		{
			pd.Add(id, new ProblemRec(id, groupId, tags, isChallenge, statement));
		}
		
		public class ProblemRec
		{
			public string id;
			public string groupId;
			public string tags;
			public bool isChallenge;
			public string statement; 
			
			public ProblemRec(string id, string groupId, string tags, bool isChallenge, string statement)
			{
				this.id = id; 
				this.groupId = groupId;
				this.tags = tags;
				this.isChallenge = isChallenge;
				this.statement = statement;	
			}
		}
	}
}
