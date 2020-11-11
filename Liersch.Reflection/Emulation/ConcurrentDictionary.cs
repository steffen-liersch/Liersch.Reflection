/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

#if NET20 || NET30 || NET35

using System.Collections.Generic;

namespace System.Collections.Concurrent
{
  sealed class ConcurrentDictionary<TKey, TValue>
  {
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
    {
      lock(m_SyncRoot)
      {
        TValue v;
        if(!m_Dictionary.TryGetValue(key, out v))
        {
          v=factory(key);
          m_Dictionary.Add(key, v);
        }

        return v;
      }
    }

    readonly object m_SyncRoot=new object();
    readonly Dictionary<TKey, TValue> m_Dictionary=new Dictionary<TKey, TValue>();
  }
}

#endif