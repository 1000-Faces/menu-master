using UnityEngine;


[CreateAssetMenu(fileName = "NewFood", menuName = "ScriptableObjects/Food")]
public class FoodData : ScriptableObject
{
    public string guid;

    public string foodName;

    public string description;

    public MealCategory category;

    public float price;

    public Sprite foodIcon;

    public Transform prefab;

    public bool isAvailable;

    public bool requirePlatform = false;
}
