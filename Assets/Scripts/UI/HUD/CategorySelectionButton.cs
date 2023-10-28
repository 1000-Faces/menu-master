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
        
        // Start is called before the first frame update
        void Start()
        {
            // m_ChangeCatogoryButon.enabled = false;

            // subscribe to the placeholder selection event
            MealComponent.OnPlaceholderSelectedEvent += OnPlaceholderSelected;
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
        }

        private void OnPlaceholderSelected(object sender, ToggleEventArgs e)
        {
            m_ChangeCatogoryButon.gameObject.SetActive(e.Toggle);
        }
    }
}
