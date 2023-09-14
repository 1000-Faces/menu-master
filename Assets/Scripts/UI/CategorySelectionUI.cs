using DineEase;
using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class CategorySelectionUI : MonoBehaviour
    {
        [SerializeField] Button m_MainCourse;
        [SerializeField] Button m_SideDish;
        [SerializeField] Button m_Beverage;
        [SerializeField] Button m_Dessert;

        void Start()
        {
            m_MainCourse.onClick.AddListener(() => OnCategorySelected(FoodCategory.MainCourse));
            m_SideDish.onClick.AddListener(() => OnCategorySelected(FoodCategory.SideDish));
            m_Beverage.onClick.AddListener(() => OnCategorySelected(FoodCategory.Beverage));
            m_Dessert.onClick.AddListener(() => OnCategorySelected(FoodCategory.Dessert));
        }

        void OnCategorySelected(FoodCategory category)
        {
            Utils.ShowToastMessage($"Category Selected: {category}");
        }

        public void OnSelected()
        {
            Utils.ShowToastMessage($"Category Selected");
        }
    }
}
