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

namespace ITAEngine.Inner
{
	/**
	 * 
	 **/
	public class Grader
	{
		
		private int maxNumQuestions;
		
		private int numCorrect;
		private int numIncorrect;
		
		private bool isCompleted;
		
		public Grader (int maxNumQuestions)
		{
			this.maxNumQuestions = maxNumQuestions;
		}
		
		public float getTheoreticalBestScore() {
			float numAttempted = numCorrect + numIncorrect;
			float numRemaining = maxNumQuestions - numAttempted;
			return (numCorrect + numRemaining) / maxNumQuestions;
		}
		
		public float getTheoreticalWorstScore() {
			return ((float)numCorrect) / maxNumQuestions;	
		}
		
		public void increment(bool isCorrect) {
			if(isCorrect) {
				numCorrect += 1;	
			} else {
				numIncorrect += 1;
			}
		}
		
		public bool IsCompleted() {
			return isCompleted;	
		}
		
		public void setCompleted(bool b) {
			isCompleted = b;	
		}
		
	}
}

