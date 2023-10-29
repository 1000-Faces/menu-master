using DineEase.AR;
using DineEase.UI;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace DineEase.Meal
{
    public class MealSelectionChangedEventArgs : EventArgs
    {
        public bool IsSelected { get; set; }
    }

    public class SelectedFoodChangeEventArgs : EventArgs
    {
        public FoodSO CurrentFood { get; set; }

        public FoodSO PreviousFood { get; set; }
    }
    
    public class MealComponent : MonoBehaviour
    {
        public static event EventHandler<MealSelectionChangedEventArgs> MealSelectionChangedEvent;
        public static event EventHandler<SelectedFoodChangeEventArgs> SelectedFoodChangeEvent;

        [SerializeField] MealComponentBaseVisual m_FoodCategoryVisualizer;
        [SerializeField] FoodVisual m_FoodVisualizer;
        [SerializeField] FoodDetailsUI m_FoodDetailsWindow;
        [SerializeField] FoodMenuUI m_FoodMenuUI;


        MealCategory m_Category;

        public MealCategory Category
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
            private set
            {
                m_Food = value;
                m_FoodVisualizer.SwapObject(m_Food.prefab);
            }
        }

        public bool IsFood => Food != null;

        public bool IsPlaceholder => Category != MealCategory.Unknown;

        public bool IsAnchor => Category == MealCategory.Unknown;

        
        void Start()
        {
            // Set the default category to the placeholder(unknown)
            Category = MealCategory.Unknown;

            Utils.ShowToastMessage("Tap to change the category");
        }

        public void ChangeFood(FoodSO newFood)
        {
            FoodSO oldFood = m_Food;
            
            // load the new food into the meal component
            m_FoodCategoryVisualizer.ToggleVisibility(newFood.requirePlatform);
            Food = newFood;

            // fireoff the event
            SelectedFoodChangeEvent?.Invoke(this, new SelectedFoodChangeEventArgs { CurrentFood = newFood, PreviousFood = oldFood });
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            // fireoff the event
            MealSelectionChangedEvent?.Invoke(this, new MealSelectionChangedEventArgs { IsSelected = true });
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            // fireoff the event
            MealSelectionChangedEvent?.Invoke(this, new MealSelectionChangedEventArgs { IsSelected = false });
        }

        public void OnObjectPlaced(ARObjectPlacementEventArgs arg0)
        {
            // set active the placement object
            arg0.placementObject.SetActive(true);
        }
    }
}