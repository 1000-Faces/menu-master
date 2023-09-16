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

        FoodListItem m_CurrentFoodListItem;

        FoodListItem m_NewFoodItem;

        public override void OnSubmit()
        {
            throw new System.NotImplementedException();
        }
    }
}
