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
  static class BasicTests
  {
    [Measuring(101, "Basic test - field vs. property")]
    public static MeasuringData[] CompareFieldProperty()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      Action a1=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, examples[i].DirectValue1, examples[i+1].DirectValue2);
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, examples[i].PropertyValue1, examples[i+1].PropertyValue2);
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(102, "Basic test - auto-property vs. manual property")]
    public static MeasuringData[] CompareAutoManualProperty()
    {
      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      Action a1=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, examples[i].PropertyValue1, examples[i+1].PropertyValue1);
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, examples[i].PropertyValue2, examples[i+1].PropertyValue2);
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    [Measuring(103, "Basic test - typed delegate vs. untyped delegate")]
    public static MeasuringData[] CompareTypedVsUntypedDelegate()
    {
      Func<Example,int> getValue1=Example.GetDirectValue1_Typed;
      Func<object, object> getValue2=Example.GetDirectValue1_Untyped;

      Example[] examples=TestEnvironment.Examples;
      int m=examples.Length-1;

      Action a1=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, getValue1(examples[i]), getValue1(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      Action a2=() =>
      {
        int res=0;
        for(int z=0; z<c_RepetitionCount; z++)
          for(int i=0; i<m; i++)
            res=TestHelpers.Calculate(res, (int)getValue2(examples[i]), (int)getValue2(examples[i+1]));
        TestHelpers.SaveValue(res);
      };

      return TestEnvironment.Measure(a1, a2);
    }

    const int c_RepetitionCount=100;
  }
}