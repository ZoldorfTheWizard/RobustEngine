using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sysconsole = System.Console;

namespace RobustEngine.System
{
    public static class RobustConsole
    {
        private static LogLevel ConsoleLL;

        static RobustConsole()
        {
            
		}

		public static void ClearConsole()
		{
			Console.Clear();
		}

        public static void SetLogLevel(LogLevel LL)
        {
            Write(LogLevel.Info, "RobustConsole", "LogLevel Set to: "+LL);
            ConsoleLL = LL;
        }

        public static void Write(LogLevel LL, object Class, string Reason)
        {

            if (LL < ConsoleLL || ConsoleLL == LogLevel.Off)
                return;

            var CallingClass ="";

			if (Class.GetType() == typeof(string))
			{
				CallingClass = (string)Class;
			}
			else
			{
				CallingClass = Class.GetType().Name;
			}

			if (Reason.Length == 0)
				return; 

			Reason = Reason.Replace(". ", ".\n");

            switch (LL)
            {
                case LogLevel.Verbose : WriteVerbose (CallingClass, Reason);  break;
                case LogLevel.Debug   : WriteDebug   (CallingClass, Reason);  break;
                case LogLevel.Info    : WriteInfo    (CallingClass, Reason);  break;
                case LogLevel.Warning : WriteWarning (CallingClass, Reason);  break;
                case LogLevel.Critical: WriteCritical(CallingClass, Reason);  break;
                case LogLevel.Fatal   : WriteFatal   (CallingClass, Reason);  break;
            }
        }

        #region Internal Methods
        private static void WriteVerbose(string whonnock, string whynnock)
        {
            Sysconsole.ForegroundColor = ConsoleColor.DarkGray;
            Sysconsole.Out.Write(DateTime.Now.ToShortTimeString() + " [" + whonnock + "] VERBOSE: " + whynnock + "\n");
        }

        private static void WriteDebug(string whonnock , string whynnock)
        {          
            Sysconsole.ForegroundColor = ConsoleColor.Gray;
            Sysconsole.Out.Write(DateTime.Now.ToShortTimeString() + " [" + whonnock+ "] DEBUG: " + whynnock + "\n");
        }

        private static void WriteInfo(string whonnock, string whynnock)
        {
            Sysconsole.ForegroundColor = ConsoleColor.White;
            Sysconsole.Out.Write(DateTime.Now.ToShortTimeString() + " ["+ whonnock+ "] INFO: " + whynnock + "\n");
        }

        private static void WriteWarning(string whonnock, string whynnock)
        {
            Sysconsole.ForegroundColor = ConsoleColor.DarkYellow;
            Sysconsole.Out.Write(DateTime.Now.ToShortTimeString() + " [" + whonnock + "] WARNING: " + whynnock + "\n");
        }

        private static void WriteCritical(string whonnock, string whynnock)
        {
            Sysconsole.ForegroundColor = ConsoleColor.DarkRed;
            Sysconsole.Out.Write( DateTime.Now.ToShortTimeString() + " [" + whonnock+ "] CRITICAL: " + whynnock + "\n");
        }

        private static void WriteFatal(string whonnock, string whynnock)
        {
            Sysconsole.ForegroundColor = ConsoleColor.Red;
            Sysconsole.Out.Write(DateTime.Now.ToShortTimeString() + " [" + whonnock+ "] FATAL: " + whynnock + "\n");
        }
        #endregion

        
    }

    public enum LogLevel
    {
        Verbose,     
        Debug,
        Info,
        Warning,      
        Critical,
        Fatal,
        Off,
    }
}
