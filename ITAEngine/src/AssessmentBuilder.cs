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
	public interface AssessmentBuilder
	{
		/// <summary>Add a specific number of a type of problem to the assessment</summary>
		///  
		/// <param name="problemCount">The number of problems to be used.</param>
		/// <param name="problemGroupId"> The group Id of the problems.</param>
		/// <param name="randomizationGroup"> is used to seed the random number generator</param>

		AssessmentBuilder AddProblem(int problemCount, string problemGroupId, string randomizationGroup);

		/// <summary>Create an assessment instance.</summary>
		///
		/// <returns>Assessment which contains the problems and challenge data
		///			needed to determine a student's knowledge.</returns>

		Assessment Build();
	}
}

