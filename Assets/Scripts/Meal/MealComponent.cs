using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MealComponent : MonoBehaviour
{
    [SerializeField] private MealComponentVisual foodCategoryVisualizer;
    [SerializeField] private FoodCategory category;
    // private ARSelectionInteractable arSelectionInteractable;
    // private ARPlacementInteractable arPlacementInteractable;

    public static event EventHandler<ComponentInitializedEventArgs> OnComponentInitialized;
    public class ComponentInitializedEventArgs : EventArgs
    {
        public MealComponent TargetComponent { get; set; }
    }

    private void Start()
    {
        // visualize the meal component
        ChangeCategory(category);

        Utils.ShowToastMessage("Tap to change the category");
    }

    public void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        Utils.ShowToastMessage("Change the category");

        // fire the event
        OnComponentInitialized?.Invoke(this, new ComponentInitializedEventArgs { TargetComponent = this });
    }

    public void ChangeCategory(FoodCategory category)
    {
        foodCategoryVisualizer.SwapObject(category);
    }
}