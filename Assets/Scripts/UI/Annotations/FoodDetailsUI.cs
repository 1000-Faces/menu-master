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
        [SerializeField] TextMeshProUGUI m_PriceText;
        [SerializeField] FoodMenuUI m_FoodMenuUI;

        public FoodSO FoodSO { get; set; }

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

        public void Open(string title, FoodSO food)
        {
            // Set the title
            Title = title;

            if (food)
            {
                // Set details
                m_SelectionText.text = food.foodName;
                m_PriceText.text = food.price.ToString();
            }

            base.Open(title);
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
