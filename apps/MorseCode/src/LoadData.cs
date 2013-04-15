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
using System.Xml;
using System.IO;

namespace MorseCode
{
	public class LoadData
	{
		private static string path = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "data" + 
			Path.DirectorySeparatorChar;
		
		public LoadData ()
		{
		}
		
		public static void LoadXmlData()
		{
			GetProblemData(DataManager.problemData);
			GetSkillAssetData(DataManager.skillData, DataManager.assetData);
		}
		
		public static void GetProblemData(ProblemData pd)
		{
			string file = path + "problems.xml";
			string data = GetStringFromXML(file);
			
			ProblemDataXMLLoader.ProblemDataXmlLoad(data, pd);
			
			foreach (string id in pd.pd.Keys)
				DataManager.ita.AssociateAssessmentProblemWithSkill(pd.pd[id].tags, pd.pd[id].groupId, id, 0, 0, 0);
		}

		public static void GetSkillAssetData(SkillData sd, AssetData ad)
		{
			string file = path + "skill.xml";
			string data = GetStringFromXML(file);
			SkillAssetXmlLoader.LoadXmlData(data, sd, ad);
			foreach (string id in sd.sd.Keys)
				DataManager.ita.AddSkill(id, false);

			foreach (string id in ad.ad.Keys)
			{
				DataManager.ita.AssociateAssetWithSkill(ad.ad[id].skillId, "text", id, 0, 0, 0);
			}
		}

		public static string GetStringFromXML(string fileName)
		{
			StreamReader sread = new StreamReader(fileName);
			string text = sread.ReadToEnd();
			sread.Close();
			return text;
		}
	}
}

