using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectLoader <TKey, TObj>
{
    List<TObj> SwappableObjects { get; }

    void AddObject(TObj obj);

    void LoadObject(TKey name);

    void SwapObject(TKey name);
}
