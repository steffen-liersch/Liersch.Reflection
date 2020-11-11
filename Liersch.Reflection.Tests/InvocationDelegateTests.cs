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
  public sealed class InvocationDelegateTests
  {
    [TestMethod]
    public void TestInvokeConstructor()
    {
      Type t=typeof(ClassWithConstructors);
      ConstructorInfo ci=t.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] {typeof(int), typeof(string)}, null);
      InvocationDelegate create=Accelerator.CreateInvocationDelegate(ci);
      Assert.IsNotNull(create);

      var o=new ClassWithConstructors(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);

      o=(ClassWithConstructors)create(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);

      o=(ClassWithConstructors)create(0, null);
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);
    }

    [TestMethod]
    public void TestInvokeFactoryFunction()
    {
      string n=nameof(ClassWithConstructors.Create);
      InvocationDelegate create=typeof(ClassWithConstructors).CreateInvocationDelegate(n, new[] {typeof(int), typeof(string)});

      var o=ClassWithConstructors.Create(0, null);
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);

      o=ClassWithConstructors.Create(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);

      o=(ClassWithConstructors)create(0, null);
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);

      o=(ClassWithConstructors)create(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);
    }

    [TestMethod]
    public void TestInvokeInstanceFunction1()
    {
      string n=nameof(ClassWithConstructors.Reset);
      InvocationDelegate reset=typeof(ClassWithConstructors).CreateInvocationDelegate(n);

      var o=new ClassWithConstructors();

      o.Update(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);
      o.Reset();
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);

      o.Update(123, "ABC");
      Assert.IsNull(reset(o));
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);
    }

    [TestMethod]
    public void TestInvokeInstanceFunction2()
    {
      string n=nameof(ClassWithConstructors.Update);
      InvocationDelegate update=typeof(ClassWithConstructors).CreateInvocationDelegate(n, new[] {typeof(int), typeof(string)});

      var o=new ClassWithConstructors();
      Assert.AreEqual(0, o.Value);
      Assert.AreEqual(null, o.Text);

      o.Update(123, "ABC");
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual("ABC", o.Text);

      update(o, 456, "XYZ");
      Assert.AreEqual(456, o.Value);
      Assert.AreEqual("XYZ", o.Text);
    }


    class ClassWithConstructors
    {
      public int Value { get; private set; }

      public string Text { get; private set; }

      public ClassWithConstructors() { }

      public ClassWithConstructors(int value, string text) { Value=value; Text=text; }

      public static ClassWithConstructors Create(int value, string text) { return new ClassWithConstructors(value, text); }

      public void Update(int value, string text) { Value=value; Text=text; }

      public void Reset() { Value=0; Text=null; }
    }
  }
}