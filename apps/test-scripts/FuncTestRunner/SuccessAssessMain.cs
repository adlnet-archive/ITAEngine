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
using TestScriptCommon;
using ITAEngine;

namespace SuccessAssess
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 0) {
				Console.WriteLine ("FAILED: A filename is required");
				Environment.Exit(1);
			}
			if (!System.IO.File.Exists (args [0])) {
				Console.WriteLine("FAILED: File " + args[0] + " does not exist.");
				Environment.Exit(1);
			}
			DataManager dm = new DataManager (args[0]);
			AssessExecute execute = new AssessExecute(dm.ita, dm.plan);
			execute.RunScript();
		}
	}

}
