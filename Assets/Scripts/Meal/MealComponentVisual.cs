using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MealComponentVisual : MonoBehaviour, IObjectLoader <FoodCategory, MealCategorySO>
{
    private Transform _currentObject;
    [SerializeField] private List<MealCategorySO> swappableObjects = new();

    public List<MealCategorySO> SwappableObjects => swappableObjects;

    public void AddObject(MealCategorySO obj)
    {
        if (!swappableObjects.Contains(obj))
        {
            swappableObjects.Add(obj);
        }
    }

    public void LoadObject(FoodCategory name)
    {
        try
        {
            // spawn the new object
            var obj = swappableObjects.Where(obj => obj.categoryName == name).FirstOrDefault();
            // Instantiate the object
            _currentObject = Instantiate(obj.prefab, transform);
        }
        catch (Exception ex)
        {
            Debug.LogError("Meal Component not found. Error: " + ex);
        }
    }

    public void SwapObject(FoodCategory name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject.gameObject);
        }

        LoadObject(name);
    }
}
