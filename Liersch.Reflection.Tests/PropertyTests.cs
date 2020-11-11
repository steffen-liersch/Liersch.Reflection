/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Reflection.Tests
{
  [TestClass]
  public sealed class PropertyTests
  {
    [TestMethod]
    public void TestAccessClassProperty()
    {
      string n=nameof(ClassWithProperties.Value);
      PropertyInfo pi=typeof(ClassWithProperties).GetProperty(n);
      Func1 getValue=Accelerator.CreateFunction1(pi.GetMethod);
      Action2 setValue=Accelerator.CreateAction2(pi.SetMethod);

      var o=new ClassWithProperties(123);
      Assert.AreEqual(123, o.Value);
      Assert.AreEqual(123, getValue(o));

      setValue(o, 456);
      Assert.AreEqual(456, o.Value);
      Assert.AreEqual(456, getValue(o));
    }

    [TestMethod]
    public void TestAccessValueProperty()
    {
      string n=nameof(ClassWithProperties.Text);
      PropertyInfo pi=typeof(ClassWithProperties).GetProperty(n);
      Func1 getText=Accelerator.CreateFunction1(pi.GetMethod);
      Action2 setText=Accelerator.CreateAction2(pi.SetMethod);

      var o=new ClassWithProperties("ABC");
      Assert.AreEqual("ABC", o.Text);
      Assert.AreEqual("ABC", getText(o));

      setText(o, "XYZ");
      Assert.AreEqual("XYZ", o.Text);
      Assert.AreEqual("XYZ", getText(o));
    }


    class ClassWithProperties
    {
      public int Value { get; private set; }

      public string Text { get; private set; }

      public ClassWithProperties(int value) { Value=value; }

      public ClassWithProperties(string text) { Text=text; }
    }
  }
}