using DineEase.Meal;
using UnityEngine;

namespace DineEase.UI.HUD
{
    public class DeleteComponentUI : FormWindow, ILookMealComponent
    {
        MealComponent m_MealComponent;

        void Start()
        {
            // subscribe to the meal selection changed event
            MealComponent.MealSelectionChangedEvent += OnMealSelectionChanged;
        }
        
        public void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            m_MealComponent = sender as MealComponent;

            Debug.Log($"Deleted {m_MealComponent}");
        }

        public override void OnSubmit()
        {
            if (m_MealComponent) Destroy(m_MealComponent.gameObject);

            base.OnSubmit();
        }
    }
}
