using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealComponent : MonoBehaviour
{
    [SerializeField] private Transform foodAnchorPoint;
    [SerializeField] private FoodScriptableObject food;
    [SerializeField] private MealComponentVisual foodCategoryVisualizer;
    
    private Utils.FoodCategory foodCategory = Utils.FoodCategory.Unknown;

    public FoodObject FoodObject { get; set; }

    public static event EventHandler<SelectedFoodChangedEventArgs> OnSelectedFoodChanged;
    public class SelectedFoodChangedEventArgs : EventArgs
    {
        public FoodScriptableObject SelectedFood { get; set; }
    }

    private void Start()
    {
        // visualize the meal component
        foodCategoryVisualizer.SwapObject(foodCategory);
    }

    //private void SpawnFood(FoodScriptableObject foodSO)
    //{
    //    if (FoodObject == null)
    //    {
    //        Transform target;

    //        if (foodSO.requirePlatform)
    //        {
    //            // show the platform
    //            platform.SetActive(true);
    //            // The object should be spawned on the platform
    //            target = foodAnchorPoint;
    //        }
    //        else
    //        {
    //            // hide the platform
    //            platform.SetActive(false);
    //            // The object should be spawned center of the meal component
    //            target = transform;
    //        }

    //        // spawn the food on the target location
    //        Transform foodTransform = Instantiate(foodSO.prefab, target);
    //        foodTransform.GetComponent<FoodObject>().MealComponent = this;

    //        // fire the event
    //        OnSelectedFoodChanged?.Invoke(this, new SelectedFoodChangedEventArgs { SelectedFood = foodSO });
    //    }
    //    else
    //    {
    //        Debug.Log($"MealComponent already has a food. (Meal component: {this.name} | Food: {food.name})");
    //    }
    //}

    public void ChangeCategory(Utils.FoodCategory category)
    {
        foodCategoryVisualizer.SwapObject(category);
    }
}