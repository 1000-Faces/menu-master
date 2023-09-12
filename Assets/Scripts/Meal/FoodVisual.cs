using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodVisual : MonoBehaviour, IObjectSwapper <string, FoodSO>
{
    private GameObject _currentObject;

    [SerializeField] private List<FoodSO> swappableObjects = new();

    public List<FoodSO> SwappableObjects => swappableObjects;

    public void AddObject(FoodSO obj)
    {
        if (!swappableObjects.Contains(obj))
        {
            swappableObjects.Add(obj);
        }
    }

    public void SwapObject(string name)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject);
        }

        // spawn the new object
        try
        {
            var obj = swappableObjects.Where(obj => obj.foodName == name).FirstOrDefault();
            // Instantiate the object
            _currentObject = Instantiate(obj.prefab.gameObject, transform);
            _currentObject.transform.localPosition = Vector3.zero;
        }
        catch
        {
            Debug.LogError("Food not found");
        }
    }
}
