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
  public sealed class InstanceGetterAndSetterTest
  {
    [TestMethod]
    public void TestCreateInstanceGetter1()
    {
      string n=nameof(ClassWithInstanceFields.InstanceValueField);
      Func1 getValue=typeof(ClassWithInstanceFields).CreateInstanceGetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(0, e.InstanceValueField);
      e.InstanceValueField=123;
      Assert.AreEqual(123, getValue(e));
      e.InstanceValueField=0;
      Assert.AreEqual(0, getValue(e));
    }

    [TestMethod]
    public void TestCreateInstanceGetter2()
    {
      string n=nameof(ClassWithInstanceFields.InstanceTextField);
      Func1 getValue=typeof(ClassWithInstanceFields).CreateInstanceGetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(null, e.InstanceTextField);
      e.InstanceTextField="ABC";
      Assert.AreEqual("ABC", getValue(e));
      e.InstanceTextField=null;
      Assert.AreEqual(null, getValue(e));
    }

    [TestMethod]
    public void TestCreateInstanceGetter3()
    {
      string n=nameof(ClassWithInstanceFields.InstanceEnumField);
      Func1 getValue=typeof(ClassWithInstanceFields).CreateInstanceGetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(EnumForTest.None, e.InstanceEnumField);
      e.InstanceEnumField=EnumForTest.Example;
      Assert.AreEqual(EnumForTest.Example, getValue(e));
      e.InstanceEnumField=EnumForTest.None;
      Assert.AreEqual(EnumForTest.None, getValue(e));
    }

    [TestMethod]
    public void TestCreateInstanceSetter1()
    {
      string n=nameof(ClassWithInstanceFields.InstanceValueField);
      Action2 setValue=typeof(ClassWithInstanceFields).CreateInstanceSetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(0, e.InstanceValueField);
      setValue(e, 123);
      Assert.AreEqual(123, e.InstanceValueField);
      setValue(e, 0);
      Assert.AreEqual(0, e.InstanceValueField);
    }

    [TestMethod]
    public void TestCreateInstanceSetter2()
    {
      string n=nameof(ClassWithInstanceFields.InstanceTextField);
      Action2 setText=typeof(ClassWithInstanceFields).CreateInstanceSetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(null, e.InstanceTextField);
      setText(e, "ABC");
      Assert.AreEqual("ABC", e.InstanceTextField);
      setText(e, null);
      Assert.AreEqual(null, e.InstanceTextField);
    }

    [TestMethod]
    public void TestCreateInstanceSetter3()
    {
      string n=nameof(ClassWithInstanceFields.InstanceEnumField);
      Action2 setText=typeof(ClassWithInstanceFields).CreateInstanceSetter(n);

      var e=new ClassWithInstanceFields();
      Assert.AreEqual(EnumForTest.None, e.InstanceEnumField);
      setText(e, EnumForTest.Example);
      Assert.AreEqual(EnumForTest.Example, e.InstanceEnumField);
      setText(e, EnumForTest.None);
      Assert.AreEqual(EnumForTest.None, e.InstanceEnumField);
    }


    class ClassWithInstanceFields
    {
      public int InstanceValueField;
      public string InstanceTextField;
      public EnumForTest InstanceEnumField;
    }

    enum EnumForTest
    {
      None,
      Example=123,
    }
  }
}