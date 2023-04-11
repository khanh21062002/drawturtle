using log4net;
using System;
namespace ArcFaceService.Utils
{
    /// <summary>
    /// Log
    /// </summary>
    public static class LogUtil
    {
        /// <summary>
        /// loginfo
        /// </summary>
        private static ILog loginfo = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="type">Type</param>param>
        /// <param name="se">info msg</param>
        public static void LogInfo(Type type, string msg)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(type.ToString(), new Exception(msg));
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="type">Type</param>param>
        /// <param name="se"></param>
        public static void LogInfo(Type type, Exception ex)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(type.ToString(), ex);
            }
        }
        
    }
}
