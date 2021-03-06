﻿using System.Diagnostics.Tracing;

namespace EventSource
{
	[EventSource(Name = "MyCompany")]
	public class MyCompanyEventSource : System.Diagnostics.Tracing.EventSource
	{
		public class Keywords
		{
			public const EventKeywords Page = (EventKeywords)1;
			public const EventKeywords DataBase = (EventKeywords)2;
			public const EventKeywords Diagnostic = (EventKeywords)4;
			public const EventKeywords Perf = (EventKeywords)8;
		}

		public class Tasks
		{
			public const EventTask Page = (EventTask)1;
			public const EventTask DBQuery = (EventTask)2;
		}

		private static MyCompanyEventSource _log = new MyCompanyEventSource();
		private MyCompanyEventSource() { }
		public static MyCompanyEventSource Log { get { return _log; } }

		[Event(1, Message = "Application Failure: {0}",
		Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
		internal void Failure(string message)
		{
			this.WriteEvent(1, message);
		}

		[Event(2, Message = "Starting up.", Keywords = Keywords.Perf,
		Level = EventLevel.Informational)]
		public void Startup()
		{
			this.WriteEvent(2);
		}

		[Event(3, Message = "loading page {1} activityID={0}",
		Opcode = EventOpcode.Start,
		Task = Tasks.Page, Keywords = Keywords.Page,
		Level = EventLevel.Informational)]
		internal void PageStart(int ID, string url)
		{
			if (this.IsEnabled()) this.WriteEvent(3, ID, url);
		}
	}
}
