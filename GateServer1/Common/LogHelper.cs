using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text;
namespace GateServer1.Common
{
    public class LogHelper
    {
        #region 日志处理

        #region 属性

        private string m_LogName = "DllLog";
        /// <summary>
        /// 日志文件名称属性
        /// </summary>
        public string LogName
        {
            get { return m_LogName; }
            set { m_LogName = value; }
        }

        private bool m_IsWriteLogToFile = true;
        /// <summary>
        /// 是否记录日志数据到文件
        /// </summary>
        public bool IsWriteLogToFile
        {
            get { return m_IsWriteLogToFile; }
            set { m_IsWriteLogToFile = value; }
        }

        private bool m_IsWriteLogToQueue = false;
        /// <summary>
        /// 是否记录日志数据到队列
        /// </summary>
        public bool IsWriteLogToQueue
        {
            get { return m_IsWriteLogToQueue; }
            set { m_IsWriteLogToQueue = value; }
        }

        #endregion

        #region 变量

        /// <summary>
        /// 日志数据队列
        /// </summary>
        private Queue<string> LogDataQueue = new Queue<string>();

        /// <summary>
        /// 日志文件夹路径名称
        /// </summary>
        private string m_LogDire = string.Empty;

        #endregion

        /// <summary>
        /// 日志内容写入日志队列
        /// </summary>
        /// <param name="LogData">日志内容</param>
        public void AddLogData(string LogData)
        {
            try
            {
                string strData = "";
                strData = System.DateTime.Now.ToString("HH:mm:ss:fff") + "  " + LogData;

                if (m_IsWriteLogToFile)
                {
                    AddDllLog(strData);
                }

                if (m_IsWriteLogToQueue)
                {
                    lock (LogDataQueue)
                    {
                        LogDataQueue.Enqueue(strData);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取日志队列数量
        /// </summary>
        /// <returns>日志队列数量</returns>
        public int GetLogCount()
        {
            return LogDataQueue.Count;
        }

        /// <summary>
        /// 获取日志队列
        /// </summary>
        /// <returns>日志队列的值</returns>
        public string ReadLogData()
        {
            string strLogData = "";

            lock (LogDataQueue)
            {
                strLogData = LogDataQueue.Dequeue();
            }

            return strLogData;
        }

        /// <summary>
        /// 清除日志数据队列
        /// </summary>
        public void ClearLogQueue()
        {
            LogDataQueue.Clear();
        }

        /// <summary>
        /// 记录组件日志
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        private void AddDllLog(string logInfo)
        {
            try
            {
                m_LogDire = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }
                m_LogDire += m_LogName + "\\";
                if (!Directory.Exists(m_LogDire))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(m_LogDire);
                }

                // 获取当前日志的文件夹
                string strLogFile = m_LogDire + "\\" + m_LogName + "_" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(strLogFile))
                {
                    // 不存在，创建
                    Directory.CreateDirectory(strLogFile);
                }

                File.AppendAllText(strLogFile +
                        "\\" + m_LogName + "_" + DateTime.Now.ToString("yyyyMMdd_HH") + ".log", logInfo + "\r\n",
                        Encoding.Default);
            }
            catch
            {
            }
        }

        #endregion
    }
}
