using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class LogWriter
    {
        #region Singleton

        public static LogWriter Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static LogWriter Instance = new LogWriter();
        }

        private LogWriter()
        {
            Task.Run(() => RecordLog());
        }

        #endregion Singleton

        private const string FolderName = "Incident";
        private const string FileName = "OperationMessage.log";
        private const string BackupName = "BackupLog";
        private const uint LogFileSize = 8388608;
        private const uint KeepDate = 90;

        public Action<string> OnLog;
        private ConcurrentQueue<string> m_logMessageQueue = new ConcurrentQueue<string>();

        public void Log(string log)
        {
            string logInfo = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            string logMsg = $"{logInfo,-30}{log.Replace("\r\n", $"\r\n{"",-30}")}";
            m_logMessageQueue.Enqueue(logMsg);
        }

        public void LogError(Exception ex)
        {
            Log($"{ex.Message}\r\n{ex.StackTrace}");
        }

        private async Task RecordLog()
        {
            string filePath = Path.Combine(FolderName, FileName);
            while (true)
            {
                try
                {
                    await Task.Delay(10);

                    if (m_logMessageQueue.Count == 0)
                        continue;

                    if (!Directory.Exists(FolderName))
                        Directory.CreateDirectory(FolderName);

                    using (StreamWriter sw = new StreamWriter(filePath, true))
                        while (m_logMessageQueue.TryDequeue(out string logMessage))
                        {
                            sw.WriteLine(logMessage);
                            OnLog?.Invoke(logMessage);
                        }

                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length > LogFileSize)
                    {
                        BackupLogFile(fileInfo, BackupName);
                        DeleteOldLogFile(FolderName, KeepDate, BackupName, ".log");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void BackupLogFile(FileInfo fileInfo, string backupFileName)
        {
            string backupFilePath = Path.Combine(fileInfo.DirectoryName, $"{backupFileName}_{DateTime.Now.ToString("yyyyMMddmmhhss")}.log");
            fileInfo.CopyTo(backupFilePath);
            fileInfo.Delete();
        }

        private void DeleteOldLogFile(string directoryPath, long keepDate, string filter, string extension)
        {
            DirectoryInfo di = new DirectoryInfo(directoryPath);
            DateTime criteriaTime = DateTime.Now;
            foreach (var item in di.GetFiles())
            {
                if (item.Extension != extension || !item.Name.StartsWith(filter))
                    continue;

                DateTime creationTime = item.CreationTime;
                int passDate = (criteriaTime - creationTime).Days;
                if (passDate > keepDate)
                    item.Delete();
            }
        }

        public bool IsWritingLog()
        {
            return m_logMessageQueue.Count > 0;
        }
    }
}