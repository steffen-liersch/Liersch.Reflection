/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

#if NET20 || NET30

using System.Collections.Generic;

namespace System.Linq
{
  static class Enumerable
  {
    public static T[] ToArray<T>(this IEnumerable<T> source) { return new List<T>(source).ToArray(); }
  }
}

#endif