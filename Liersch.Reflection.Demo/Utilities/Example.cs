/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

namespace Liersch.Reflection.Demo
{
  sealed class Example
  {
    public int DirectValue1;

    public int DirectValue2;


    public static object GetDirectValue1_Untyped(object instance) { return ((Example)instance).DirectValue1; }

    public static int GetDirectValue1_Typed(Example instance) { return instance.DirectValue1; }


    public int PropertyValue1 { get; set; }

    public int PropertyValue2 { get { return m_PropertyValue2; } set { m_PropertyValue2=value; } }

    int m_PropertyValue2;
  }
}