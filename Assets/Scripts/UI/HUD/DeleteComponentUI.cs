using DineEase.Meal;
using DineEase.State;
using UnityEngine;

namespace DineEase.UI.HUD
{
    public class DeleteComponentUI : FormWindow
    {
        [SerializeField] DataStore m_DataStore;

        MealComponent m_MealComponent;

        private void OnEnable()
        {
            m_MealComponent = m_DataStore.SelectedComponent;
        }

        public void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            m_MealComponent = sender as MealComponent;

            Debug.Log($"Deleted {m_MealComponent}");
        }

        public override void OnSubmit()
        {
            if (m_MealComponent) Destroy(m_MealComponent.gameObject);

            base.OnSubmit();
        }
    }
}
