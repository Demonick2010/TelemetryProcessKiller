using System.Diagnostics;
using Models;

namespace Business
{
    public class ProcessMonitor
    {
        // Interval for checking - 10 seconds
        private const int CHEACKING_INTERVAL = 10000;
        private readonly List<ProcessModel> _processesForCilling;
        private static int _count = 0;

        public ProcessMonitor(List<ProcessModel> processes)
        {
            _processesForCilling = processes;
        }

        /// <summary>
        /// Start monitoring and killing founded processes from list
        /// </summary>
        /// <returns>Nothing, just loop</returns>
        public async Task StartMonitoring()
        {
            Console.WriteLine($"Monitoring started! {DateTime.Now}");

            while (true)
            {
                // Get all current running processes
                var processes = Process.GetProcesses();

                // If processes not found, stop this iteration logic
                if (processes.Length == 0)
                    continue;

                // Esle, start process cycle
                foreach (var currentProcess in processes)
                {
                    foreach (var _ in
                    // Start cheking and compare processes for killing
                    from processForKilling in _processesForCilling
                    where currentProcess.ProcessName.Contains(processForKilling.ProcessName)
                    select new { })
                    {
                        KillTheProcess(currentProcess);
                    }
                }
                await Task.Delay(CHEACKING_INTERVAL);
            }
        }

        /// <summary>
        /// Kill process method
        /// </summary>
        /// <param name="processForKill">Process for killing</param>
        private static void KillTheProcess(Process processForKill)
        {
            try
            {
                ProcessStartInfo info = new()
                {
                    FileName = "CMD.exe",
                    Arguments = $"/C taskkill /im {processForKill.ProcessName}.exe /f",
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas"
                };

                Process killProcess = new()
                {
                    StartInfo = info
                };
                killProcess.Start();
                _count++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Process {processForKill} with ID: {processForKill.Id} killing sucessful! {DateTime.Now}");
                Console.WriteLine($"Total killed process count: {_count}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
