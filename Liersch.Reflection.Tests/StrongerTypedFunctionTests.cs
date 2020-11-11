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
  public sealed class StrongerTypedFunctionTests
  {
    [TestMethod]
    public void TestCreateFunction1a()
    {
      string n=nameof(ExampleClass1.IncrementAndGet);
      MethodInfo m=typeof(ExampleClass1).GetMethod(n, Type.EmptyTypes);
      Func1 incAndGet=Accelerator.CreateFunction1(m);

      var o=new ExampleClass1();
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(1, o.IncrementAndGet());
      Assert.AreEqual(1, o.Value);
      Assert.AreEqual(2, (int)incAndGet(o));
      Assert.AreEqual(2, o.Value);
      Assert.AreEqual(3, (int)incAndGet(o));
      Assert.AreEqual(4, (int)incAndGet(o));
      Assert.AreEqual(4, o.Value);
    }

    [TestMethod]
    public void TestCreateFunction1b()
    {
      string n=nameof(ExampleStruct1.IncrementAndGet);
      MethodInfo m=typeof(ExampleStruct1).GetMethod(n, Type.EmptyTypes);
      Func1 incAndGet=Accelerator.CreateFunction1(m);

      var unboxed=new ExampleStruct1();
      Assert.AreEqual(0, unboxed.Value);
      Assert.AreEqual(1, unboxed.IncrementAndGet());
      Assert.AreEqual(1, unboxed.Value);

      object boxed=unboxed;
      Assert.AreEqual(2, (int)incAndGet(boxed));
      Assert.AreEqual(2, ((ExampleStruct1)boxed).Value);
      Assert.AreEqual(3, (int)incAndGet(boxed));
      Assert.AreEqual(4, (int)incAndGet(boxed));
      Assert.AreEqual(4, ((ExampleStruct1)boxed).Value);
    }

    [TestMethod]
    public void TestCreateFunction1c()
    {
      string n=nameof(ExampleClass2.AppendTextAndGet);
      MethodInfo m=typeof(ExampleClass2).GetMethod(n, Type.EmptyTypes);
      Func1 appendAndGet=Accelerator.CreateFunction1(m);

      var o=new ExampleClass2();
      Assert.AreEqual(null, o.Text);
      o.Text="ABC";
      Assert.AreEqual("ABCtext", o.AppendTextAndGet());
      Assert.AreEqual("ABCtext", o.Text);
      Assert.AreEqual("ABCtexttext", appendAndGet(o));
      Assert.AreEqual("ABCtexttext", o.Text);
    }

    [TestMethod]
    public void TestCreateFunction1d()
    {
      string n=nameof(ExampleStruct2.AppendTextAndGet);
      MethodInfo m=typeof(ExampleStruct2).GetMethod(n, Type.EmptyTypes);
      Func1 appendAndGet=Accelerator.CreateFunction1(m);

      var unboxed=new ExampleStruct2();
      Assert.AreEqual(null, unboxed.Text);
      unboxed.Text="ABC";
      Assert.AreEqual("ABCtext", unboxed.AppendTextAndGet());
      Assert.AreEqual("ABCtext", unboxed.Text);

      object boxed=unboxed;
      Assert.AreEqual("ABCtexttext", appendAndGet(boxed));
      Assert.AreEqual("ABCtexttext", ((ExampleStruct2)boxed).Text);
    }


    [TestMethod]
    public void TestCreateFunction2a()
    {
      string n=nameof(ExampleClass2.IncrementAndGet);
      MethodInfo m=typeof(ExampleClass2).GetMethod(n, new Type[] {typeof(int)});
      Func2 incAndGet=Accelerator.CreateFunction2(m);

      var o=new ExampleClass2();
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(1, o.IncrementAndGet(1));
      Assert.AreEqual(1, o.Value);
      Assert.AreEqual(3, (int)incAndGet(o, 2));
      Assert.AreEqual(3, o.Value);
      Assert.AreEqual(6, (int)incAndGet(o, 3));
      Assert.AreEqual(6, o.Value);
    }

    [TestMethod]
    public void TestCreateFunction2b()
    {
      string n=nameof(ExampleStruct2.IncrementAndGet);
      MethodInfo m=typeof(ExampleStruct2).GetMethod(n, new Type[] {typeof(int)});
      Func2 incAndGet=Accelerator.CreateFunction2(m);

      var unboxed=new ExampleStruct2();
      Assert.AreEqual(0, unboxed.Value);
      Assert.AreEqual(1, unboxed.IncrementAndGet(1));
      Assert.AreEqual(1, unboxed.Value);

      object boxed=unboxed;
      Assert.AreEqual(3, (int)incAndGet(boxed, 2));
      Assert.AreEqual(3, ((ExampleStruct2)boxed).Value);
      Assert.AreEqual(6, (int)incAndGet(boxed, 3));
      Assert.AreEqual(6, ((ExampleStruct2)boxed).Value);
    }

    [TestMethod]
    public void TestCreateFunction2c()
    {
      string n=nameof(ExampleClass1.AppendTextAndGet);
      MethodInfo m=typeof(ExampleClass1).GetMethod(n, new Type[] {typeof(string)});
      Func2 appendAndGet=Accelerator.CreateFunction2(m);

      var o=new ExampleClass1();
      Assert.AreEqual("ABC", o.AppendTextAndGet("ABC"));
      Assert.AreEqual("ABC", o.Text);
      Assert.AreEqual("ABCXYZ", (string)appendAndGet(o, "XYZ"));
      Assert.AreEqual("ABCXYZ", o.Text);
    }

    [TestMethod]
    public void TestCreateFunction2d()
    {
      string n=nameof(ExampleStruct1.AppendTextAndGet);
      MethodInfo m=typeof(ExampleStruct1).GetMethod(n, new Type[] {typeof(string)});
      Func2 appendAndGet=Accelerator.CreateFunction2(m);

      var unboxed=new ExampleStruct1();
      Assert.AreEqual("ABC", unboxed.AppendTextAndGet("ABC"));
      Assert.AreEqual("ABC", unboxed.Text);

      object boxed=unboxed;
      Assert.AreEqual("ABCXYZ", (string)appendAndGet(boxed, "XYZ"));
      Assert.AreEqual("ABCXYZ", ((ExampleStruct1)boxed).Text);
    }


    class ExampleClass1
    {
      public string Text { get; set; }

      public int Value { get; set; }


      public int IncrementAndGet() { return ++Value; }

      public int IncrementAndGet(int offset) { return Value+=offset; }


      public string AppendTextAndGet() { return Text+="text"; }

      public string AppendTextAndGet(string text) { return Text+=text; }
    }

    class ExampleClass2 : IExample
    {
      public string Text { get; set; }

      public int Value { get; set; }


      public int IncrementAndGet() { return ++Value; }

      public int IncrementAndGet(int offset) { return Value+=offset; }


      public string AppendTextAndGet() { return Text+="text"; }

      public string AppendTextAndGet(string text) { return Text+=text; }
    }

    struct ExampleStruct1
    {
      public string Text { get; set; }

      public int Value { get; set; }


      public int IncrementAndGet() { return ++Value; }

      public int IncrementAndGet(int offset) { return Value+=offset; }


      public string AppendTextAndGet() { return Text+="text"; }

      public string AppendTextAndGet(string text) { return Text+=text; }
    }

    struct ExampleStruct2 : IExample
    {
      public string Text { get; set; }

      public int Value { get; set; }


      public int IncrementAndGet() { return ++Value; }

      public int IncrementAndGet(int offset) { return Value+=offset; }


      public string AppendTextAndGet() { return Text+="text"; }

      public string AppendTextAndGet(string text) { return Text+=text; }
    }

    interface IExample
    {
      string Text { get; set; }

      int Value { get; set; }


      int IncrementAndGet();

      int IncrementAndGet(int offset);


      string AppendTextAndGet();

      string AppendTextAndGet(string text);
    }
  }
}