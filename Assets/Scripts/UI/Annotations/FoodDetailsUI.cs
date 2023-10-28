using DineEase.Meal;
using System;
using TMPro;
using UnityEngine;


namespace DineEase.UI
{
    public class FoodDetailsUI : ARAnnotationWindow
    {
        const string SELECTION_TEXT_DEFAULT = "Select a food";

        [SerializeField] TextMeshProUGUI m_SelectionText;
        [SerializeField] FoodMenuUI m_FoodMenuUI;

        protected override void Awake()
        {
            base.Awake();

            // set the default text
            Title = SELECTION_TEXT_DEFAULT;
        }

        protected void Start()
        {
            // subscribe to the FoodMenuUI response event
            OnFormResponseEvent += OnFoodMenuFormResponse;
        }

        private void OnFoodMenuFormResponse(object sender, FormResponse e)
        {
            // if the food menu is closed in success, There is no need to show this. The response is 0 (success)
            if (sender is FoodMenuUI && e.Response == 0 && IsOpened)
            {
                Close(0);
            }
        }

        public void LoadSelectedFood(FoodSO food)
        {
            m_SelectionText.text = food.foodName;
        }

        public override void Close(int state)
        {
            if (state != 0)
            {
                // Close the food menu if it is open
                m_FoodMenuUI.Close(state);
            }

            base.Close(state);
        }

        public override void OnSubmit()
        {
            m_FoodMenuUI.Open($"Select food from {Title} Category");

            base.OnSubmit();
        }
    }
}
