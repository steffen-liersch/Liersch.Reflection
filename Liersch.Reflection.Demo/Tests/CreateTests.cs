/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using Liersch.Profiling;

namespace Liersch.Reflection.Demo
{
  static class CreateTests
  {
    [Measuring(201, "Create tests - Accelerator vs. reflection (int)")]
    public static MeasuringData[] CompareNewInt_AcceleratorReflection()
    {
      Type type=typeof(int);
      Func0 create=Accelerator.CreateStandardConstructor(type);

      Action a1=() =>
      {
        for(int z=0; z<c_RepetitionCount; z++)
          create();
      };

      Action a2=() =>
      {
        for(int z=0; z<c_RepetitionCount; z++)
          Activator.CreateInstance(type);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(202, "Create tests - Accelerator vs. reflection (Guid)")]
    public static MeasuringData[] CompareNewGuid_AcceleratorReflection()
    {
      Type type=typeof(Guid);
      Func0 create=Accelerator.CreateStandardConstructor(type);

      Action a1=() =>
      {
        for(int z=0; z<c_RepetitionCount; z++)
          create();
      };

      Action a2=() =>
      {
        for(int z=0; z<c_RepetitionCount; z++)
          Activator.CreateInstance(type);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    const int c_RepetitionCount=100;
  }
}