/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Reflection;
using Liersch.Profiling;

namespace Liersch.Reflection.Demo
{
  static class FieldTests
  {
    [Measuring(301, "Field test - field access vs. reflection")]
    public static MeasuringData[] CompareFieldAccess_DirectReflection()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      FieldInfo fi1=typeof(Example).GetField("DirectValue1");
      FieldInfo fi2=typeof(Example).GetField("DirectValue2");

      Action a1=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, examples[i].DirectValue1, examples[i+1].DirectValue2);
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)fi1.GetValue(examples[i]), (int)fi2.GetValue(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(302, "Field test - field access vs. Accelerator")]
    public static MeasuringData[] CompareFieldAccess_DirectAccelerator()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      FieldInfo fi1=typeof(Example).GetField("DirectValue1");
      FieldInfo fi2=typeof(Example).GetField("DirectValue2");

      Func1 getValue1=Accelerator.CreateInstanceGetter(fi1);
      Func1 getValue2=Accelerator.CreateInstanceGetter(fi2);

      Action a1=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, examples[i].DirectValue1, examples[i+1].DirectValue2);
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)getValue1(examples[i]), (int)getValue2(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(303, "Field test - Accelerator vs. reflection")]
    public static MeasuringData[] CompareFieldAccess_AcceleratorReflection()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      FieldInfo fi1=typeof(Example).GetField("DirectValue1");
      FieldInfo fi2=typeof(Example).GetField("DirectValue2");

      Func1 getValue1=Accelerator.CreateInstanceGetter(fi1);
      Func1 getValue2=Accelerator.CreateInstanceGetter(fi2);

      Action a1=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)getValue1(examples[i]), (int)getValue2(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)fi1.GetValue(examples[i]), (int)fi2.GetValue(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(304, "Field test - untyped delegate vs. Accelerator getter")]
    public static MeasuringData[] CompareFieldAcess_UntypedAccelerator()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      Func1 getValue1=Example.GetDirectValue1_Untyped;
      Func1 getValue2=typeof(Example).CreateInstanceGetter("DirectValue1");

      Action a1=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)getValue1(examples[i]), (int)getValue1(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int i=0; i<m; i++)
          for(int z=0; z<c_RepetitionCount; z++)
            res=TestHelpers.Calculate(res, (int)getValue2(examples[i]), (int)getValue2(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    const int c_RepetitionCount=100;
  }
}