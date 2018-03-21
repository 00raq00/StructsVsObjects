using StructsVsObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasssVsObjects
{
  public static class ClassLargeStatic
  {
    static bool logMemory = false;
    static bool logTimeInternal = false;
    public static int SampleClass(int readedValue)
    {
      int ss = 0;
      Process currentProc;
      long memory, memory2;
      unchecked
      {
        currentProc = Process.GetCurrentProcess();
        memory = GC.GetTotalMemory(true);
        memory2 = currentProc.WorkingSet64;
        if (logMemory)
        {
          Console.WriteLine($"memory before {memory}");
          Console.WriteLine($"memory before {memory2}");
        }

        {
          SampleLargeClass[] sampleClass = new SampleLargeClass[readedValue];
          for (int i = 0; i < readedValue; i++)
          {
            sampleClass[i] = new SampleLargeClass(i, i * 2, i * 3);

          }


          int[] tmp = new int[readedValue];

          var sw = Stopwatch.StartNew();

          for (int i = 0; i < readedValue; i++)
          {
            tmp[i] = AddClass(sampleClass[i]);
          }


          for (int i = 0; i < tmp.Length; i++)
            ss += tmp[i];
          if (logTimeInternal)
          {
            Console.WriteLine($"{ss}   {sw.Elapsed}");
          }

          Process currentProc2 = Process.GetCurrentProcess();
          long memory12 = GC.GetTotalMemory(true);
          long memory22 = currentProc2.WorkingSet64;
          if (logMemory)
          {
            Console.WriteLine($"memory in Class {memory12}");
            Console.WriteLine($"memory in Class {memory22}");
          }
        }

        currentProc = Process.GetCurrentProcess();
        memory = GC.GetTotalMemory(true);
        memory2 = currentProc.WorkingSet64;
        if (logMemory)
        {
          Console.WriteLine($"memory after Class {memory}");
          Console.WriteLine($"memory after Class {memory2}");
        }
      }
      return ss;
    }

    public static int SampleClass(int readedValue, int count, bool logMemoryInternal = false, bool logTimeInternal = false)
    {

     var currentProc = Process.GetCurrentProcess();
      currentProc.Refresh();
     var gcMemoryBefore = GC.GetTotalMemory(true);
     var memoryBefore = currentProc.WorkingSet64;


      logMemory = logMemoryInternal;
      ClassLargeStatic.logTimeInternal = logTimeInternal;
      var sw = Stopwatch.StartNew();
      int val = 0;
      for (int i = 0; i < count; i++)
      {
        val += SampleClass(readedValue);
      }
      Console.WriteLine($"SampleClass(int readedValue, int count)  {sw.Elapsed}");

      var currentProc1 = Process.GetCurrentProcess();
      currentProc1.Refresh();
      var gcMemoryAfter = GC.GetTotalMemory(true);
      var memoryAfter = currentProc1.WorkingSet64;

      Console.WriteLine($"SampleClass(int readedValue, int count) MEMORY {memoryAfter- memoryBefore}, GC MEMORY {gcMemoryAfter- gcMemoryBefore}");


      Console.WriteLine("  physical memory usage: {0}",
                        currentProc1.WorkingSet64);
      Console.WriteLine("  base priority: {0}",
          currentProc1.BasePriority);
      Console.WriteLine("  priority class: {0}",
          currentProc1.PriorityClass);
      Console.WriteLine("  user processor time: {0}",
          currentProc1.UserProcessorTime);
      Console.WriteLine("  privileged processor time: {0}",
          currentProc1.PrivilegedProcessorTime);
      Console.WriteLine("  total processor time: {0}",
          currentProc1.TotalProcessorTime);
      Console.WriteLine("  PagedSystemMemorySize64: {0}",
          currentProc1.PagedSystemMemorySize64);
      Console.WriteLine("  PagedMemorySize64: {0}",
         currentProc1.PagedMemorySize64);

      // Update the values for the overall peak memory statistics.
      Console.WriteLine("  PeakPagedMemorySize64: {0}", currentProc1.PeakPagedMemorySize64);
      Console.WriteLine("  PeakVirtualMemorySize64: {0}", currentProc1.PeakVirtualMemorySize64);
      Console.WriteLine("  PeakWorkingSet64: {0}", currentProc1.PeakWorkingSet64);

      return val;
    }


    public static int AddClass(SampleLargeClass cl)
    {
      if (cl.z < 100)
        return cl.i + cl.j + cl.z;
      else
      {
        return cl.i % 1000;
      }
    }
  }
}
