using DineEase.Meal;
using System;
using TMPro;
using UnityEngine;


namespace DineEase.UI
{
    public class FoodDetailsUI : ARAnnotationWindow
    {
        [SerializeField] TextMeshProUGUI m_SelectionText;
        [SerializeField] TextMeshProUGUI m_PriceText;
        [SerializeField] FoodMenuUI m_FoodMenuUI;

        private MealComponent m_MealComponent;

        public FoodSO Food { get; set; }

        protected void Start()
        {
            // get meal component object from the parent
            m_MealComponent = GetComponentInParent<MealComponent>();

            if (!m_MealComponent)
            {
                Debug.LogError("MealComponent not found in the parent object");
            }
        }

        private void OnEnable()
        {
            // Load the food details
            Load(m_MealComponent.Food);
        }

        private void Load(FoodSO food)
        {
            // Load the food details
            Food = food;

            // Set the title
            Title = Food.category.ToString();

            if (Food)
            {
                // Set details
                m_SelectionText.text = Food.foodName;
                m_PriceText.text = Food.price.ToString();
            }
        }

        public void Open(FoodSO food)
        {
            // Load the food details
            Load(food);

            base.Open();
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
            m_FoodMenuUI.Open(Food);

            base.OnSubmit();
        }
    }
}
