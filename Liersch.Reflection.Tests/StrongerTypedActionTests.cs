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
  public sealed class StrongerTypedActionTests
  {
    [TestMethod]
    public void TestCreateAction1a()
    {
      string n=nameof(ExampleClass1.Increment);
      Action1 inc=Accelerator.CreateAction1(typeof(ExampleClass1).GetMethod(n, Type.EmptyTypes));

      var o=new ExampleClass1();
      o.Increment();
      Assert.AreEqual(1, o.Value);
      inc(o);
      Assert.AreEqual(2, o.Value);
      inc(o);
      inc(o);
      Assert.AreEqual(4, o.Value);
    }

    [TestMethod]
    public void TestCreateAction1b()
    {
      string n=nameof(ExampleClass2.Increment);
      Action1 inc=Accelerator.CreateAction1(typeof(ExampleClass2).GetMethod(n, Type.EmptyTypes));

      var o=new ExampleClass2();
      o.Increment();
      Assert.AreEqual(1, o.Value);
      inc(o);
      Assert.AreEqual(2, o.Value);
      inc(o);
      inc(o);
      Assert.AreEqual(4, o.Value);
    }

    [TestMethod]
    public void TestCreateAction1c()
    {
      string n=nameof(ExampleStruct1.Increment);
      Action1 inc=Accelerator.CreateAction1(typeof(ExampleStruct1).GetMethod(n, Type.EmptyTypes));

      var unboxed=new ExampleStruct1();
      unboxed.Increment();
      Assert.AreEqual(1, unboxed.Value);

      object boxed=unboxed;
      inc(boxed);
      Assert.AreEqual(2, ((ExampleStruct1)boxed).Value);
      inc(boxed);
      inc(boxed);
      Assert.AreEqual(4, ((ExampleStruct1)boxed).Value);
    }

    [TestMethod]
    public void TestCreateAction1d()
    {
      string n=nameof(ExampleStruct2.Increment);
      Action1 inc=Accelerator.CreateAction1(typeof(ExampleStruct2).GetMethod(n, Type.EmptyTypes));

      var unboxed=new ExampleStruct2();
      unboxed.Increment();
      Assert.AreEqual(1, unboxed.Value);

      IExample boxed=unboxed;
      inc(boxed);
      Assert.AreEqual(2, boxed.Value);
      inc(boxed);
      inc(boxed);
      Assert.AreEqual(4, boxed.Value);
    }


    [TestMethod]
    public void TestCreateAction2a()
    {
      string n=nameof(ExampleClass1.Increment);
      Action2 inc=Accelerator.CreateAction2(typeof(ExampleClass1).GetMethod(n, new[] {typeof(int)}));

      var o=new ExampleClass1();
      o.Increment(1);
      Assert.AreEqual(1, o.Value);
      inc(o, 3);
      Assert.AreEqual(4, o.Value);
      inc(o, 11);
      Assert.AreEqual(15, o.Value);
    }

    [TestMethod]
    public void TestCreateAction2b()
    {
      string n=nameof(ExampleClass2.Increment);
      Action2 inc=Accelerator.CreateAction2(typeof(ExampleClass2).GetMethod(n, new[] {typeof(int)}));

      var o=new ExampleClass2();
      o.Increment(1);
      Assert.AreEqual(1, o.Value);
      inc(o, 3);
      Assert.AreEqual(4, o.Value);
      inc(o, 11);
      Assert.AreEqual(15, o.Value);
    }

    [TestMethod]
    public void TestCreateAction2c()
    {
      string n=nameof(ExampleStruct1.Increment);
      Action2 inc=Accelerator.CreateAction2(typeof(ExampleStruct1).GetMethod(n, new[] {typeof(int)}));

      var unboxed=new ExampleStruct1();
      unboxed.Increment(1);
      Assert.AreEqual(1, unboxed.Value);

      object boxed=unboxed;
      inc(boxed, 3);
      Assert.AreEqual(4, ((ExampleStruct1)boxed).Value);
      inc(boxed, 11);
      Assert.AreEqual(15, ((ExampleStruct1)boxed).Value);
    }

    [TestMethod]
    public void TestCreateAction2d()
    {
      string n=nameof(ExampleStruct2.Increment);
      Action2 inc=Accelerator.CreateAction2(typeof(ExampleStruct2).GetMethod(n, new[] {typeof(int)}));

      var unboxed=new ExampleStruct2();
      unboxed.Increment(1);
      Assert.AreEqual(1, unboxed.Value);

      IExample boxed=unboxed;
      inc(boxed, 3);
      Assert.AreEqual(4, boxed.Value);
      inc(boxed, 11);
      Assert.AreEqual(15, boxed.Value);
    }


    class ExampleClass1
    {
      public int Value { get; set; }

      public void Increment() { ++Value; }

      public void Increment(int offset) { Value+=offset; }
    }

    class ExampleClass2 : IExample
    {
      public int Value { get; set; }

      public void Increment() { ++Value; }

      public void Increment(int offset) { Value+=offset; }
    }

    struct ExampleStruct1
    {
      public int Value { get; set; }

      public void Increment() { ++Value; }

      public void Increment(int offset) { Value+=offset; }
    }

    struct ExampleStruct2 : IExample
    {
      public int Value { get; set; }

      public void Increment() { ++Value; }

      public void Increment(int offset) { Value+=offset; }
    }

    interface IExample
    {
      int Value { get; set; }

      void Increment();

      void Increment(int offset);
    }
  }
}