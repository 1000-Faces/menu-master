using DineEase.AR.Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DineEase.Meal
{
    public class ComponentSelectionEventArgs : EventArgs
    {
        public bool IsSelected { get; set; }
    }

    [RequireComponent(typeof(ExtendedAnnotationInteractable))]
    public class MealComponent : MonoBehaviour
    {
        const string CATEGORY_SELECTION_MENU = "CategoryWindow";

        public static event EventHandler<ComponentSelectionEventArgs> OnComponentSelectionChanged;

        [SerializeField] MealComponentVisual m_FoodCategoryVisualizer;

        FoodCategory m_Category = FoodCategory.Unknown;

        public FoodCategory Category
        {
            get => m_Category;
            set
            {
                m_Category = value;
                ChangeCategory(m_Category);
            }
        }


        // private ARSelectionInteractable arSelectionInteractable;
        // private ARPlacementInteractable arPlacementInteractable;
        ExtendedAnnotationInteractable m_ExtendedAnnotationInteractable;

        void Awake()
        {
            // arSelectionInteractable = GetComponent<ARSelectionInteractable>();
            // arPlacementInteractable = GetComponent<ARPlacementInteractable>();
            m_ExtendedAnnotationInteractable = GetComponent<ExtendedAnnotationInteractable>();

            // visualize the meal component
            ChangeCategory(m_Category);
        }

        void Start()
        {
            Utils.ShowToastMessage("Tap to change the category");
        }

        void ChangeCategory(FoodCategory category)
        {
            m_FoodCategoryVisualizer.SwapObject(m_Category);
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            if (m_Category == FoodCategory.Unknown)
            {
                // Enable Add button using the event
                // OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = true });

                // Enable the Category selection UI
                m_ExtendedAnnotationInteractable.GetAnnotation(CATEGORY_SELECTION_MENU).IsEnabled = true;
            }
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            if (m_Category == FoodCategory.Unknown)
            {
                // Enable Add button using the event
                // OnComponentSelectionChanged?.Invoke(this, new ComponentSelectionEventArgs { IsSelected = false });

                // Disable the Category selection UI
                m_ExtendedAnnotationInteractable.GetAnnotation(CATEGORY_SELECTION_MENU).IsEnabled = false;
            }
        }
    }
}