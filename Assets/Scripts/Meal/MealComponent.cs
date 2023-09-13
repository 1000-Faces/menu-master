using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ComponentSelectionEventArgs : EventArgs
{
    public bool IsSelected { get; set; }
}

public class MealComponent : MonoBehaviour
{
    [SerializeField] private MealComponentVisual foodCategoryVisualizer;
    private FoodCategory _category = FoodCategory.Unknown;
    // private ARSelectionInteractable arSelectionInteractable;
    // private ARPlacementInteractable arPlacementInteractable;

    public static event EventHandler<ComponentSelectionEventArgs> OnComponentSelectionChanged;

    public FoodCategory Category
    {
        get => _category;
        set
        {
            _category = value;
            ChangeCategory(_category);
        }
    }

    private void Start()
    {
        // visualize the meal component
        ChangeCategory(_category);

        Utils.ShowToastMessage("Tap to change the category");
    }

    private void ChangeCategory(FoodCategory category)
    {
        foodCategoryVisualizer.SwapObject(category);
    }

    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        if (_category == FoodCategory.Unknown)
        {
            // Enable Add button using the event
            OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = true });
        }
    }

    public void OnSelectExited(SelectExitEventArgs arg0)
    {
        if (_category == FoodCategory.Unknown)
        {
            // Enable Add button using the event
            OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = false });
        }
    }
}