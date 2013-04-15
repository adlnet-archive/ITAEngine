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
using ITAEngine.Inner;

namespace ITAEngine
{
	public interface AssetRecommendationEngine
	{
		
		/// <summary>Returns a list of educational assets for a specific skill, in the 
		///   order the player should attempt to learn from them</summary>
		/// 
		/// <param name="skillIndex">The id of the skill that will be used to get
		///         the list of recommendations.</param>
		 
		IList<AssetRecomendation> GetRecommendationsFor(string skillIndex);

		/// <summary>Returns a list of educational assets for a specific problemID</summary>
		/// 
		/// <param name="problemId">The id of the problem that will be used to get
		///         the list of recommendations.</param>
		
		IList<AssetRecomendation> GetRecommendationsForProblem(string problemId);

		
		/// <summary>Set the Heuristics Engine being used to generate asset recommendations.</summary>
		/// 
		/// <para>Allows the ability to plug in different analytic engines.</para>
		/// 
		/// <param name="engine">A different engine for use otherwise default is used.</param>
		 
		void setHeuristicsEngine(IAssetHeuristicEngine engine);
			
		
		/// <summary>Mark that the player has started using a specific asset</summary>
		/// 
		/// <param name="assetIdentifier">The unique id of the asset.</param>
		 
		void MarkAssetStarted(string assetIdentifier);
		
		
		/// <summary>Mark that the player has completed the use of a specific asset.</summary>
		/// 
		/// <param name="assetIdentifier">The unique id of the asset.</param>
		 
		void MarkAssetCompleted(string assetIdentifier);
	}
}

