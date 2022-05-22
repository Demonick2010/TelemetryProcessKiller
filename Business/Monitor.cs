using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business
{
    public class ProcessMonitor
    {
        private readonly List<ProcessModel> _processesForCilling;
        public ProcessMonitor(List<ProcessModel> processes)
        {
            _processesForCilling = processes;
        }

        public async Task StartMonitoring()
        {
            // Interval for checking - 10 seconds
            const int CHEACKING_INTERVAL = 10000;
            int chekingCount = 1;

            Console.WriteLine($"Monitoring started! {DateTime.UtcNow}");

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

                chekingCount++;
                await Task.Delay(CHEACKING_INTERVAL);
            }
        }

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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Process {processForKill} with ID: {processForKill.Id} killing sucessful! {DateTime.UtcNow}");
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
