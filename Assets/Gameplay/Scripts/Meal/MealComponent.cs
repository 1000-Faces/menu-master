using DineEase;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MealSelectionChangeEventArgs : EventArgs
{
    public bool IsSelected { get; set; }
}

public class MealComponent : MonoBehaviour
{
    public static event EventHandler<MealSelectionChangeEventArgs> MealSelectionChangeEvent;
    public static event EventHandler FoodChangeEvent;

    [SerializeField] MealComponentBaseVisual m_FoodCategoryVisualizer;
    [SerializeField] FoodVisual m_FoodVisualizer;
    [SerializeField] FoodDetailsUI m_FoodDetailsWindow;
    [SerializeField] FoodMenuUI m_FoodMenuUI;

    public FoodDetailsUI FoodDetailsWindow => m_FoodDetailsWindow;
    public FoodMenuUI FoodMenuUI => m_FoodMenuUI;

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

    FoodData m_Food;

    public FoodData Food
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


    private void Start()
    {
        // Set the default category to the placeholder(unknown)
        Category = MealCategory.Unknown;

        Utils.ShowToastMessage("Tap to change the category");
    }

    public void ChangeFood(FoodData newFood)
    {
        FoodData oldFood = m_Food;

        // load the new food into the meal component
        m_FoodCategoryVisualizer.ToggleVisibility(newFood.requirePlatform);
        Food = newFood;

        // fireoff the event
        FoodChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OpenUI()
    {
        if (IsAnchor) return;
        else if (IsFood)
        {
            // Open the Food Details window
            m_FoodDetailsWindow.Open();
        }
        else
        {
            // Open the Food Menu window with the selected category
            m_FoodMenuUI.Open();
        }
    }

    public void CloseUI()
    {
        // Close the Food Details window
        m_FoodDetailsWindow.Close(1);
        // Close the Food Menu window
        m_FoodMenuUI.Close(1);
    }

    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        // fireoff the event
        MealSelectionChangeEvent?.Invoke(this, new MealSelectionChangeEventArgs { IsSelected = true });
    }

    public void OnSelectExited(SelectExitEventArgs arg0)
    {
        // fireoff the event
        MealSelectionChangeEvent?.Invoke(this, new MealSelectionChangeEventArgs { IsSelected = false });
    }

    public void OnObjectPlaced(ARObjectPlacementEventArgs arg0)
    {
        // set active the placement object
        arg0.placementObject.SetActive(true);
    }
}