using DineEase.Meal;
using DineEase.State;
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

        private void OnEnable()
        {
            m_MealComponent = m_DataStore.SelectedComponent;
            
            // Check if the component is an anchor
            if (m_MealComponent.IsAnchor)
            {
                Debug.Log($"Dynamic HUD: An anchor selected. ({m_MealComponent.name})");
                // Show meal category button
                m_ChangeMealCategoryButon.gameObject.SetActive(true);
                // Hide food button
                m_ChangeFoodButon.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log($"Dynamic HUD: A {m_MealComponent.Category} selected. ({m_MealComponent.name})");
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
