using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace DineEase.Meal
{
    [CreateAssetMenu(fileName = "NewFood", menuName = "ScriptableObjects/Food")]
    public class FoodSO : ScriptableObject
    {
        public string guid;

        public string foodName;

        public string description;

        public MealCategory category;

        public float price;

        public Sprite foodIcon;

        public Transform prefab;

        public bool isAvailable;

        public bool requirePlatform = false;
    }
}
