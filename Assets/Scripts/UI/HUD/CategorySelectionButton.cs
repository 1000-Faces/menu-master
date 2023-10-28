using DineEase.Meal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DineEase.UI
{
    public class CategorySelectionButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // subscribe to the placeholder selection event
            Placeholder.OnPlaceholderSelectedEvent += OnPlaceholderSelected;
            // subscribe to the message box response event
            FormWindow.OnFormResponseEvent += OnMessageBoxResponse;
        }

        private void OnMessageBoxResponse(object sender, FormResponse e)
        {
            if (sender is CategorySelectionUI && e.Response == 0)
            {
                // Hide the button
                gameObject.SetActive(false);
            }
        }

        private void OnPlaceholderSelected(object sender, ToggleEventArgs e)
        {
            if (e.Toggle)
            {
                gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
