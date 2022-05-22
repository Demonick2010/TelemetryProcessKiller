using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProcessModel
    {
        public string ProcessName { get; set; }

        public ProcessModel(string processName)
        {
            ProcessName = processName;
        }

        public bool IsProcessRunning()
        {
            // Try to get all existing processes by name
            var processes = Process.GetProcessesByName(ProcessName);
            return processes is null;
        }
    }
}
