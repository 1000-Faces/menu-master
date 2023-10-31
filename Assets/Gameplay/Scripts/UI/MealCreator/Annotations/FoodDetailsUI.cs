using TMPro;
using UnityEngine;

public class FoodDetailsUI : ARAnnotationWindow
{
    [SerializeField] TextMeshProUGUI m_SelectionText;
    [SerializeField] TextMeshProUGUI m_PriceText;
    [SerializeField] FoodMenuUI m_FoodMenuUI;

    private MealComponent m_MealComponent;

    public FoodData Food { get; set; }

    private void OnEnable()
    {
        // get meal component object from the parent
        m_MealComponent = GetComponentInParent<MealComponent>();

        if (m_MealComponent.IsFood)
        {
            // Load the food details
            Load(m_MealComponent.Food);
        }
    }

    private void Load(FoodData food)
    {
        // Load the food details
        Food = food;

        // Set the title
        Title = Food.category.ToString();

        if (Food)
        {
            // Set details
            m_SelectionText.text = Food.foodName;
            m_PriceText.text = Food.price.ToString();
        }
    }

    public void Open(FoodData food)
    {
        base.Open();

        // Load the food details
        Load(food);
    }

    public override void Close(int state)
    {
        if (state != 0)
        {
            // Close the food menu if it is open
            m_FoodMenuUI.Close(state);
        }

        base.Close(state);
    }

    public override void OnSubmit()
    {
        m_FoodMenuUI.Open();

        base.OnSubmit();
    }
}
