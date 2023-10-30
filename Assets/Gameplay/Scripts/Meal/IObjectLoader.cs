using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DineEase.Meal
{
    public interface IObjectLoader<T>
    {
        void LoadObject(T obj);

        void SwapObject(T obj);
    }
}
