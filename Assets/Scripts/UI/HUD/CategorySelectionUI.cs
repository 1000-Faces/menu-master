using DineEase.Meal;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public Placeholder Placeholder { get; set; }

        public MealCategory Category { get; set; }
    }

    public class CategorySelectionUI : FormWindow
    {
        public static event EventHandler<ComponentSelectionEventArgs> OnCategorySelectedEvent;

        [SerializeField] ToggleGroup m_ToggleGroup;

        Placeholder m_Placeholder;

        void Start()
        {
            // subscribe to the placeholder selection event
            Placeholder.OnPlaceholderSelectedEvent += OnPlaceholderSelected;
        }

        void OnPlaceholderSelected(object sender, ToggleEventArgs e)
        {
            if (e.Toggle)
            {
                m_Placeholder = (Placeholder)sender;
            }
        }

        void OnCategorySelected(MealCategory category)
        {
            if (m_Placeholder) OnCategorySelectedEvent?.Invoke(this, new ComponentSelectionEventArgs { Placeholder = m_Placeholder, Category = category });
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
                        OnCategorySelected(MealCategory.MainCourse);
                        break;
                }

                // close the window
                Close(1);
            }
        }
    }
}
