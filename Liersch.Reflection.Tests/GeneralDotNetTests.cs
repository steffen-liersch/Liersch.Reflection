/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Reflection.Tests
{
  [TestClass]
  public sealed class GeneralDotNetTests
  {
    [TestMethod]
    public void TestCastedValueTypesAreBoxed()
    {
      var e1=new ExampleStruct1();
      var e2=(ValueType)e1;
      e1.Value++;
      Assert.AreEqual(1, e1.Value);
      Assert.AreEqual(0, ((ExampleStruct1)e2).Value);
    }

    [TestMethod]
    public void TestInterfacedValueTypesAreBoxed()
    {
      var e1=new ExampleStruct2();
      IExample e2=e1;
      e1.Value++;
      Assert.AreEqual(1, e1.Value);
      e1.Value++;
      Assert.AreEqual(0, e2.Value);
    }


    struct ExampleStruct1
    {
      public int Value { get; set; }
    }

    struct ExampleStruct2 : IExample
    {
      public int Value { get; set; }
    }

    interface IExample
    {
      int Value { get; set; }
    }
  }
}