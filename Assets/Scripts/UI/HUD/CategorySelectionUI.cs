using DineEase.Meal;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI.HUD
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public MealComponent Component { get; set; }

        public MealCategory Category { get; set; }
    }

    public class CategorySelectionUI : FormWindow, ILookMealComponent
    {
        [SerializeField] ToggleGroup m_ToggleGroup;

        MealComponent m_Anchor;

        void Start()
        {
            // subscribe to the meal selection changed event
            MealComponent.MealSelectionChangedEvent += OnMealSelectionChanged;
        }

        public void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            MealComponent component = sender as MealComponent;

            // Check if the component is an anchor
            if (component.IsAnchor)
            {
                m_Anchor = component;
            }
        }

        void OnCategorySelected(MealCategory category)
        {
            Debug.Log($"Category selected: {category}, Target Meal Component: {m_Anchor}");
            if (m_Anchor) m_Anchor.Category = category;
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
