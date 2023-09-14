using DineEase.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnAdd;
    [SerializeField] private MessageBox mealSelector;

    private void Start()
    {
        // make sure the add button is not visible at the start
        btnAdd.gameObject.SetActive(false);

        mealSelector.OnCloseExtraFunction += OnMealSelectorClosed;

        // subscribe to the event
        MealComponent.OnComponentSelectionChanged += MealComponent_OnComponentSelectionChanged;
    }

    private void MealComponent_OnComponentSelectionChanged(object sender, ComponentSelectionEventArgs e)
    {
        MealComponent target = sender as MealComponent;

        // show the add button if the selected component is unknown
        if (target.Category == FoodCategory.Unknown)
        {
            btnAdd.gameObject.SetActive(e.IsSelected);
        }
    }

    public void OnMenuClicked()
    {
        Utils.ShowToastMessage("Menu Opened!");
    }

    public void OnAddClicked()
    {
        mealSelector.gameObject.SetActive(true);
    }

    private void OnMealSelectorClosed()
    {
        gameObject.SetActive(true);
    }
}
