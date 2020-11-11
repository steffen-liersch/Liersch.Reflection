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
  static class TestEnvironment
  {
    public static readonly Example[] Examples=new ExampleGenerator().CreateExamples(10);

    public static Func<Action[], MeasuringData[]> MeasureExternal { get; set; }

    public static MeasuringData[] Measure(params Action[] actions) { return MeasureExternal(actions); }
  }
}