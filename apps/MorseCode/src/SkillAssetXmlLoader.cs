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
	public class SkillAssetXmlLoader
	{
		public SkillAssetXmlLoader ()
		{
		}
		
		public static void LoadXmlData(string data, SkillData sd, AssetData ad)
		{
			XmlDocument skillXmlDocument = new XmlDocument();
			skillXmlDocument.LoadXml(data);
			foreach(XmlNode skillNode in skillXmlDocument.GetElementsByTagName("skill")) {
				string id = skillNode.Attributes.GetNamedItem("id").InnerText;
				string desc = skillNode.Attributes.GetNamedItem("name").InnerText;
				sd.AddSkill(id, desc);
				foreach(XmlNode linksDefn in skillNode.ChildNodes) {
					string assetId = linksDefn.Attributes.GetNamedItem("id").InnerText;
					string lesson = linksDefn.InnerText;
					ad.AddAsset(assetId, id, lesson);
				}
			}
		}
	}
}

