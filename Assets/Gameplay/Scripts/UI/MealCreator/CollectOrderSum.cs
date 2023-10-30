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

    public void OnFoodListChange()
    {
        OrderSum = m_DataStore.GetTotalPrice();
    }

    private void SetOrderSumText(float value)
    {
        // set the order sum
        m_OrderSumText.text = "LKR " + value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US"));
    }
}
