using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSwapper <TKey, TObj>
{
    List<TObj> SwappableObjects { get; }

    void AddObject(TObj obj);

    void SwapObject(TKey name);
}
