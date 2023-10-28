using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class CategorySelectionButton : MonoBehaviour
    {
        [SerializeField] Button m_ChangeCatogoryButon;
        [SerializeField] Button m_DeleteButon;

        // Start is called before the first frame update
        void Start()
        {
            // m_ChangeCatogoryButon.enabled = false;

            // subscribe to the placeholder selection event
            MealComponent.OnMealSelectionChangedEvent += OnMealSelectionChanged;
            // subscribe to the message box response event
            FormWindow.OnFormResponseEvent += OnMessageBoxResponse;
        }

        private void OnMessageBoxResponse(object sender, FormResponse e)
        {
            if (sender is CategorySelectionUI && e.Response == 0)
            {
                // Hide the button
                m_ChangeCatogoryButon.gameObject.SetActive(false);
            }
            else if (sender is DeleteComponentUI && e.Response == 0)
            {
                // Hide the button
                m_DeleteButon.gameObject.SetActive(false);
            }
        }

        private void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e)
        {
            if (e.Category == MealCategory.Unknown)
            {
                m_ChangeCatogoryButon.gameObject.SetActive(e.IsSelected);
            }
            else
            {
                m_DeleteButon.gameObject.SetActive(e.IsSelected);
            }
        }
    }
}
