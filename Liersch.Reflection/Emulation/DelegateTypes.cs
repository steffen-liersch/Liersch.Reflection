/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

#if NET20 || NET30

namespace System
{
  delegate TResult Func<in T, out TResult>(T arg);
}

#endif