using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class FoodSelectedEventArgs : EventArgs
    {
        public FoodSO NewFoodSelection { get; set; }
    }

    public class FoodMenuUI : ARAnnotationWindow
    {
        public event EventHandler<FoodSelectedEventArgs> OnFoodSelectedEvent;

        [SerializeField] GameObject m_FoodListItemTemplate;
        [SerializeField] ToggleGroup m_ToggleGroup;
        [SerializeField] List<FoodSO> m_FoodList;

        FoodListItem m_CurrentFoodListItem;

        FoodListItem m_NewFoodListItem;

        protected override void Start()
        {
            base.Start();

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
            m_CurrentFoodListItem = m_NewFoodListItem;

            // fire off the event to change the food visual
            OnFoodSelectedEvent?.Invoke(this, new FoodSelectedEventArgs { NewFoodSelection = m_CurrentFoodListItem.Food });

            Close();
        }
    }
}
