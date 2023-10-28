using DineEase.AR;
using DineEase.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DineEase.Meal
{
    [RequireComponent(typeof(ExtendedAnnotationInteractable))]
    [RequireComponent(typeof(ExtendedSelectionInteractable))]
    public class MealComponent : MessageBox
    {
        [SerializeField] MealComponentBaseVisual m_FoodCategoryVisualizer;

        [SerializeField] FoodVisual m_FoodVisualizer;

        [SerializeField] FoodDetailsUI m_FoodSelectionUI;


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
            // subscribe to the food selection changing event
            m_FoodSelectionUI.OnFoodSelectedEvent += OnFoodSelected;
        }

        void OnFoodSelected(object sender, FoodSelectionChangedEventArgs e)
        {
            if (e.NewFoodSelection != null)
            {
                m_FoodCategoryVisualizer.ToggleVisibility(e.NewFoodSelection.requirePlatform);

                Food = e.NewFoodSelection;
            }
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            m_FoodSelectionUI.Title = m_Category.ToString();
            m_FoodSelectionUI.Open();
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            m_FoodSelectionUI.Close();
        }
    }
}