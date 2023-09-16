using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public MealCategory Category { get; set; }
    }

    public class CategorySelectionUI : ARAnnotationWindow
    {
        public event EventHandler<ComponentSelectionEventArgs> OnCategorySelectedEvent;

        [SerializeField] ToggleGroup m_ToggleGroup;

        void OnCategorySelected(MealCategory category)
        {
            OnCategorySelectedEvent?.Invoke(this, new ComponentSelectionEventArgs { Category = category });
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
                base.Close();
            }
        }

        public override void Close()
        {
            OnCategorySelected(MealCategory.Unknown);

            base.Close();
        }
    }
}
