/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Reflection.Tests
{
  [TestClass]
  public sealed class StaticGetterAndSetterTest
  {
    [TestMethod]
    public void TestCreateStaticGetter1()
    {
      string n=nameof(ClassWithStaticFields.StaticValueField);
      Func0 getValue=typeof(ClassWithStaticFields).CreateStaticGetter(n);
      ClassWithStaticFields.StaticValueField=123;
      Assert.AreEqual(123, getValue());
      ClassWithStaticFields.StaticValueField=0;
      Assert.AreEqual(0, getValue());
    }

    [TestMethod]
    public void TestCreateStaticGetter2()
    {
      string n=nameof(ClassWithStaticFields.StaticTextField);
      Func0 getValue=typeof(ClassWithStaticFields).CreateStaticGetter(n);
      ClassWithStaticFields.StaticTextField="ABC";
      Assert.AreEqual("ABC", getValue());
      ClassWithStaticFields.StaticTextField=null;
      Assert.AreEqual(null, getValue());
    }

    [TestMethod]
    public void TestCreateStaticGetter3()
    {
      string n=nameof(ClassWithStaticFields.StaticEnumField);
      Func0 getValue=typeof(ClassWithStaticFields).CreateStaticGetter(n);
      ClassWithStaticFields.StaticEnumField=EnumForTest.Example;
      Assert.AreEqual(EnumForTest.Example, getValue());
      ClassWithStaticFields.StaticEnumField=EnumForTest.None;
      Assert.AreEqual(EnumForTest.None, getValue());
    }

    [TestMethod]
    public void TestCreateStaticSetter1()
    {
      string n=nameof(ClassWithStaticFields.StaticValueField);
      Action1 setValue=typeof(ClassWithStaticFields).CreateStaticSetter(n);
      setValue(456);
      Assert.AreEqual(456, ClassWithStaticFields.StaticValueField);
      setValue(0);
      Assert.AreEqual(0, ClassWithStaticFields.StaticValueField);
    }

    [TestMethod]
    public void TestCreateStaticSetter2()
    {
      string n=nameof(ClassWithStaticFields.StaticTextField);
      Action1 setValue=typeof(ClassWithStaticFields).CreateStaticSetter(n);
      setValue("ABC");
      Assert.AreEqual("ABC", ClassWithStaticFields.StaticTextField);
      setValue(null);
      Assert.AreEqual(null, ClassWithStaticFields.StaticTextField);
    }

    [TestMethod]
    public void TestCreateStaticSetter3()
    {
      string n=nameof(ClassWithStaticFields.StaticEnumField);
      Action1 setValue=typeof(ClassWithStaticFields).CreateStaticSetter(n);
      setValue(EnumForTest.Example);
      Assert.AreEqual(EnumForTest.Example, ClassWithStaticFields.StaticEnumField);
      setValue(EnumForTest.None);
      Assert.AreEqual(EnumForTest.None, ClassWithStaticFields.StaticEnumField);
    }


    static class ClassWithStaticFields
    {
      public static int StaticValueField;
      public static string StaticTextField;
      public static EnumForTest StaticEnumField;
    }

    enum EnumForTest
    {
      None,
      Example=123,
    }
  }
}