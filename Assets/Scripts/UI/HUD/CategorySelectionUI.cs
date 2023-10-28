using DineEase.Meal;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public MealComponent Component { get; set; }

        public MealCategory Category { get; set; }
    }

    public class CategorySelectionUI : FormWindow
    {
        [SerializeField] ToggleGroup m_ToggleGroup;

        MealComponent m_Placeholder;

        void Start()
        {
            // subscribe to the placeholder selection event
            MealComponent.OnMealSelectionChangedEvent += OnMealSelectionChanged;
        }

        void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                m_Placeholder = (MealComponent)sender;
            }
            else
            {
                if (IsOpened) Close(1);
            }
        }

        void OnCategorySelected(MealCategory category)
        {
            if (m_Placeholder) m_Placeholder.Category = category;
        }

        public override void OnSubmit()
        {
            var toggle = m_ToggleGroup.ActiveToggles().FirstOrDefault();
            if (toggle != null)
            {
                switch (toggle.GetComponentInChildren<Text>().text)
                {
                    case "Main Course":
                        OnCategorySelected(MealCategory.MainCourse);
                        break;
                    case "Side Dish":
                        OnCategorySelected(MealCategory.SideDish);
                        break;
                    case "Beverage":
                        OnCategorySelected(MealCategory.Beverage);
                        break;
                    case "Dessert":
                        OnCategorySelected(MealCategory.Dessert);
                        break;
                    default:
                        OnCategorySelected(MealCategory.Unknown);
                        break;
                }

                // close the window
                Close(0);
            }
        }
    }
}
