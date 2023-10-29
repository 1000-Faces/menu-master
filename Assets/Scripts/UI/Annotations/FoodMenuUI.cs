using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class FoodMenuUI : ARAnnotationWindow
    {
        [SerializeField] MealComponent m_MealComponent;
        [SerializeField] GameObject m_FoodListItemTemplate;
        [SerializeField] ToggleGroup m_ToggleGroup;
        [SerializeField] FoodDetailsUI m_FoodDetailsUI;
        
        List<FoodSO> m_FoodList;

        readonly List<FoodListItem> m_FoodListItemList;

        FoodListItem m_SelectedFoodListItem;

        protected void Start()
        {
            LoadFoodList();

            // Create the list of food items
            foreach (var item in m_FoodList)
            {
                // Instantiate
                CreateFoodlistItem(item);
            }

            // Destroy the template list item
            Destroy(m_FoodListItemTemplate);
        }

        void LoadFoodList()
        {
            // Load the food list from resources
            m_FoodList = Resources.LoadAll<FoodSO>("Foods/ScriptableObjects").ToList();

            // Verify the food list is not empty
            if (m_FoodList.Count == 0)
            {
                Debug.LogError("No food items found in the resources folder!");
                return;
            }
        }

        void CreateFoodlistItem(FoodSO food)
        {
            // Instantiate GameObject using the template
            GameObject foodListItem = Instantiate(m_FoodListItemTemplate, m_FoodListItemTemplate.transform.parent);
            foodListItem.name = "ListItem: " + food.foodName;

            // Instantiate FoodListItem Data
            FoodListItem foodListItemComponent = foodListItem.GetComponent<FoodListItem>();
            foodListItemComponent.Food = food;

            // Add event listner
            foodListItemComponent.CheckBox.onValueChanged.AddListener((bool value) =>
            {
                if (value)
                {
                    m_SelectedFoodListItem = foodListItemComponent;
                }
            });

            // push list item to the list
            m_FoodListItemList.Add(foodListItemComponent);
        }

        FoodListItem GetFoodListItem(FoodSO food)
        {
            if (food == null) return null;

            return m_FoodListItemList.Where(listItm => listItm.Food == food).FirstOrDefault();
        }

        public void Open(MealCategory category)
        {
            // Show the window
            base.Open($"Select food from {category} Category");
        }

        public void Open(FoodSO currentFood)
        {
            // Select the current food item if available
            m_SelectedFoodListItem = GetFoodListItem(currentFood);

            if (m_SelectedFoodListItem != null)
            {
                m_SelectedFoodListItem.CheckBox.isOn = true;
            }

            // Show the window
            base.Open($"Select food from {currentFood.category} Category");
        }

        public override void OnSubmit()
        {
            // Change the food selection in the meal component
            m_MealComponent.ChangeFood(m_SelectedFoodListItem.Food);

            // Open the Food Details UI if its closed
            if (!m_FoodDetailsUI.IsOpened)
            {
                m_FoodDetailsUI.Open(m_SelectedFoodListItem.Food);
            }

            base.OnSubmit();
        }
    }
}
