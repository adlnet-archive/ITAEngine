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
	public class AssetData
	{
		public Dictionary<string, AssetRec> ad; 
		
		public AssetData ()
		{
			ad = new Dictionary<string, AssetRec>();
		}
		
		public void AddAsset(string id, string skillId, string type, string desc = "")
		{
			ad.Add(id, new AssetRec(id, skillId, type, desc));
		}
		
		public class AssetRec
		{
			public string id;
			public string skillId;
			public string type;
			public string desc;
			
			public AssetRec(string id, string skillId, string type, string desc)
			{
				this.id = id;
				this.skillId = skillId;
				this.type = type;
				this.desc = desc; 
			}
		}
	}
}

