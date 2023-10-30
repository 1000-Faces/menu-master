using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelectionUI : FormWindow
{
    [SerializeField] DataStore m_DataStore;
    [SerializeField] ToggleGroup m_ToggleGroup;

    MealComponent m_Anchor;

    private void OnEnable()
    {
        if (m_DataStore.SelectedComponent.IsAnchor)
        {
            m_Anchor = m_DataStore.SelectedComponent;
            return;
        }

        Debug.LogError("CategorySelectionUI: Window is loaded but no anchor is selected");
    }

    void OnCategorySelected(MealCategory category)
    {
        m_Anchor.Category = category;
        Debug.Log($"Category selected: {category}, Target Meal Component: {m_Anchor}");
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
