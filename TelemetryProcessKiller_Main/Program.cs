using Business;
using Models;

// Create list of processes for killing
List<ProcessModel> killingProcessList = new()
{
     new ProcessModel("CompatTelRunner"),
     new ProcessModel("sqlceip")
};

// Create monitor
ProcessMonitor monitor = new(killingProcessList);

Console.WriteLine($"Loaded {killingProcessList.Count} process(es) for monitoring and killing. {DateTime.UtcNow}");

// Start processes monitoring
await monitor.StartMonitoring();
