using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodVisual : MonoBehaviour, IObjectLoader <string, FoodSO>
{
    private Transform _currentObject;

    [SerializeField] private List<FoodSO> swappableObjects = new();

    public List<FoodSO> SwappableObjects => swappableObjects;

    public void AddObject(FoodSO obj)
    {
        if (!swappableObjects.Contains(obj))
        {
            swappableObjects.Add(obj);
        }
    }

    public void LoadObject(string name)
    {
        try
        {
            var obj = swappableObjects.Where(obj => obj.foodName == name).FirstOrDefault();
            // Instantiate the object
            _currentObject = Instantiate(obj.prefab, transform);
        }
        catch
        {
            Debug.LogError("Food not found");
        }
    }

    public void SwapObject(string name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject.gameObject);
        }

        // spawn the new object
        LoadObject(name);
    }
}
