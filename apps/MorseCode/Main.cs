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
using ITAEngine; 
using ITAEngine.Inner;

namespace MorseCode
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.CancelKeyPress += delegate { 
				Console.WriteLine ("\nSorry to see you go.\n");
				ProgramController.keepRunning = false;
			};
			
			LoadData.LoadXmlData();
			ProgramController.Run();
		}
	}
		
	class ProgramController
	{
		public static bool keepRunning = true;
		
		
		public static void Run()
		{
			Translate trans = new Translate();
			
			int assessmentCurrent = 1;
			while (keepRunning && assessmentCurrent <=6)
			{
				Console.WriteLine("--------------------------------------------------");
				Console.WriteLine("   Starting new assessment");
				Console.WriteLine("--------------------------------------------------");
				
				Console.WriteLine("Create AssessmentBuilder");
				AssessmentBuilder aBuild = DataManager.ita.CreateAssessmentBuilder();
				Console.WriteLine("Create a Problem Sequence of 3 problems for group id: " + assessmentCurrent);
				aBuild.AddProblem(3, ""+assessmentCurrent,"random");
				Console.WriteLine("Build the Assessment");
				Assessment assess = aBuild.Build();
				
				IList<string> probList = assess.GetProblemSequence();
				
				ActionResponse actRes = new ActionResponse();
				foreach (string id in probList)
				{
					ProblemData.ProblemRec probRec =  DataManager.problemData.pd[id];
					Console.Write("\nTranslate the following to alpha.\nMorse:  ");
					Console.WriteLine(trans.AlphaToMorse(probRec.statement));
					string line = Console.ReadLine();
					if (!line.Equals(probRec.statement))
					{
						Console.WriteLine("Mark problem completed with FAIL.");
						assess.MarkCompleted(id, false, actRes);
						if (actRes.HasFailed())
						{
							Console.WriteLine("Failed Assessment Terminating");
						}
						break;
					}
					else
					{
						Console.WriteLine("Mark problem completed CORRECT.");
						assess.MarkCompleted(id, true, actRes);
					}
				}
				
				assess.Close(new CompletionResponse());
				if (actRes.HasFailed())
				{
					Console.WriteLine("Create AssetRecommendationEngine");
					AssetRecommendationEngine are = DataManager.ita.CreateAssetRecommendationEngine();
					Console.WriteLine("Displaying Assests for Remediation\n");
					IList<AssetRecomendation> arList = are.GetRecommendationsFor("" + assessmentCurrent);
					
					foreach (AssetRecomendation ar in arList)
					{
						Console.WriteLine("*Asset********************************************");
						string assetId = ar.GetAssetIdentifier();
						string [] text = DataManager.assetData.ad[assetId].desc.Split(new string [] {"\n"}, 
																StringSplitOptions.RemoveEmptyEntries);
						foreach (string line in text)
							Console.WriteLine(line);
						Console.WriteLine("**************************************************\n");
					}
				}
				else
				{
					Console.WriteLine("Finished Asessment Successfully");
					assessmentCurrent++;
				}

			}
		}
	}
}
