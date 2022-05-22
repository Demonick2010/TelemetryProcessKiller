using System.Diagnostics;
namespace Models
{
    public class ProcessModel
    {
        public string ProcessName { get; set; }

        public ProcessModel(string processName)
        {
            ProcessName = processName;
        }

        /// <summary>
        /// Check process running right now
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsProcessRunning()
        {
            // Try to get all existing processes by name
            var processes = Process.GetProcessesByName(ProcessName);
            return processes is null;
        }
    }
}
