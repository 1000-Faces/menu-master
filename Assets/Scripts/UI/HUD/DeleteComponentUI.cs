using DineEase.Meal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DineEase.UI
{
    public class DeleteComponentUI : FormWindow
    {
        MealComponent m_MealComponent;

        void Start()
        {
            // subscribe to the placeholder selection event
            MealComponent.OnMealSelectionChangedEvent += OnMealSelectionChanged;
        }

        void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                m_MealComponent = (MealComponent)sender;
            }
            else
            {
                if (IsOpened) Close(1);
            }
        }

        public override void OnSubmit()
        {
            if (m_MealComponent) Destroy(m_MealComponent.gameObject);

            base.OnSubmit();
        }
    }
}
