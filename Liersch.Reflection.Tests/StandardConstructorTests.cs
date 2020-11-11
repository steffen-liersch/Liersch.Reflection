/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Reflection.Tests
{
  [TestClass]
  public sealed class StandardConstructorTests
  {
    [TestMethod]
    public void TestCreateConstructor1()
    {
      Func0 create=Accelerator.CreateStandardConstructor(typeof(int));
      Assert.IsNotNull(create);
      Assert.IsTrue(create() is int);
    }

    [TestMethod]
    public void TestCreateConstructor2()
    {
      Func0 create=Accelerator.CreateStandardConstructor(typeof(int[]));
      Assert.IsNotNull(create);
      int[] a=(int[])create();
      Assert.AreEqual(0, a.Length);
    }

    [TestMethod]
    public void TestCreateConstructor3()
    {
      Assert.IsNotNull(default(ExampleStruct1));
      Func0 create=Accelerator.CreateStandardConstructor(typeof(ExampleStruct1));
      Assert.IsNotNull(create);
      Assert.IsTrue(create() is ExampleStruct1);
    }

    [TestMethod]
    public void TestCreateConstructor4()
    {
      new ExampleClass1();
      Assert.IsNull(default(ExampleClass1));
      Func0 create=Accelerator.CreateStandardConstructor(typeof(ExampleClass1));
      Assert.IsNotNull(create);
      Assert.IsTrue(create() is ExampleClass1);
    }

    [TestMethod]
    public void TestCreateConstructor5()
    {
      new ExampleClass1();
      Assert.IsNull(default(ExampleClass1));
      ConstructorInfo ci=Accelerator.RetrieveStandardConstructor(typeof(ExampleClass1));
      InvocationDelegate create=Accelerator.CreateInvocationDelegate(ci);
      Assert.IsNotNull(create);
      Assert.IsTrue(create() is ExampleClass1);
    }


    class ExampleClass1
    {
    }

    struct ExampleStruct1
    {
    }
  }
}