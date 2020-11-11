/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;

namespace Liersch.Reflection.Demo
{
  sealed class ExampleGenerator
  {
    public Example[] CreateExamples(int count)
    {
      var res=new Example[count];
      for(int i = 0; i<count; i++)
        res[i]=CreateExample();
      return res;
    }

    public Example CreateExample()
    {
      var res=new Example();
      res.DirectValue1=m_Random.Next(1000000);
      res.DirectValue2=m_Random.Next(1000000);
      res.PropertyValue1=m_Random.Next(1000000);
      res.PropertyValue2=m_Random.Next(1000000);
      return res;
    }

    readonly Random m_Random=new Random(Guid.NewGuid().GetHashCode());
  }
}