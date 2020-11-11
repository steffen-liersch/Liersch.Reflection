/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Reflection;

namespace Liersch.Reflection
{
  public static class AcceleratorExtensions
  {
    public static InvocationDelegate CreateInvocationDelegate(this Type type, string method)
    {
      return CreateInvocationDelegate(type, method, null);
    }

    public static InvocationDelegate CreateInvocationDelegate(this Type type, string method, Type[] types)
    {
      return Accelerator.CreateInvocationDelegate(
        type.GetMethod(
          method,
          BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
          null,
          types ?? Type.EmptyTypes,
          null));
    }

    public static Func0 CreateStaticGetter(this Type type, string field)
    {
      return Accelerator.CreateStaticGetter(type.GetField(field, BindingFlags.Public | BindingFlags.Static));
    }

    public static Func1 CreateInstanceGetter(this Type type, string field)
    {
      return Accelerator.CreateInstanceGetter(type.GetField(field, BindingFlags.Public | BindingFlags.Instance));
    }

    public static Action1 CreateStaticSetter(this Type type, string field)
    {
      return Accelerator.CreateStaticSetter(type.GetField(field, BindingFlags.Public | BindingFlags.Static));
    }

    public static Action2 CreateInstanceSetter(this Type type, string field)
    {
      return Accelerator.CreateInstanceSetter(type.GetField(field, BindingFlags.Public | BindingFlags.Instance));
    }
  }
}