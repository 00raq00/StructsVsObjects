using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
  class Program
  {

    static void Main(string[] args)
    {
      // Define variables to track the peak
      // memory usage of the process.
      memoryTester("StructsVsObjects.exe", "Class");
      memoryTester("StructsVsObjects.exe", "Struct");
      memoryTester("dotnet", "netcoreapp2.0\\StructsVsObjectsCore.dll Class");
      memoryTester("dotnet", "netcoreapp2.0\\StructsVsObjectsCore.dll Struct");

      Console.ReadKey();
    }

    private static void memoryTester(string FileName, string Arguments, bool logInProgress=false)
    {
      var sw = Stopwatch.StartNew();
      long peakPagedMem = 0, peakWorkingSet = 0, peakVirtualMem = 0;

      Process myProcess = null;

      try
      {
        // Start the process.
        myProcess = Process.Start(FileName, Arguments);

        // Display the process statistics until
        // the user closes the program.
        do
        {
          if (!myProcess.HasExited)
          {
            // Refresh the current process property values.
            myProcess.Refresh();
            if (logInProgress)
            {
              Console.WriteLine();

              // Display current process statistics.

              Console.WriteLine("{0} -", myProcess.ToString());
              Console.WriteLine("-------------------------------------");

              Console.WriteLine("  physical memory usage: {0}",
                  myProcess.WorkingSet64);
              Console.WriteLine("  base priority: {0}",
                  myProcess.BasePriority);
              Console.WriteLine("  priority class: {0}",
                  myProcess.PriorityClass);
              Console.WriteLine("  user processor time: {0}",
                  myProcess.UserProcessorTime);
              Console.WriteLine("  privileged processor time: {0}",
                  myProcess.PrivilegedProcessorTime);
              Console.WriteLine("  total processor time: {0}",
                  myProcess.TotalProcessorTime);
              Console.WriteLine("  PagedSystemMemorySize64: {0}",
                  myProcess.PagedSystemMemorySize64);
              Console.WriteLine("  PagedMemorySize64: {0}",
                 myProcess.PagedMemorySize64);
            }
            // Update the values for the overall peak memory statistics.
            peakPagedMem = myProcess.PeakPagedMemorySize64;
            peakVirtualMem = myProcess.PeakVirtualMemorySize64;
            peakWorkingSet = myProcess.PeakWorkingSet64;
if (logInProgress)
              {
            if (myProcess.Responding)
            {
              
                Console.WriteLine("Status = Running");
              
            }
            else
            {
              Console.WriteLine("Status = Not Responding");
            }}
          }
        }
        while (!myProcess.WaitForExit(1000));


        Console.WriteLine();
        Console.WriteLine($"Process FileName:{FileName} Arguments:{Arguments} exit code: {myProcess.ExitCode} TIME:{sw.Elapsed}"            );

        // Display peak memory statistics for the process.
        Console.WriteLine("Peak physical memory usage of the process: {0}",
            peakWorkingSet);
        Console.WriteLine("Peak paged memory usage of the process: {0}",
            peakPagedMem);
        Console.WriteLine("Peak virtual memory usage of the process: {0}",
            peakVirtualMem);

      }
      finally
      {
        if (myProcess != null)
        {
          myProcess.Close();
        }
      }
    }

  }
}
