using DineEase.Meal;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI.HUD
{
    public class DynamicHUDButtons : MonoBehaviour, ILookMealComponent
    {
        [SerializeField] Button m_ChangeMealCategoryButon;
        [SerializeField] Button m_ChangeFoodButon;
        [SerializeField] Button m_DeleteButon;

        // Start is called before the first frame update
        void Start()
        {
            // subscribe to the meal selection changed event
            MealComponent.MealSelectionChangedEvent += OnMealSelectionChanged;
        }

        public void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            ChangeBehaviour(sender as MealComponent);
        }

        private void ChangeBehaviour(MealComponent component)
        {
            // Check if the component is an anchor
            if (component.IsAnchor)
            {
                Debug.Log($"An anchor selected. ({component.name})");
                // Show meal category button
                m_ChangeMealCategoryButon.gameObject.SetActive(true);
                // Hide food button
                m_ChangeFoodButon.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log($"A {component.Category} selected. ({component.name})");
                // Hide meal category button
                m_ChangeMealCategoryButon.gameObject.SetActive(true);
                // Show food button
                m_ChangeFoodButon.gameObject.SetActive(false);
            }
        }
    }
}
