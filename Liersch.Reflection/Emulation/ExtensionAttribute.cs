/*--------------------------------------------------------------------------*\
::
::  Copyright © 2013-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

#if NET20 || NET30

namespace System.Runtime.CompilerServices
{
  /// <summary> This attribute enables extension methods in .NET 2.0 and 3.0. </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
  sealed class ExtensionAttribute : Attribute
  {
  }
}

#endif