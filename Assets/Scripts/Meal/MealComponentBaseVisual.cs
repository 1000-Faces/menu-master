using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DineEase.Meal
{
    [Serializable]
    public class MealCategoryVisual
    {
        [SerializeField]
        [Tooltip("The food category (enum)")]
        MealCategory m_CategoryName;

        /// <summary>
        /// The food category (enum)
        /// </summary>
        public MealCategory CategoryName { get => m_CategoryName; set => m_CategoryName = value; }

        [SerializeField]
        [Tooltip("The gamobject of the category")]
        Transform m_Prefab;

        /// <summary>
        /// The gamobject of the category
        /// </summary>
        public Transform Prefab { get => m_Prefab; set => m_Prefab = value; }
    }

    public class MealComponentBaseVisual : MonoBehaviour, IObjectLoader<MealCategory>
    {
        private Transform _currentObject;
        [SerializeField] private List<MealCategoryVisual> m_SwappableObjects;

        public List<MealCategoryVisual> SwappableObjects => m_SwappableObjects;

        public void AddObject(MealCategoryVisual obj)
        {
            if (!m_SwappableObjects.Contains(obj))
            {
                m_SwappableObjects.Add(obj);
            }
        }

        public void LoadObject(MealCategory category)
        {
            try
            {
                // spawn the new object
                var obj = m_SwappableObjects.Where(obj => obj.CategoryName == category).FirstOrDefault();
                // Instantiate the object
                _currentObject = Instantiate(obj.Prefab, transform);
            }
            catch (Exception ex)
            {
                Debug.LogError("Meal Component not found. Error: " + ex);
            }
        }

        public void SwapObject(MealCategory category)
        {
            // If the object exists, remove it
            if (_currentObject != null)
            {
                Destroy(_currentObject.gameObject);
            }

            LoadObject(category);
        }

        public void ToggleVisibility(bool visible)
        {
            if (_currentObject != null)
            {
                _currentObject.gameObject.SetActive(visible);
            }
        }
    }
}


