using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class LoggerService
    {
        private readonly string _logFilePath;

        public LoggerService()
        {
            _logFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\" + "log" + DateTime.Now.ToString(("MM.dd.yyyy HH-mm-ss")) + ".txt";
        }

        public void Log(string message)
        {
            using (StreamWriter fileStream = File.Exists(_logFilePath) ? File.AppendText(_logFilePath) : File.CreateText(_logFilePath))
            {
                fileStream.WriteLine(DateTime.Now.ToString(("MM.dd.yyyy HH-mm-ss.fff")) + " - " + message);
            }
        }

        public string Path()
        { 
            return _logFilePath; 
        }
    }
}
