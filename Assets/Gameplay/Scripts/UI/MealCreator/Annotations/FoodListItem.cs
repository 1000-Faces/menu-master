using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodListItem : MonoBehaviour
{
    public FoodData Food { get; set; }

    [SerializeField] Image m_Image;

    public Sprite Image { get => m_Image.sprite; set => m_Image.sprite = value; }

    [SerializeField] TextMeshProUGUI m_NameText;

    public string Name { get => m_NameText.text; set => m_NameText.text = value; }

    [SerializeField] TextMeshProUGUI m_DescriptionText;

    public string Description { get => m_DescriptionText.text; set => m_DescriptionText.text = value; }

    [SerializeField] TextMeshProUGUI m_PriceText;

    public string Price { get => m_PriceText.text; set => m_PriceText.text = value; }

    public void Load(FoodData food)
    {
        Load(food, food.description);
    }

    public void Load(FoodData food, string description)
    {
        Food = food;
        Image = food.foodIcon;
        Name = food.foodName;
        Description = description;
        Price = "LKR " + food.price;
    }
}