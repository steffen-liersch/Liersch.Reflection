/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Liersch.Reflection
{
  static class InternalAccelerator
  {
    public static Func0 CreateStandardConstructor(ConstructorInfo constructor)
    {
      if(constructor.GetParameters().Length>0)
        throw new ArgumentException("Standard constructor expected", nameof(constructor));

      var m=new DynamicMethod("CreateObject", typeof(object), null, typeof(DynamicType), true);

      ILGenerator g=m.GetILGenerator();

      g.Emit(OpCodes.Newobj, constructor);

      Type rt=constructor.DeclaringType;
      if(rt.IsValueType)
        g.Emit(OpCodes.Box, rt);

      g.Emit(OpCodes.Ret);

      return (Func0)m.CreateDelegate(typeof(Func0));
    }

    public static Func0 CreateStandardConstructor(Type type)
    {
      if(type.IsValueType)
        return CreateValueConstructor(type);

      if(type.IsArray)
        return CreateArrayConstructor(type);

      return CreateStandardConstructor(Accelerator.RetrieveStandardConstructor(type));
    }

    static Func0 CreateValueConstructor(Type type)
    {
      var m=new DynamicMethod("CreateValue", typeof(object), null, typeof(DynamicType), true);

      ILGenerator g=m.GetILGenerator();

      g.DeclareLocal(type);
      g.Emit(OpCodes.Ldloca_S, 0);
      g.Emit(OpCodes.Initobj, type);
      g.Emit(OpCodes.Ldloc_0);

      g.Emit(OpCodes.Box, type);
      g.Emit(OpCodes.Ret);

      return (Func0)m.CreateDelegate(typeof(Func0));
    }

    static Func0 CreateArrayConstructor(Type type)
    {
      if(type.GetArrayRank()!=1)
        throw new NotSupportedException("Multi-dimensional arrays are not supported");

      var m=new DynamicMethod("CreateArray", typeof(object), null, typeof(DynamicType), true);

      ILGenerator g=m.GetILGenerator();
      g.Emit(OpCodes.Ldc_I4_0);
      g.Emit(OpCodes.Newarr, type.GetElementType());
      g.Emit(OpCodes.Ret);

      return (Func0)m.CreateDelegate(typeof(Func0));
    }


    public static Action1 CreateAction1(MethodInfo method)
    {
      DynamicMethod m=CreateDynamicMethod(method, false, 1);
      return (Action1)m.CreateDelegate(typeof(Action1));
    }

    public static Action2 CreateAction2(MethodInfo method)
    {
      DynamicMethod m=CreateDynamicMethod(method, false, 2);
      return (Action2)m.CreateDelegate(typeof(Action2));
    }


    public static Func1 CreateFunction1(MethodInfo method)
    {
      DynamicMethod m=CreateDynamicMethod(method, true, 1);
      return (Func1)m.CreateDelegate(typeof(Func1));
    }

    public static Func2 CreateFunction2(MethodInfo method)
    {
      DynamicMethod m=CreateDynamicMethod(method, true, 2);
      return (Func2)m.CreateDelegate(typeof(Func2));
    }


    public static InvocationDelegate CreateInvocationDelegate(ConstructorInfo constructor)
    {
      var m=new DynamicMethod("InvokeConstructor", typeof(object), c_TypesX, typeof(DynamicType), true);

      ILGenerator g=m.GetILGenerator();

      PushParameters(g, false, EnumerateParameterTypes(constructor));
      g.Emit(OpCodes.Newobj, constructor);

      Type rt=constructor.DeclaringType;
      if(rt.IsValueType)
        g.Emit(OpCodes.Box, rt);

      g.Emit(OpCodes.Ret);

      return (InvocationDelegate)m.CreateDelegate(typeof(InvocationDelegate));
    }

    public static InvocationDelegate CreateInvocationDelegate(MethodInfo method)
    {
      var m=new DynamicMethod("InvokeMethod", typeof(object), c_TypesX, typeof(DynamicType), true);

      ILGenerator g=m.GetILGenerator();

      PushParameters(g, !method.IsStatic, EnumerateParameterTypes(method));
      g.Emit(method.IsVirtual && !method.IsFinal ? OpCodes.Callvirt : OpCodes.Call, method);

      Type rt=method.ReturnType;
      if(rt==typeof(void))
        g.Emit(OpCodes.Ldnull);
      else if(rt.IsValueType)
        g.Emit(OpCodes.Box, rt);

      g.Emit(OpCodes.Ret);

      return (InvocationDelegate)m.CreateDelegate(typeof(InvocationDelegate));
    }


    static DynamicMethod CreateDynamicMethod(MethodInfo method, bool hasReturnValue, int parameterCount)
    {
      Type rt=method.ReturnType;
      bool hasRV=rt!=typeof(void);
      if(hasRV!=hasReturnValue)
        throw new ArgumentException(hasReturnValue
          ? "Method with return value expected"
          : "Method without return value expected");

      Type[] paramTypes=RetrieveParameterTypes(method, parameterCount);

      var res=new DynamicMethod("CallFunction",
        hasRV ? typeof(object) : null,
        CreateObjectTypes(parameterCount),
        typeof(DynamicType),
        true);

      ILGenerator g=res.GetILGenerator();

      for(int i = 0; i<parameterCount; i++)
      {
        Type pt=paramTypes[i];

        OpCode op;
        if(pt.IsValueType)
          op=i==0 && !method.IsStatic ? OpCodes.Unbox : OpCodes.Unbox_Any;
        else op=OpCodes.Castclass;

        g.EmitLdarg(i);
        g.Emit(op, pt);
      }

      g.Emit(method.IsVirtual && !method.IsFinal ? OpCodes.Callvirt : OpCodes.Call, method);

      if(hasRV && rt.IsValueType)
        g.Emit(OpCodes.Box, rt);

      g.Emit(OpCodes.Ret);

      return res;
    }

    static void PushParameters(ILGenerator generator, bool hasThis, IEnumerable<Type> parameters)
    {
      int i=0;
      foreach(Type pt in parameters)
      {
        OpCode op;
        if(pt.IsValueType)
          op=i==0 && hasThis ? OpCodes.Unbox : OpCodes.Unbox_Any;
        else op=OpCodes.Castclass;

        generator.Emit(OpCodes.Ldarg_0);
        generator.EmitLdc_I4(i++);
        generator.Emit(OpCodes.Ldelem_Ref);
        generator.Emit(op, pt);
      }
    }

    static Type[] RetrieveParameterTypes(MethodBase method, int expectedParameterCount)
    {
      Type[] res=EnumerateParameterTypes(method).ToArray();
      if(res.Length!=expectedParameterCount)
        throw new ArgumentException(
          "Method with "+expectedParameterCount.ToString(CultureInfo.InvariantCulture)+
          " parameter(s) expected (including this for non-static methods)");

      return res;
    }

    static IEnumerable<Type> EnumerateParameterTypes(MethodBase method)
    {
      if(!method.IsStatic && !method.IsConstructor)
        yield return method.DeclaringType;

      foreach(ParameterInfo pi in method.GetParameters())
        yield return pi.ParameterType;
    }

    static Type[] CreateObjectTypes(int count)
    {
      switch(count)
      {
        case 0: return Type.EmptyTypes;
        case 1: return c_Types1;
        case 2: return c_Types2;
      }

      var res=new Type[count];
      for(int i = 0; i<count; i++)
        res[i]=typeof(object);

      return res;
    }


    public static Func0 CreateStaticGetter(FieldInfo field)
    {
      DynamicMethod m=CreateGetter(field, true);
      return (Func0)m.CreateDelegate(typeof(Func0));
    }

    public static Func1 CreateInstanceGetter(FieldInfo field)
    {
      DynamicMethod m=CreateGetter(field, false);
      return (Func1)m.CreateDelegate(typeof(Func1));
    }


    public static Action1 CreateStaticSetter(FieldInfo field)
    {
      DynamicMethod m=CreateSetter(field, true);
      return (Action1)m.CreateDelegate(typeof(Action1));
    }

    public static Action2 CreateInstanceSetter(FieldInfo field)
    {
      DynamicMethod m=CreateSetter(field, false);
      return (Action2)m.CreateDelegate(typeof(Action2));
    }


    static DynamicMethod CreateGetter(FieldInfo field, bool isStatic)
    {
      CheckIsStatic(field, isStatic);

      var res=new DynamicMethod("ReadValue",
        typeof(object),
        isStatic ? null : c_Types1,
        typeof(DynamicType),
        true);

      ILGenerator g=res.GetILGenerator();

      if(field.IsStatic)
        g.Emit(OpCodes.Ldsfld, field);
      else
      {
        g.Emit(OpCodes.Ldarg_0);

        Type dt=field.DeclaringType;
        g.Emit(dt.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, dt);

        g.Emit(OpCodes.Ldfld, field);
      }

      Type ft=field.FieldType;
      if(ft.IsValueType)
        g.Emit(OpCodes.Box, ft);

      g.Emit(OpCodes.Ret);

      return res;
    }

    static DynamicMethod CreateSetter(FieldInfo field, bool isStatic)
    {
      CheckIsStatic(field, isStatic);

      var res=new DynamicMethod("WriteValue",
        null,
        isStatic ? c_Types1 : c_Types2,
        typeof(DynamicType),
        true);

      ILGenerator g=res.GetILGenerator();

      g.Emit(OpCodes.Ldarg_0);

      if(!isStatic)
      {
        Type dt=field.DeclaringType;
        g.Emit(dt.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, dt);
        g.Emit(OpCodes.Ldarg_1);
      }

      Type ft=field.FieldType;
      g.Emit(ft.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, ft);

      g.Emit(isStatic ? OpCodes.Stsfld : OpCodes.Stfld, field);
      g.Emit(OpCodes.Ret);

      return res;
    }

    static void CheckIsStatic(FieldInfo field, bool isStatic)
    {
      if(field.IsStatic!=isStatic)
        throw new ArgumentException(isStatic
          ? "Static field expected"
          : "Instance field expected");
    }


    sealed class DynamicType
    {
    }


    static readonly Type[] c_Types1=new[] {typeof(object)};
    static readonly Type[] c_Types2=new[] {typeof(object), typeof(object)};
    static readonly Type[] c_TypesX=new[] {typeof(object[])};
  }
}