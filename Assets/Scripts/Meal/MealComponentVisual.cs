using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MealComponentVisual : MonoBehaviour, IObjectSwapper <FoodCategory, MealCategorySO>
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

    public void SwapObject(FoodCategory name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject);
        }

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
}
