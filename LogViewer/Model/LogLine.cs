using System;

namespace LogViewer
{
	public class LogLine
	{
		public LogLine (string timestamp = "", string process = "", string level = "", string message = "")
		{
			this.Timestamp = timestamp;
			this.Process = process;
			this.Level = level;
			this.Message = message;
		}

		public string Timestamp;
		public string Process;
		public string Level;
		public string Message;
	}
}

