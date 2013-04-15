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
using System.IO;
using System.Xml;

namespace TestScriptCommon
{
	public class XMLDataLoader
	{
		protected XMLDataLoader ()
		{
		}

		public static string GetStringFromXML(string fileName)
		{
			StreamReader sread = new StreamReader(fileName);
			string text = sread.ReadToEnd();
			sread.Close();
			return text;
		}

		public static void ProblemDataLoad (string data, ProblemData pd)
		{
			XmlDocument problemXmlDocument = new XmlDocument ();
			problemXmlDocument.LoadXml (data);
			foreach (XmlNode problemsNode in problemXmlDocument.SelectNodes("test/test_data/problems")) {
				foreach (XmlNode problemNode in problemsNode.SelectNodes("problem")) {
					string id = problemNode.Attributes.GetNamedItem ("UID").InnerText;
					string tags = problemNode.Attributes.GetNamedItem ("tags").InnerText;
					bool isChallenge = (problemNode.Attributes.GetNamedItem ("isChallenge").InnerText == "true");
					string groupId = problemNode.Attributes.GetNamedItem ("GroupID").InnerText;
					string statement = GetNodeString(problemNode.Attributes, "statement");
					pd.AddProblem (id, groupId, tags, isChallenge, statement);
				} 
			}
		}

		public static void AssetDataLoad(string data, SkillData sd, AssetData ad)
		{
			XmlDocument skillXmlDocument = new XmlDocument();
			skillXmlDocument.LoadXml(data);
			foreach(XmlNode skillNode in skillXmlDocument.SelectNodes("test/test_data/skills/skill")) {
				string id = skillNode.Attributes.GetNamedItem("id").InnerText;
				string desc = skillNode.Attributes.GetNamedItem("name").InnerText;
				sd.AddSkill(id, desc);
				foreach(XmlNode linksDefn in skillNode.SelectNodes("asset")) {
					string assetId = GetNodeString(linksDefn.Attributes, "id");
					string type = GetNodeString(linksDefn.Attributes,"type");
					string lesson = linksDefn.InnerText;
					ad.AddAsset(assetId, id, type, lesson);
				}
			}
		}

		public static void TestPlanLoad (string data, TestPlan testPlan)
		{
			XmlDocument scriptDoc = new XmlDocument ();
			scriptDoc.LoadXml (data);

			foreach (XmlNode planNode in scriptDoc.GetElementsByTagName("test")) {
				testPlan.name = GetNodeString (planNode.Attributes, "name");
			}

			foreach (XmlNode testNode in scriptDoc.GetElementsByTagName("test_script")) {
				foreach (XmlNode assessNode in testNode.SelectNodes("assessment")) {
					AssessScriptData assess = new AssessScriptData ();
					assess.problemCount = int.Parse (GetNodeString (assessNode.Attributes, "problem_count"));
					string use_mark_all = GetNodeString (assessNode.Attributes, "mark_all");
					if (use_mark_all != null) {
						assess.useMarkAll = true;
						assess.markAll = (use_mark_all == "true") ?  true : false;
					}
					assess.expectSuccess = (GetNodeString (assessNode.Attributes, "expect_success") == "true" ? true : false);
					assess.groupId = GetNodeString (assessNode.Attributes, "group_id");
					GetAssessSkillList(assessNode, assess);
					assess.tutorData = GetTutorData(assessNode);
					assess.checkForProblemDupes = (GetNodeString (assessNode.Attributes, "check_dupes") == "true" ? true : false);
					testPlan.scripts.Add (assess);
				}
			}
		}

		private static TutorScriptData GetTutorData (XmlNode node)
		{
			XmlNode tutorNode = node.SelectNodes("tutor")[0];
			if (tutorNode == null)
				return null;

			TutorScriptData tutorData = new TutorScriptData();

			string typeChoice = GetNodeString(tutorNode.Attributes, "choose_asset_type");
			string typeOrder =  GetNodeString(tutorNode.Attributes, "verify_type_order");

			if (typeChoice != "")
				tutorData.AddTypeChoice(typeChoice);
			if (typeOrder != "")
				tutorData.AddTypeOrder(typeOrder);
			return tutorData;
		}

		private static void GetAssessSkillList (XmlNode node, AssessScriptData assess)
		{
			XmlNode verifySkillNode = node.SelectNodes("verify_skills") [0];
			if (verifySkillNode == null) {
				return;
			}
			assess.verifySkills = verifySkillNode.Attributes.GetNamedItem ("list").InnerText;
		}

		private static string GetNodeString(XmlAttributeCollection attrs, string name)
		{
			XmlNode node = attrs.GetNamedItem(name);
		 	if ( node == null)
				return "";
			else
				return node.InnerText;
		}
	}
}

