/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Reflection.Emit;

namespace Liersch.Reflection
{
  static class InternalExtensions
  {
    public static void EmitLdarg(this ILGenerator generator, int index)
    {
      switch(index)
      {
        case 0: generator.Emit(OpCodes.Ldarg_0); break;
        case 1: generator.Emit(OpCodes.Ldarg_1); break;
        case 2: generator.Emit(OpCodes.Ldarg_2); break;
        case 3: generator.Emit(OpCodes.Ldarg_3); break;
        default: generator.Emit(OpCodes.Ldarg, index); break;
      }
    }

    public static void EmitLdc_I4(this ILGenerator generator, int index)
    {
      switch(index)
      {
        case 0: generator.Emit(OpCodes.Ldc_I4_0); break;
        case 1: generator.Emit(OpCodes.Ldc_I4_1); break;
        case 2: generator.Emit(OpCodes.Ldc_I4_2); break;
        case 3: generator.Emit(OpCodes.Ldc_I4_3); break;
        case 4: generator.Emit(OpCodes.Ldc_I4_4); break;
        case 5: generator.Emit(OpCodes.Ldc_I4_5); break;
        case 6: generator.Emit(OpCodes.Ldc_I4_6); break;
        case 7: generator.Emit(OpCodes.Ldc_I4_7); break;
        case 8: generator.Emit(OpCodes.Ldc_I4_8); break;
        default: generator.Emit(OpCodes.Ldc_I4, index); break;
      }
    }
  }
}