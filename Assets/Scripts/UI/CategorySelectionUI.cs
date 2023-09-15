using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public FoodCategory Category { get; set; }
    }

    public class CategorySelectionUI : ARAnnotationWindow
    {
        public event EventHandler<ComponentSelectionEventArgs> OnCategorySelectedEvent;

        [SerializeField] ToggleGroup m_ToggleGroup;

        void OnCategorySelected(FoodCategory category)
        {
            OnCategorySelectedEvent?.Invoke(this, new ComponentSelectionEventArgs { Category = category });
        }

        public void OnSubmit()
        {
            var toggle = m_ToggleGroup.ActiveToggles().FirstOrDefault();
            if (toggle != null)
            {
                switch (toggle.GetComponentInChildren<Text>().text)
                {
                    case "Main Course":
                        OnCategorySelected(FoodCategory.MainCourse);
                        break;
                    case "Side Dish":
                        OnCategorySelected(FoodCategory.SideDish);
                        break;
                    case "Beverage":
                        OnCategorySelected(FoodCategory.Beverage);
                        break;
                    case "Dessert":
                        OnCategorySelected(FoodCategory.Dessert);
                        break;
                    default:
                        OnCategorySelected(FoodCategory.Unknown);
                        break;
                }

                // gameObject.SetActive(false);
                Debug.Log("Category selected: " + toggle.GetComponentInChildren<Text>().text);
            }
        }

        public override void Close()
        {
            OnCategorySelected(FoodCategory.Unknown);

            base.Close();
        }
    }
}
