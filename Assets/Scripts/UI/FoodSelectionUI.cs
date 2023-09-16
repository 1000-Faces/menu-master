using DineEase.Meal;
using DineEase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace DineEase.UI
{
    public class FoodSelectionChangedEventArgs : EventArgs
    {
        public FoodSO NewFoodSelection { get; set; }
    }

    public class FoodSelectionUI : ARAnnotationWindow
    {
        const string SELECTION_TEXT_DEFAULT = "Select a food";

        public event EventHandler<FoodSelectionChangedEventArgs> OnFoodSelectedEvent;

        [SerializeField] TextMeshProUGUI m_SelectionText;
        [SerializeField] FoodSO m_TestFoodSO; // for debuging

        FoodSO m_CurrentFoodSelection;

        FoodSO m_NewFoodSelection;

        protected override void Awake()
        {
            base.Awake();

            // set the default text
            m_SelectionText.text = SELECTION_TEXT_DEFAULT;
        }

        void OnFoodSelected(FoodSO newFoodSelection)
        {
            if (m_CurrentFoodSelection != newFoodSelection)
            {
                m_NewFoodSelection = newFoodSelection;

                // fire off the event to change the food visual
                OnFoodSelectedEvent?.Invoke(this, new FoodSelectionChangedEventArgs { NewFoodSelection = newFoodSelection });
            }
        }

        public void OnSlectionChanged()
        {
            // Change the text to the new selection
            m_SelectionText.text = m_TestFoodSO.foodName;

            OnFoodSelected(m_TestFoodSO);
        }

        public override void OnSubmit()
        {
            // Change current food selection to the new one
            m_CurrentFoodSelection = m_NewFoodSelection;

            // fire off the event to change the food visual
            OnFoodSelectedEvent?.Invoke(this, new FoodSelectionChangedEventArgs { NewFoodSelection = m_CurrentFoodSelection });

            // Close the window.
            // The default close method is overriden. So It sould be used to the simple close method of the base class
            base.Close();
        }

        public override void Close()
        {
            // Reset the new food selection
            m_NewFoodSelection = null;

            if (m_CurrentFoodSelection)
            {
                // Reset the text
                m_SelectionText.text = m_CurrentFoodSelection.foodName;

                // fire off the event to change the food visual back to the previous selection
                OnFoodSelectedEvent?.Invoke(this, new FoodSelectionChangedEventArgs { NewFoodSelection = m_CurrentFoodSelection });
            }

            // Close the window.
            base.Close();
        }
    }
}
