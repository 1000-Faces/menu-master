using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMealCategory", menuName = "ScriptableObjects/Meal Category")]
public class MealCategorySO : ScriptableObject
{
    public FoodCategory categoryName;

    public Transform prefab;

    public Sprite categoryIcon;
}
