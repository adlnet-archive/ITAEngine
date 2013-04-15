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



//-----------------------------------------------------------------------
// <copyright company="AT&amp;LT">
//     Copyright (c) AT&amp;LT LLC. All rights reserved.
// </copyright>
// <author>Peter Franza</author>
// <email>pfranza@atltgames.com</email>
//-----------------------------------------------------------------------

using System;

namespace ITAEngine
{
	public interface DataStorageDelegate
	{
		/**
		 *   Store the details of specific problem data out.
		 **/
		void UpdateProblemData(string problemId, int timesCorrect, int timesAttempted, long lastAttempted);
		
		/**
		 *   Store the details of specific assets data out
		 **/
		void UpdateAssetData(string assetId, int timesAccessed, long firstAccessed, long lastAccessed);
		
		/**
		 *  Store the data of specific skill clearance out
		 **/
		void UpdateSkillClearanceState(string skillId, bool isCompleted);
	}
}

