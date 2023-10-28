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
            MessageBox.OnMessageBoxResponseEvent += OnMessageBoxResponse;
        }

        private void OnMessageBoxResponse(object sender, MessageBoxResponse e)
        {
            if (e.Response == 1)
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
