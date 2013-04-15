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

namespace MorseCode
{
	public class ProblemDataXMLLoader
	{
		public ProblemDataXMLLoader ()
		{
		}

		public static void ProblemDataXmlLoad(string data, ProblemData pd)
		{
			XmlDocument problemXmlDocument = new XmlDocument();
			problemXmlDocument.LoadXml(data);
			foreach(XmlNode problemNode in problemXmlDocument.GetElementsByTagName("problem")) {
				string id = problemNode.Attributes.GetNamedItem("UID").InnerText;
				string tags = problemNode.Attributes.GetNamedItem("tags").InnerText;
				bool isChallenge = (problemNode.Attributes.GetNamedItem("isChallenge").InnerText == "true");
				string groupId = problemNode.Attributes.GetNamedItem("GroupID").InnerText;
				string statement = problemNode.Attributes.GetNamedItem("statement").InnerText;
				pd.AddProblem(id, groupId, tags, isChallenge, statement);
			}
		}
	}
}
