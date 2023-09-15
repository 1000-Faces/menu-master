using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DineEase.Meal
{
    [CreateAssetMenu(fileName = "NewFood", menuName = "ScriptableObjects/Food")]
    public class FoodSO : ScriptableObject
    {
        public bool requirePlatform = false;

        public Transform prefab;

        public Sprite foodIcon;

        public string foodName;
    }
}
