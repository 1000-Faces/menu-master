using DineEase.AR;
using DineEase.UI;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DineEase.Meal
{
    public class MealSelectionChangedEventArgs : EventArgs
    {
        public bool IsSelected { get; set; }

        public MealCategory Category { get; set; }
    }
    
    public class MealComponent : MonoBehaviour
    {
        public static event EventHandler<MealSelectionChangedEventArgs> OnMealSelectionChangedEvent;

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
            set
            {
                m_Food = value;
                m_FoodVisualizer.SwapObject(m_Food.prefab);
            }
        }
        
        void Start()
        {
            // Set the default category to the placeholder(unknown)
            Category = MealCategory.Unknown;

            Utils.ShowToastMessage("Tap to change the category");
        }

        public void OnSelectedFoodChange(FoodSO newFood)
        {
            m_FoodCategoryVisualizer.ToggleVisibility(newFood.requirePlatform);

            Food = newFood;
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            if (Category != MealCategory.Unknown)
            {
                m_FoodDetailsWindow.Open(m_Category.ToString(), Food);
                
            }

            OnMealSelectionChangedEvent?.Invoke(this, new MealSelectionChangedEventArgs { IsSelected = true, Category = m_Category });
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            if (Category != MealCategory.Unknown)
            {
                m_FoodDetailsWindow.Close(1); // close without saving the selection (1 = close in failure)
                
            }

            OnMealSelectionChangedEvent?.Invoke(this, new MealSelectionChangedEventArgs { IsSelected = true, Category = m_Category });
        }
    }
}