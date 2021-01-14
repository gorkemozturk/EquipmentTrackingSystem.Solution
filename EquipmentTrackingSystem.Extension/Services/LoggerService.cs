using EquipmentTrackingSystem.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EquipmentTrackingSystem.Extension.Services
{
    public sealed class LoggerService : ILoggerService
    {
        private static readonly Lazy<LoggerService> instance = new Lazy<LoggerService>(() => new LoggerService());

        private LoggerService()
        {
        }

        public static LoggerService GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void CreateLog(string message)
        {
            string fileName = string.Format("{0}_{1}.log", "Logs", DateTime.Now.ToShortDateString());
            string path = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}
