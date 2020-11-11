/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Liersch.Reflection
{
  public static class Accelerator
  {
    public static Func0 CreateStandardConstructor(ConstructorInfo constructor)
    {
      if(constructor==null)
        throw new ArgumentNullException(nameof(constructor));

      return m_Cache1b.GetOrAdd(constructor, x => InternalAccelerator.CreateStandardConstructor(constructor));
    }

    public static Func0 CreateStandardConstructor(Type type)
    {
      if(type==null)
        throw new ArgumentNullException(nameof(type));

      return m_Cache1a.GetOrAdd(type, x => InternalAccelerator.CreateStandardConstructor(type));
    }

    public static ConstructorInfo RetrieveStandardConstructor(Type type)
    {
      ConstructorInfo ci=type.GetConstructor(Type.EmptyTypes);
      if(ci==null)
        throw new InvalidOperationException("Missing standard constructor for type "+type.FullName);

      return ci;
    }


    public static Action1 CreateAction1(MethodInfo method)
    {
      if(method==null)
        throw new ArgumentNullException(nameof(method));

      return m_Cache2a.GetOrAdd(method, x => InternalAccelerator.CreateAction1(method));
    }

    public static Action2 CreateAction2(MethodInfo method)
    {
      if(method==null)
        throw new ArgumentNullException(nameof(method));

      return m_Cache2b.GetOrAdd(method, x => InternalAccelerator.CreateAction2(method));
    }


    public static Func1 CreateFunction1(MethodInfo method)
    {
      if(method==null)
        throw new ArgumentNullException(nameof(method));

      return m_Cache3a.GetOrAdd(method, x => InternalAccelerator.CreateFunction1(method));
    }

    public static Func2 CreateFunction2(MethodInfo method)
    {
      if(method==null)
        throw new ArgumentNullException(nameof(method));

      return m_Cache3b.GetOrAdd(method, x => InternalAccelerator.CreateFunction2(method));
    }


    public static InvocationDelegate CreateInvocationDelegate(ConstructorInfo constructor)
    {
      if(constructor==null)
        throw new ArgumentNullException(nameof(constructor));

      return m_Cache4a.GetOrAdd(constructor, x => InternalAccelerator.CreateInvocationDelegate(constructor));
    }

    public static InvocationDelegate CreateInvocationDelegate(MethodInfo method)
    {
      if(method==null)
        throw new ArgumentNullException(nameof(method));

      return m_Cache4b.GetOrAdd(method, x => InternalAccelerator.CreateInvocationDelegate(method));
    }


    public static Func0 CreateStaticGetter(FieldInfo field)
    {
      if(field==null)
        throw new ArgumentNullException(nameof(field));

      return m_Cache5a.GetOrAdd(field, x => InternalAccelerator.CreateStaticGetter(field));
    }

    public static Func1 CreateInstanceGetter(FieldInfo field)
    {
      if(field==null)
        throw new ArgumentNullException(nameof(field));

      return m_Cache5b.GetOrAdd(field, x => InternalAccelerator.CreateInstanceGetter(field));
    }


    public static Action1 CreateStaticSetter(FieldInfo field)
    {
      if(field==null)
        throw new ArgumentNullException(nameof(field));

      return m_Cache6a.GetOrAdd(field, x => InternalAccelerator.CreateStaticSetter(field));
    }

    public static Action2 CreateInstanceSetter(FieldInfo field)
    {
      if(field==null)
        throw new ArgumentNullException(nameof(field));

      return m_Cache6b.GetOrAdd(field, x => InternalAccelerator.CreateInstanceSetter(field));
    }


    static readonly ConcurrentDictionary<Type, Func0> m_Cache1a=new ConcurrentDictionary<Type, Func0>();
    static readonly ConcurrentDictionary<ConstructorInfo, Func0> m_Cache1b=new ConcurrentDictionary<ConstructorInfo, Func0>();
    static readonly ConcurrentDictionary<MethodInfo, Action1> m_Cache2a=new ConcurrentDictionary<MethodInfo, Action1>();
    static readonly ConcurrentDictionary<MethodInfo, Action2> m_Cache2b=new ConcurrentDictionary<MethodInfo, Action2>();
    static readonly ConcurrentDictionary<MethodInfo, Func1> m_Cache3a=new ConcurrentDictionary<MethodInfo, Func1>();
    static readonly ConcurrentDictionary<MethodInfo, Func2> m_Cache3b=new ConcurrentDictionary<MethodInfo, Func2>();
    static readonly ConcurrentDictionary<ConstructorInfo, InvocationDelegate> m_Cache4a=new ConcurrentDictionary<ConstructorInfo, InvocationDelegate>();
    static readonly ConcurrentDictionary<MethodInfo, InvocationDelegate> m_Cache4b=new ConcurrentDictionary<MethodInfo, InvocationDelegate>();
    static readonly ConcurrentDictionary<FieldInfo, Func0> m_Cache5a=new ConcurrentDictionary<FieldInfo, Func0>();
    static readonly ConcurrentDictionary<FieldInfo, Func1> m_Cache5b=new ConcurrentDictionary<FieldInfo, Func1>();
    static readonly ConcurrentDictionary<FieldInfo, Action1> m_Cache6a=new ConcurrentDictionary<FieldInfo, Action1>();
    static readonly ConcurrentDictionary<FieldInfo, Action2> m_Cache6b=new ConcurrentDictionary<FieldInfo, Action2>();
  }
}