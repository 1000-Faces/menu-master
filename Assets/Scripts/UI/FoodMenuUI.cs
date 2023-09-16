using DineEase.Meal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class FoodMenuUI : ARAnnotationWindow
    {
        const string TEXT_DEFAULT = "N/A";

        [SerializeField] GameObject m_FoodListItemPrefab;
        [SerializeField] ToggleGroup m_ToggleGroup;
        [SerializeField] FoodSO m_TestFood;

        FoodListItem m_CurrentFoodListItem;

        FoodListItem m_NewFoodItem;

        protected override void Start()
        {
            base.Start();

            for (int i = 0; i < 5; i++)
            {
                // FoodSO food = FoodManager.Instance.FoodList[i];

                GameObject foodListItem = Instantiate(m_FoodListItemPrefab, m_FoodListItemPrefab.transform.parent);
                foodListItem.name = "Pizza" + i;

                FoodListItem foodListItemComponent = foodListItem.GetComponent<FoodListItem>();
                foodListItemComponent.Food = m_TestFood;

                Toggle toggle = foodListItem.GetComponent<Toggle>();
                toggle.group = m_ToggleGroup;

                toggle.onValueChanged.AddListener((bool value) =>
                {
                    if (value)
                    {
                        m_CurrentFoodListItem = foodListItemComponent;
                    }
                });
            }

            // Destroy the template list item
            Destroy(m_FoodListItemPrefab);
        }

        public override void OnSubmit()
        {
            throw new System.NotImplementedException();
        }
    }
}
