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
	public interface Assessment
	{
		/// <summary>Gets the list of skills for the assessment after 
		/// problems have been MarkCompleted.</summary>
		/// 
		/// <para>This call should only be made after problems have been mark 
		/// completed.  The skill list contains only those skills that were
		/// associated with a problem that was answered.</para>
		/// 
		/// <returns>A list of skills to used in the assessment</returns>

		IList<string> GetSkills();

		/// <summary>Return a sequence of problem identifiers.</summary>
		///
		/// <para>Gets the specific list of problems by id, in the order they are to 
		/// be asked for this instance of the assessment.</para>
		/// 
		/// <returns>List of problem identifiers.</returns>

		IList<string> GetProblemSequence();

		/// <summary>Mark that the student has started work on a specific problem</summary>
		/// 
		///  <param name="problemId">The unique identifier of the problem.</param>

		void MarkProblemStarted(string problemId);

		/// <summary>Mark problem as being completed.</summary>
		/// 
		/// <para>Mark that the student has completed a problem, if the problem was answered 
		/// correctly or not, and a callback that provides possible reactions to the answer.</para>
		/// 
		/// <param name="problemId">The unique identifier.</param>
		/// <param name="isCorrect">Indicates that the problem was answered correctly.</param>
		/// <param name="callback">The function that will be returned in the event that a response
		/// 					is needed.</param>

		void MarkCompleted(string problemId, bool isCorrect, AssessmentActionResponse callback);
		
		/// <summary>Close the Assessment</summary>
		/// 
		/// <para>Called upon the completion of the assessment, the callback will provide aggregate scores
		///  as well as the scores specific to each skill in the assessment.</para>
		/// 
		/// <param name="callback"> is used provide a mechanism for score data.</param>

		void Close(AssessmentCompletionResponse callback);
	}
}

