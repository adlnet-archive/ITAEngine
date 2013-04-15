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
using System.Diagnostics;
using System.Collections.Generic;

namespace ITAEngine.Inner
{
	/// <summary>
	/// The StopWatchManager manages a list of stopwatches.  Creating a new
	/// Stopwatch starts the timer.  Grabbing the elapsed time stops the timer
	/// and removes it from the list.  
	/// </summary>
	public class StopWatchManager
	{
		Dictionary <string, ISimpleStopWatch> swList; 

		public StopWatchManager()
		{
			swList = new Dictionary<string, ISimpleStopWatch>();
		}
		
		//// <summary>Adds a new stopwatch to the Manager and starts the time.</summary>
		public void NewStopWatch(string id) 
		{
			NewStopWatch(id, new SimpleStopWatch());
		}
		
		/// <summary>Used to test NewStopWatch with a mock class.</summary>
		protected void NewStopWatch(string id, ISimpleStopWatch sw)
		{
			swList.Add(id, sw);
		}
		
		/// <summary>Get the elapsed time and remove the StopWatch.</summary>
		/// <param name="id"> The id of the stopwatch.</param>
		/// <returns>The elapsed time in milliseconds</returns>
		public long GetElapsed(string id)
		{
			if (!swList.ContainsKey(id))
				return 0;
			long time = swList[id].Stop();
			swList.Remove(id);
			return time;
		}
	}
	
	
	public interface ISimpleStopWatch
	{
		/// <summary>Returns the time in ms doesn't not clear the stopwatch.</summary>
		///
		/// <returns>The time in milliseconds that has elapsed for the stopwatch.</returns>
		long Stop(); 
	}
	
	public class SimpleStopWatch : ISimpleStopWatch
	{
		private Stopwatch sw; 
		
		public SimpleStopWatch()
		{
			sw = new Stopwatch();
			sw.Start();
		}
		
		public long Stop()
		{
			sw.Stop();
			return sw.ElapsedMilliseconds;
		}
	}
}

