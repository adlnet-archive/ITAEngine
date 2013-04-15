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



// 
// Copyright 2012 Advanced Training & Learning Technology, LLC
// 
// Author: James Hubbard   jhubbard@atlt-llc.com
//
using System;
using System.Collections.Generic;

namespace TestScriptCommon
{
	public class SkillData
	{
		public Dictionary<string, SkillRec> sd;
		
		public SkillData ()
		{
			sd = new Dictionary< string, SkillRec>();
		}
		
		public void AddSkill(string id, string desc)
		{
			sd.Add(id, new SkillRec(id, desc));
		}
		
		public class SkillRec
		{
			public string id;
			public string desc;
			
			public SkillRec(string id, string desc)
			{
				this.id = id;
				this.desc = desc;
			}
		}
	}
}

