using DineEase.AR;
using DineEase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DineEase.Meal
{
    [RequireComponent(typeof(ExtendedAnnotationInteractable))]
    public class MealComponent : MonoBehaviour
    {
        [SerializeField] MealComponentVisual m_FoodCategoryVisualizer;

        [SerializeField] CategorySelectionUI m_CategorySelectionUI;

        [SerializeField] FoodVisual m_FoodVisualizer;

        [SerializeField] FoodSelectionUI m_FoodSelectionUI;


        DineEase.MealCategory m_Category = DineEase.MealCategory.Unknown;

        public DineEase.MealCategory Category
        {
            get => m_Category;
            set
            {
                m_Category = value;
                m_FoodCategoryVisualizer.SwapObject(m_Category);
            }
        }

        FoodSO m_Food;

        public FoodSO Food
        {
            get => m_Food;
            set
            {
                m_Food = value;
                m_FoodVisualizer.SwapObject(m_Food.prefab);
            }
        }

        void Awake()
        {
            // visualize the 'Unknown' meal component
            Category = m_Category;
        }

        void Start()
        {
            Utils.ShowToastMessage("Tap to change the category");

            // subscribe to the category selection event
            m_CategorySelectionUI.OnCategorySelectedEvent += OnCategorySelected;

            // subscribe to the food selection changing event
            m_FoodSelectionUI.OnFoodSelectedEvent += OnFoodSelected;
        }

        void OnCategorySelected(object sender, ComponentSelectionEventArgs e)
        {
            Category = e.Category;
        }

        void OnFoodSelected(object sender, FoodSelectionChangedEventArgs e)
        {
            if (e.NewFoodSelection != null)
            {
                if (e.NewFoodSelection.requirePlatform)
                {
                    m_FoodCategoryVisualizer.ToggleVisibility(false);
                }
                else
                {
                    m_FoodCategoryVisualizer.ToggleVisibility(true);
                }

                Food = e.NewFoodSelection;
            }
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            if (m_Category == DineEase.MealCategory.Unknown)
            {
                // Enable Add button using the event
                // OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = true });

                // Open the Category selection UI
                m_CategorySelectionUI.Open();
            }
            else
            {
                m_FoodSelectionUI.Title = m_Category.ToString();
                m_FoodSelectionUI.Open();
            }
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            if (m_Category == DineEase.MealCategory.Unknown)
            {
                // Enable Add button using the event
                // OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = false });

                // Close the Category selection UI
                m_CategorySelectionUI.Close();
            }
            else
            {
                m_FoodSelectionUI.Close();
            }
        }
    }
}