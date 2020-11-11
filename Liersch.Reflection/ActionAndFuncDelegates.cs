/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

namespace Liersch.Reflection
{
  public delegate void Action1(object arg);
  public delegate void Action2(object arg1, object arg2);
  public delegate object Func0();
  public delegate object Func1(object arg);
  public delegate object Func2(object arg1, object arg2);
}