using DineEase.Meal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public class FoodListItem : MonoBehaviour
    {
        [SerializeField] FoodSO m_Food;

        public FoodSO Food
        {
            get => m_Food;
            set
            {
                m_Food = value;
                SetFoodData(m_Food);
            }
        }

        [SerializeField] Image m_Image;

        [SerializeField] TextMeshProUGUI m_NameText;

        [SerializeField] TextMeshProUGUI m_DescriptionText;

        [SerializeField] TextMeshProUGUI m_PriceText;

        void Start()
        {
            SetFoodData(m_Food);
        }
        
        void SetFoodData(FoodSO data)
        {
            if (data == null)
                return;

            m_Image.sprite = data.foodIcon;
            m_NameText.text = data.name;
            m_DescriptionText.text = data.description;
            m_PriceText.text = data.price.ToString();
        }
    }
}
