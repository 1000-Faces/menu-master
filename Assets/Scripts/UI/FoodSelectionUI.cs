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
        [SerializeField] FoodMenuUI m_FoodMenuUI;

        FoodSO m_CurrentFoodSelection;

        FoodSO m_NewFoodSelection;

        protected override void Awake()
        {
            base.Awake();

            // set the default text
            m_SelectionText.text = SELECTION_TEXT_DEFAULT;
        }

        protected override void Start()
        {
            base.Start();

            // subscribe to the food selection event
            m_FoodMenuUI.OnFoodSelectedEvent += OnFoodSelected;
        }

        private void OnFoodSelected(object sender, FoodSelectedEventArgs e)
        {
            m_NewFoodSelection = e.NewFoodSelection;
            m_SelectionText.text = m_NewFoodSelection.foodName;
        }

        public void OnFoodMenuOpen()
        {
            m_FoodMenuUI.Open();
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
