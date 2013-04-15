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



///-----------------------------------------------------------------------
/// <copyright company="AT&amp;LT">
///     Copyright (c) AT&amp;LT LLC. All rights reserved.
/// </copyright>
/// <author>Peter Franza</author>
/// <email>pfranza@atltgames.com</email>
///-----------------------------------------------------------------------

using System;

namespace ITAEngine
{
	/// <summary>This is the primary Interface through which the ITA is accessed.</summary>

	public interface ITAEngineManager
	{
		/// <summary>Add a skill to the engine.</summary>
		/// 
		/// <param name="skillId">The unique identifier of the skill.</param>
		/// <param name="isCompleted">Indicates that this skilll has been successfully completed.</param>
		 
		void AddSkill(string skillId, bool isCompleted);
		
		/// <summary>Associate a specific learning asset with a particular skill.</summary>
		/// 
		/// <param name="skillId"> The uniquie id of the skill.</param>
		/// <param name="assetType"> The type of the asset.</param>
		/// <param name="assetIdentifier"> The unique identifier of the asset.</param>
		/// <param name="timesAccessed"> The number of times that the asset has been used.</param>
		/// <param name="firstAccessed"> The time when the asset as first accessed.</param>
		/// <param name="lastAccessed"> The time when the asset was last accessed.</param>
		 
		void AssociateAssetWithSkill(string skillId, string assetType, string assetIdentifier, 
			int timesAccessed, long firstAccessed, long lastAccessed);
		
		/// <summary>Associate a specific assessment problem with a particular skill.</summary>
		/// 
		/// <param name="skillId"> The unique identifier of the skill.</param>
		/// <param name="problemGroupIdentifier"> The unique identifier of a group of problem.</param>
		/// <param name="problemUniqueIdentifer"> The unique identifier of the a specific problem.</param>
		/// <param name="timesAttempted"> A count of the number attempts that have been made to answer the problem.</param>
		/// <param name="timeCorrect">A count of the number times the problem has been answered correctly.</param>
		/// <param name="lastAttempted">The time of the last attempt at the problem.</param>
		 
		void AssociateAssessmentProblemWithSkill(string skillId, string problemGroupIdentifier, 
			string problemUniqueIdentifier, int timesAttempted, int timesCorrect, long lastAttempted);

		/// <summary>Associate a specific assessment problem with a particular skill.</summary>
		/// 
		/// <param name="skillId"> The unique identifier of the skill.</param>
		/// <param name="problemGroupIdentifier"> The unique identifier of a group of problem.</param>
		/// <param name="problemUniqueIdentifer"> The unique identifier of the a specific problem.</param>
		/// <param name="timesAttempted"> A count of the number attempts that have been made to answer the problem.</param>
		/// <param name="timeCorrect">A count of the number times the problem has been answered correctly.</param>
		/// <param name="lastAttempted">The time of the last attempt at the problem.</param>
		/// <param name="timeThreshold">When a problem is answered below the threshold the recommendation engine 
		/// can indicate that the questions are being answered too quickly.</param>
		void AssociateAssessmentProblemWithSkill(string skillId, string problemGroupIdentifier, 
		                                         string problemUniqueIdentifier, int timesAttempted, 
		                                         int timesCorrect, long lastAttempted, long timeThreshold);

		/// <summary>Associate problems to instructional assets.</summary>
		/// 
		/// <param name="problemID">The unique identifier of the problem.</param>
		/// <param name="assets">The array of asset ids that the problem is related to.</param>
		 
		void AssociateProblemWithAssets(string problemId, string [] assets);

		/// <summary>Associate a problem group with Assets</summary>
		 		
		void AssociateProblemGroupWithAssets(string groupId, string [] assets);

		/// <summary>Create an instance of an AssessmentBuilder.</summary> 
		 
		AssessmentBuilder CreateAssessmentBuilder();
		
		/// <summary>Create an instance of the AssetRecommendationEngine.</summary> 
		 
		AssetRecommendationEngine CreateAssetRecommendationEngine();
		
		/// <summary>Call this method with a callback to flush out any data that needs to be saved.</summary> 
		 
		void StoreData(DataStorageDelegate storeDelegate);
	}
}

