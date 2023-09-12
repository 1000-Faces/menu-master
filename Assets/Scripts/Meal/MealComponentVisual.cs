using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MealComponentVisual : MonoBehaviour, IObjectSwapper <Utils.FoodCategory>
{
    [Serializable]
    public struct MealComponentType
    {
        public Utils.FoodCategory Name { get; set; }
        public GameObject Prefab { get; set; }
    }
    
    private GameObject _currentObject;
    private List<MealComponentType> _swappableObjects = new();

    [SerializeField] private GameObject placeholder;
    [SerializeField] private GameObject maiinCourse;
    [SerializeField] private GameObject sideDish;
    [SerializeField] private GameObject beverage;
    [SerializeField] private GameObject dessert;


    private void Start()
    {
        // make sure remove all all child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Add all the objects to the list
        AddObject(Utils.FoodCategory.Unknown, placeholder);
        AddObject(Utils.FoodCategory.MainCourse, maiinCourse);
        AddObject(Utils.FoodCategory.SideDish, sideDish);
        AddObject(Utils.FoodCategory.Beverage, beverage);
        AddObject(Utils.FoodCategory.Dessert, dessert);
    }

    public void AddObject(Utils.FoodCategory name, GameObject obj)
    {
        MealComponentType temp = new() { Name = name, Prefab = obj };

        if (!_swappableObjects.Contains(temp))
        {
            _swappableObjects.Add(temp);
        }
    }

    public void SwapObject(Utils.FoodCategory name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject);
        }

        try
        {
            // spawn the new object
            var obj = _swappableObjects.Where(obj => obj.Name == name).FirstOrDefault();
            // Instantiate the object
            _currentObject = Instantiate(obj.Prefab, transform);
            _currentObject.transform.localPosition = Vector3.zero;
            // _currentObject.SetActive(true);
        }
        catch
        {
            Debug.LogError("Meal Component not found");
        }

    }
}
