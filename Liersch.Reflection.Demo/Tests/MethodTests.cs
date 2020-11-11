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
  sealed class MethodTests
  {
    public MethodTests()
    {
      MethodInfo mi=typeof(string).GetMethod("IndexOf", new[] {typeof(string)});
      m_IndexOf1=Accelerator.CreateFunction2(mi);
      m_IndexOf2=Accelerator.CreateInvocationDelegate(mi);
    }


    [Measuring(401, "Method tests - String.IndexOf vs. Accelerator.CreateFunction2")]
    public MeasuringData[] RunTest1() { return TestEnvironment.Measure(TestIndexOfDirect, TestIndexOf1); }

    [Measuring(402, "Method tests - String.IndexOf vs. Accelerator.CreateInvocationDelegate")]
    public MeasuringData[] RunTest2() { return TestEnvironment.Measure(TestIndexOfDirect, TestIndexOf2); }

    [Measuring(403, "Method tests - Accelerator.CreateFunction2 vs. CreateInvocationDelegate")]
    public MeasuringData[] RunTest3() { return TestEnvironment.Measure(TestIndexOf1, TestIndexOf2); }


    void TestIndexOfDirect()
    {
      int c=m_Examples.Length;
      for(int z = 0; z<c_RepetitionCount; z++)
        for(int i = 0; i<c; i++)
          for(int j = 0; j<c; j++)
          {
            int r=m_Examples[i].IndexOf(m_Examples[j]);
            CheckResult(i, j, r);
          }
    }

    void TestIndexOf1()
    {
      int c=m_Examples.Length;
      for(int z = 0; z<c_RepetitionCount; z++)
        for(int i = 0; i<c; i++)
          for(int j = 0; j<c; j++)
          {
            int r=(int)m_IndexOf1(m_Examples[i], m_Examples[j]);
            CheckResult(i, j, r);
          }
    }

    void TestIndexOf2()
    {
      int c=m_Examples.Length;
      for(int z = 0; z<c_RepetitionCount; z++)
        for(int i = 0; i<c; i++)
          for(int j = 0; j<c; j++)
          {
            int r=(int)m_IndexOf2(m_Examples[i], m_Examples[j]);
            CheckResult(i, j, r);
          }
    }

    static void CheckResult(int i, int j, int r)
    {
      int expected=i<j ? -1 : 0;
      if(r!=expected)
        throw new InvalidOperationException();
    }


    readonly Func2 m_IndexOf1;
    readonly InvocationDelegate m_IndexOf2;

    readonly string[] m_Examples={string.Empty, "abc", "abc def", "abc def ghi"};
    const int c_RepetitionCount=25;
  }
}