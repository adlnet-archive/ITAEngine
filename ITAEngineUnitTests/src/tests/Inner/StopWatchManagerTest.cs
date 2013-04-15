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
using NUnit.Framework;
using ITAEngine.Inner; 

namespace ITAEngine.Tests
{
	[TestFixture, DescriptionAttribute("SimpleStopWatch Tests")] 
	public class SimpleStopWatchTest 
	{
		[Test, DescriptionAttribute("Test that the SimpleStopWatch Wrapper works. The assertion verifies the time.")]
		public void  BasicStopWatchTest()
		{
			SimpleStopWatch sdt = new SimpleStopWatch();
			Assert.IsNotNull(sdt);
			System.Threading.Thread.Sleep(50);
			long elapsedMs = sdt.Stop();
			Assert.Greater(elapsedMs, 48);
		}
	}
	
	[TestFixture, DescriptionAttribute("StopWatchManager Tests")]
	public class StopWatchManagerTest
	{
		[Test, DescriptionAttribute("Test construction.")]
		public void BasicTest()
		{
			StopWatchManager sw = new StopWatchManager();
			Assert.IsNotNull(sw);
		}
		
		[Test, DescriptionAttribute("Test add timed event ID.  Asssertion verfies the elapsed time.")]
		public void AddTest()
		{
			MyStopWatchManager sw = new MyStopWatchManager();
			sw.NewStopWatch("1", new MockSimpleStopWatch(44));
			Assert.AreEqual(44, sw.GetElapsed("1"));
		}
		
		public class MyStopWatchManager : StopWatchManager
		{
			new public void NewStopWatch(string id, ISimpleStopWatch sw)
			{
				base.NewStopWatch(id, sw);
			}
		}
	}
	
	public class MockSimpleStopWatch : ISimpleStopWatch
	{
		private long elapsed = 0;
		
		public MockSimpleStopWatch(long timeValue)
		{
			elapsed = timeValue;
		}
		
		public long Stop()
		{
			return elapsed;
		}
	}
}

