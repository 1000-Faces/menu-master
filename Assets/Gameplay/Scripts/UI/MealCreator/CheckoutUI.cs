using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckoutUI : FormWindow
{
    [SerializeField] MealDataStore m_DataStore;
    [SerializeField] GameObject m_FoodListItemTemplate;
    [SerializeField] Transform m_FoodListItemContainer;
    [SerializeField] TextMeshProUGUI m_OrderSumText;
    [SerializeField] Button m_SubmitButton;

    private readonly List<FoodListItem> m_FoodListItemList = new();

    private float m_OrderSum;

    public float OrderSum
    {
        get => m_OrderSum;
        private set
        {
            m_OrderSum = value;
            // change the text
            SetOrderSumText(value);
        }
    }

    private void Awake()
    {
        // Set the submit button
        m_SubmitButton.onClick.AddListener(OnSubmit);
    }

    private void OnEnable()
    {
        // Initialize the list of food list items for the category
        foreach (var item in m_DataStore.FoodObjects)
        {
            // Instantiate
            CreateFoodlistItem(item.Food);
        }

        // get the order sum
        OrderSum = m_DataStore.GetTotalPrice();

        // enable / disable the submit button on the list count
        m_SubmitButton.interactable = m_FoodListItemList.Count != 0;
    }

    private void OnDisable()
    {
        // check food list item list is not empty
        if (m_FoodListItemList.Count != 0)
        {
            // Clear the list
            m_FoodListItemList.ForEach(listItem => Destroy(listItem.gameObject));
            m_FoodListItemList.Clear();
        }
    }

    private void CreateFoodlistItem(FoodData food)
    {
        // Instantiate GameObject using the template
        GameObject foodListItemObject = Instantiate(m_FoodListItemTemplate, m_FoodListItemContainer);
        FoodListItem foodListItem = foodListItemObject.GetComponent<FoodListItem>();
        foodListItemObject.name = "ListItem: " + food.foodName;

        // Instantiate FoodListItem Data
        foodListItem.Load(food, food.category.ToString());

        // push list item to the list
        m_FoodListItemList.Add(foodListItem);
    }

    private void SetOrderSumText(float value)
    {
        // set the order sum
        m_OrderSumText.text = "LKR " + value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US"));
    }

    public override void OnSubmit()
    {
        // TODO: Implement the checkout logic

        base.OnSubmit();
    }
}
