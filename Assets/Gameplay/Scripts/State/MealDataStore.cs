using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MealDataStore : MonoBehaviour
{
    public MealComponent SelectedComponent { get; private set; }

    public HashSet<MealComponent> FoodObjects { get; } = new HashSet<MealComponent>();

    [Header("Events")]
    public UnityEvent FoodListChangeEvent;


    // Start is called before the first frame update
    private void Start()
    {
        // subscribe to the meal selection changed event
        MealComponent.MealSelectionChangeEvent += OnMealSelectionChange;
        // subscribe to the food changed event
        MealComponent.FoodChangeEvent += OnFoodChange;
    }

    private void OnFoodChange(object sender, EventArgs e)
    {
        MealComponent mealComponent = sender as MealComponent;

        Debug.Log($"Data Store: Food has changed in the {mealComponent}");
        // Add the food to the food objects list
        FoodObjects.Add(mealComponent);

        // Invoke the food list change event
        FoodListChangeEvent.Invoke();
    }

    private void OnMealSelectionChange(object sender, MealSelectionChangeEventArgs e)
    {
        MealComponent mealComponent = sender as MealComponent;

        if (e.IsSelected)
        {
            SelectedComponent = mealComponent;
            Debug.Log($"Data Store: {mealComponent} is selected");
        }
        else
        {
            SelectedComponent = null;
            Debug.Log($"Data Store: {mealComponent} is unselected");
        }
    }

    public float GetTotalPrice()
    {
        // Calculate the total price of the food objects
        return FoodObjects.Sum(food => food.Food.price);
    }
}