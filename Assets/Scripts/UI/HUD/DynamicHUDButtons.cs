using DineEase.Meal;
using DineEase.State;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI.HUD
{
    public class DynamicHUDButtons : MonoBehaviour
    {
        [SerializeField] DataStore m_DataStore;
        [SerializeField] Button m_ChangeMealCategoryButon;
        [SerializeField] Button m_ChangeFoodButon;
        [SerializeField] Button m_DeleteButon;

        MealComponent m_MealComponent;

        private void Start()
        {
            // Subscribe to the category selection ui form response event
            FormWindow.FormResponseEvent += OnFormResponse;
        }

        private void OnEnable()
        {
            m_MealComponent = m_DataStore.SelectedComponent;

            Refresh();
        }

        private void OnFormResponse(object sender, FormResponse e)
        {
            if (sender is CategorySelectionUI && e.Response == 0)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            // Check if the component is an anchor
            if (m_MealComponent.IsAnchor)
            {
                // Show meal category button
                m_ChangeMealCategoryButon.gameObject.SetActive(true);
                // Hide food button
                m_ChangeFoodButon.gameObject.SetActive(false);
            }
            else
            {
                // Hide meal category button
                m_ChangeMealCategoryButon.gameObject.SetActive(false);
                // Show food button
                m_ChangeFoodButon.gameObject.SetActive(true);
            }
        }

        public void OnFoodButtonClick()
        {
            m_MealComponent.OpenUI();
        }
    }
}
