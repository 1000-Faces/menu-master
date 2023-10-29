using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DineEase.State
{
    public class DataStore : MonoBehaviour
    {
        public MealComponent SelectedComponent { get; private set; }
        
        // Start is called before the first frame update
        void Start()
        {
            // subscribe to the meal selection changed event
            MealComponent.MealSelectionChangedEvent += OnMealSelectionChanged;
        }

        private void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                SelectedComponent = sender as MealComponent;
                Debug.Log($"Data Store: {SelectedComponent} is selected");
            }
            else
            {
                Debug.Log($"Data Store: {SelectedComponent} is unselected");
                SelectedComponent = null;
            }
        }
    }

}