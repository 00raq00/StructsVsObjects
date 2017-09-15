﻿using ClasssVsObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructsVsObjects
{
  static class Program
  {
    static void Main(string[] args)
    {
      unchecked
      {
        int readedValue = int.MaxValue / 300;

        if (args[0].Equals("Class"))
        {

          GC.Collect(GC.MaxGeneration);
          GC.WaitForFullGCComplete();


          Console.WriteLine(ClassStatic.SampleClass(readedValue, 30));

        }
        else if (args[0].Equals("Struct"))
        {

          // Console.ReadKey();
          GC.Collect(GC.MaxGeneration);
          GC.WaitForFullGCComplete();

          Console.WriteLine(StructStatic.SampleStruct(readedValue, 30));
        }
        
      }
    }
  }
}