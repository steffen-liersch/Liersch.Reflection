/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Runtime.CompilerServices;

namespace Liersch.Reflection.Demo
{
  static class TestHelpers
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Calculate(int a, int b, int c) { return unchecked(a+b+c); }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int SaveValue(int value) { return value; }
  }
}