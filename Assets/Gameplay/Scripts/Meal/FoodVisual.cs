using UnityEngine;

public class FoodVisual : MonoBehaviour, IObjectLoader<Transform>
{
    private Transform _currentObject;

    public void LoadObject(Transform obj)
    {
        // Instantiate the object
        _currentObject = Instantiate(obj, transform);
    }

    public void SwapObject(Transform obj)
    {
        // If the object exists, remove it
        if (_currentObject != null)
        {
            Destroy(_currentObject.gameObject);
        }

        // spawn the new object
        LoadObject(obj);
    }
}
