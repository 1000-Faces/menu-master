using DineEase.AR;
using DineEase.UI;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DineEase.Meal
{
    public class ToggleEventArgs : EventArgs
    {
        public bool Toggle { get; set; }
    }

    [RequireComponent(typeof(ExtendedSelectionInteractable))]
    public class Placeholder : MonoBehaviour
    {
        public static event EventHandler<ToggleEventArgs> OnPlaceholderSelectedEvent;

        [SerializeField] Transform m_MealComponentPrefab;

        private void Start()
        {
            Utils.ShowToastMessage("Tap to change the category");

            // subscribe to the catogory selection event
            CategorySelectionUI.OnCategorySelectedEvent += OnCategorySelected;
        }

        private void OnCategorySelected(object sender, ComponentSelectionEventArgs e)
        {
            if (e.Placeholder == this)
            {
                SwapToMealComponent(e.Category);
            }
        }

        void SwapToMealComponent(MealCategory category)
        {
            // Instantiate the meal component prefab
            var mealComponentTransform = Instantiate(m_MealComponentPrefab, transform);

            // Set the category
            var mealComponent = mealComponentTransform.GetComponent<MealComponent>();
            mealComponent.Category = category;

            // Dispose the placeholder
            Destroy(gameObject);
        }

        public void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            OnPlaceholderSelectedEvent?.Invoke(this, new ToggleEventArgs { Toggle = true });
        }

        public void OnSelectExited(SelectExitEventArgs arg0)
        {
            OnPlaceholderSelectedEvent?.Invoke(this, new ToggleEventArgs { Toggle = false });
        }
    }
}
