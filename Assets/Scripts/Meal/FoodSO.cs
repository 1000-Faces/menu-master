using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DineEase.Meal
{
    [CreateAssetMenu(fileName = "NewFood", menuName = "ScriptableObjects/Food")]
    public class FoodSO : ScriptableObject
    {
        public string foodName;

        public string description;

        public bool isAvailable;

        public float price;

        public Sprite foodIcon;

        public Transform prefab;

        public bool requirePlatform = false;
    }
}
