using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MealComponentVisual : MonoBehaviour, IObjectLoader <FoodCategory, MealCategorySO>
{
    private GameObject _currentObject;
    [SerializeField] private List<MealCategorySO> swappableObjects = new();

    public List<MealCategorySO> SwappableObjects => swappableObjects;

    private void Start()
    {
        // make sure remove all all child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

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
            _currentObject = Instantiate(obj.prefab.gameObject, transform);
            _currentObject.transform.localPosition = Vector3.zero;
        }
        catch
        {
            Debug.LogError("Meal Component not found");
        }
    }

    public void SwapObject(FoodCategory name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject);
        }

        LoadObject(name);
    }
}
