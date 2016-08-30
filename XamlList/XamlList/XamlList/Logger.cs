using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImLogging
{

    public sealed class LogConstants
    {
        public const String TRACE = "Trace";
        public const String WARNING = "Warning";
        public const String ISSUE = "Issue";
        public const String ANALYTICS = "Performance";
        public const String TODO = "Todo";
        public const String EXCEPTION = "Exception";
        public const String ERROR = "Error";
        public const String NOTIFICATION = "Notification";

        public const String USER = "User";
        public const String DEVELOPER = "Developer";
        public const String TESTER = "Tester";
        public const String ANALYTICS_ENGINE = "Analytics Engine";

        public const String USER_LOGS_FILE_NAME = "user_log";
        public const String USER_LOGS_FOLDER_NAME = "User";
        public const String DEV_LOGS_FILE_NAME = "dev_log";
        public const String DEV_LOGS_FOLDER = "DeveloperLogs";
    }

    public enum ImLogType
    {
        Trace = 0,
        Warning = 1,
        Issue = 2,
        Analytics = 3,
        Todo = 4,
        Exception = 5,
        Error = 6,
        Notification = 7
    };


    public enum ImLogPermission
    {
        USER = 0,
        DEVELOPER = 1,
        TESTER = 2,
        ANALYTICS_ENGINE = 3
    };

    public sealed class ImLog
    {
        public int id { get; set; }

        public DateTime time { get; set; }

        public String message { get; set; }

        public String filePath { get; set; }

        public String methodName { get; set; }

        public ImLogType type { get; set; }

        public ImLogPermission permission { get; set; }

        public String tag { get; set; }
    }

    public class Logger
    {
        static Stopwatch logWatch = new Stopwatch();

        #region Logging

        public static void LogStart(bool lap = false)
        {
            if (!lap)
                logWatch.Reset();
            DLog(ImLogType.Trace, "Execution Started");
            logWatch.Start();
        }

        public static void LogElapsedTime(bool lap = false)
        {
            DLog(ImLogType.Trace, "Execution completed in " + logWatch.ElapsedMilliseconds);
            if (lap)
                logWatch.Stop();
        }

        /// <summary>
        /// Logs which can be viewed by the user in the application
        /// </summary>
        /// 
        public static void ULog(ImLogType type, String message, String tag = "", [CallerMemberName] String methodName = "", [CallerFilePath] String filePath = "")
        {
            Log(message, ImLogPermission.USER, type, tag, methodName, filePath);
        }

        /// <summary>
        /// Logs which can be viewed by developer in the console
        /// If needed in the app while testing process 
        /// 
        /// </summary>
        public static void DLog(ImLogType type, String message, [CallerMemberName] String methodName = "", [CallerFilePath] String filePath = "")
        {
            Log(message, ImLogPermission.DEVELOPER, type, LogConstants.DEVELOPER, methodName, filePath);
        }

        /// <summary>
        /// Logs which can be uploaded to Analytics engine
        /// </summary>
        public static void ALog(String message, String tag = "", [CallerMemberName] String methodName = "", [CallerFilePath] String filePath = "")
        {
            Log(message, ImLogPermission.ANALYTICS_ENGINE, ImLogType.Analytics, tag, methodName, filePath);
        }

        private static void Log(ImLog imlog)
        {

            String logType = String.Empty;

            switch (imlog.type)
            {
                case ImLogType.Error:
                    {
                        logType = LogConstants.ERROR;
                    }
                    break;
                case ImLogType.Exception:
                    {
                        logType = LogConstants.EXCEPTION;
                    }
                    break;
                case ImLogType.Issue:
                    {
                        logType = LogConstants.ISSUE;
                    }
                    break;
                case ImLogType.Analytics:
                    {
                        logType = LogConstants.ANALYTICS;
                    }
                    break;
                case ImLogType.Todo:
                    {
                        logType = LogConstants.TODO;
                    }
                    break;
                case ImLogType.Trace:
                    {
                        logType = LogConstants.TRACE;
                    }
                    break;
                case ImLogType.Warning:
                    {
                        logType = LogConstants.WARNING;
                    }
                    break;

                case ImLogType.Notification:
                    {
                        logType = LogConstants.NOTIFICATION;
                    }
                    break;
            }

            System.Diagnostics.Debug.WriteLine("\n[" + logType + "]:" + "@ [" + imlog.time + "]" + "[" + imlog.filePath + "." + imlog.methodName + "]" + "\n" + imlog.message + "\n");

        }

        private static void Log(String message, ImLogPermission permission, ImLogType type, String tag, [CallerMemberName] String methodName = "", [CallerFilePath] String filePath = "")
        {
            ImLog imlog = new ImLog();

            imlog.message = message;
            imlog.time = DateTime.Now;
            imlog.permission = permission;
            imlog.type = type;
            imlog.filePath = filePath;
            imlog.methodName = methodName;
            imlog.tag = tag;

            Log(imlog);
        }

        public static void DLog(String message, [CallerMemberName] String methodName = "", [CallerFilePath] String filePath = "")
        {
            Log(message, ImLogPermission.DEVELOPER, ImLogType.Trace, String.Empty, methodName, filePath);
        }

        #endregion
    }

}
