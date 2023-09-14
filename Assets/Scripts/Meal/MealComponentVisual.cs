using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DineEase.Meal
{
    [Serializable]
    public class MealCategory
    {
        [SerializeField]
        [Tooltip("The food category (enum)")]
        FoodCategory m_CategoryName;

        /// <summary>
        /// The food category (enum)
        /// </summary>
        public FoodCategory CategoryName { get => m_CategoryName; set => m_CategoryName = value; }

        [SerializeField]
        [Tooltip("The gamobject of the category")]
        Transform m_Prefab;

        /// <summary>
        /// The gamobject of the category
        /// </summary>
        public Transform Prefab { get => m_Prefab; set => m_Prefab = value; }
    }

    public class MealComponentVisual : MonoBehaviour, IObjectLoader<FoodCategory, MealCategory>
    {
        private Transform _currentObject;
        [SerializeField] private List<MealCategory> m_SwappableObjects;

        public List<MealCategory> SwappableObjects => m_SwappableObjects;

        public void AddObject(MealCategory obj)
        {
            if (!m_SwappableObjects.Contains(obj))
            {
                m_SwappableObjects.Add(obj);
            }
        }

        public void LoadObject(FoodCategory name)
        {
            try
            {
                // spawn the new object
                var obj = m_SwappableObjects.Where(obj => obj.CategoryName == name).FirstOrDefault();
                // Instantiate the object
                _currentObject = Instantiate(obj.Prefab, transform);
            }
            catch (Exception ex)
            {
                Debug.LogError("Meal Component not found. Error: " + ex);
            }
        }

        public void SwapObject(FoodCategory name)
        {
            // If the object exists, remove it
            if (_currentObject != null)
            {
                Destroy(_currentObject.gameObject);
            }

            LoadObject(name);
        }
    }
}


