using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CollectOrderSum : MonoBehaviour
{
    [SerializeField] DataStore m_DataStore;
    [SerializeField] TextMeshProUGUI m_OrderSumText;

    float m_OrderSum;

    public float OrderSum { 
        get => m_OrderSum;
        private set
        {
            m_OrderSum = value;
            // change the text
            SetOrderSumText(value);
        }
    }

    private void Start()
    {
        // subscribe to the food changed event
        m_DataStore.FoodListChangeEvent += OnFoodListChange;
    }

    private void OnFoodListChange(object sender, EventArgs e)
    {
        OrderSum = m_DataStore.GetTotalPrice();
    }

    private void SetOrderSumText(float value)
    {
        // set the order sum
        m_OrderSumText.text = "LKR " + value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US"));
    }
}
