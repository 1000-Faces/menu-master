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
        
        List<FoodSO> m_FoodList;

        FoodListItem m_CurrentFoodListItem;

        FoodListItem m_NewFoodListItem;

        protected void Start()
        {
            // Load the food list from resources
            m_FoodList = Resources.LoadAll<FoodSO>("ScriptableObjects/Foods").ToList();

            // Verify the food list is not empty
            if (m_FoodList.Count == 0)
            {
                Debug.LogError("No food items found in the resources folder!");
                return;
            }
            
            // Create the list of food items
            foreach (var item in m_FoodList)
            {
                // Instantiate
                CreateFoodlistItem(item);
            }

            // Select the current food item if available
            if (m_CurrentFoodListItem != null)
            {
                m_CurrentFoodListItem.CheckBox.isOn = true;
            }

            // Destroy the template list item
            Destroy(m_FoodListItemTemplate);
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
                    m_NewFoodListItem = foodListItemComponent;
                }
            });
        }

        public override void OnSubmit()
        {
            // Change current food selection to the new one
            if (m_NewFoodListItem) m_CurrentFoodListItem = m_NewFoodListItem;

            // Change the food selection in the meal component
            m_MealComponent.OnSelectedFoodChange(m_CurrentFoodListItem.Food);

            base.OnSubmit();
        }
    }
}
