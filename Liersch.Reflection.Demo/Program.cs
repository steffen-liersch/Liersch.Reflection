/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Diagnostics;
using Liersch.Profiling;

namespace Liersch.Reflection.Demo
{
  static class Program
  {
#if DEBUG
    const bool IsDebug=true;
#else
    const bool IsDebug=false;
#endif

    static void Main()
    {
      try
      {
        RunTests();
      }
      catch(Exception e)
      {
        Console.WriteLine(e.ToString());
      }

      Console.WriteLine();
      Console.WriteLine("[Press any key!]");
      Console.ReadKey(true);
    }

    static void RunTests()
    {
      Console.WriteLine("Debug version   : "+(IsDebug ? "yes" : "no"));
      Console.WriteLine("Debugger present: "+(Debugger.IsAttached ? "yes" : "no"));
      Console.WriteLine();

      Console.WriteLine("Performance based on Stopwatch");
      TestEnvironment.MeasureExternal=MeasuringTools.MeasurePerformance;
      MeasuringTools.RunTests(
        typeof(BasicTests),
        typeof(CreateTests),
        typeof(FieldTests),
        typeof(MethodTests)
      );
    }
  }
}