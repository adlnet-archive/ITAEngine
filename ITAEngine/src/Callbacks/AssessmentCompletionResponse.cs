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
using System.Collections.Generic;

namespace ITAEngine
{
	public interface AssessmentCompletionResponse
	{
		/// <summary>This callback is used to pass the final scores for a single iteration of an assessment
		///   both the aggregate score and the individule skill scores are present. </summary>
		/// <param name="totalScore">The total score of the Assessment.</param>
		/// <param name="skillScores">Scores for each skill</param>
		void OnCompletion(float totalScore, IList<SkillScoreStructure> skillScores);
	}
}

