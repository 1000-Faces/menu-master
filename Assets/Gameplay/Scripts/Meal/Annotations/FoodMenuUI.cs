using DineEase.Meal;
using DineEase.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.Meal.Annotation
{
    public class FoodMenuUI : ARAnnotationWindow
    {
        [SerializeField] MealComponent m_MealComponent;
        [SerializeField] GameObject m_FoodListItemTemplate;
        [SerializeField] Transform m_FoodListItemContainer;
        [SerializeField] Button m_SubmitButton;
        [SerializeField] FoodDetailsUI m_FoodDetailsUI;
        
        readonly List<FoodListItem> m_FoodListItemList = new();

        FoodListItem m_SelectedFoodListItem;

        protected override void Awake()
        {
            base.Awake();

            // Set the submit button
            m_SubmitButton.onClick.AddListener(OnSubmit);
        }

        private void OnEnable()
        {
            // Set the title
            Title = $"{m_MealComponent.Category}: Food list";

            // Initialize the list of food list items for the category
            LoadFoodList().ForEach(food =>
            {
                if (food.category == m_MealComponent.Category)
                {
                    // Instantiate
                    CreateFoodlistItem(food);
                }
            });

            // Select the food item on the list if available
            m_SelectedFoodListItem = SelectFoodListItem(GetFoodListItem(m_MealComponent.Food));

            // If active toggle is null, disable the submit button
            if (m_SelectedFoodListItem == null)
            {
                m_SubmitButton.interactable = false;
            }
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

        private List<FoodSO> LoadFoodList()
        {
            // Load the food list from resources
            return Resources.LoadAll<FoodSO>("Foods/ScriptableObjects").ToList();
        }

        private void CreateFoodlistItem(FoodSO food)
        {
            // Instantiate GameObject using the template
            GameObject foodListItemObject = Instantiate(m_FoodListItemTemplate, m_FoodListItemContainer);
            FoodListItem foodListItem = foodListItemObject.GetComponent<FoodListItem>();
            foodListItemObject.name = "ListItem: " + food.foodName;

            // Set the toggle
            Toggle toggle = foodListItemObject.GetComponent<Toggle>();
            toggle.group = m_FoodListItemContainer.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener((bool value) =>
            {
                if (value)
                {
                    m_SelectedFoodListItem = foodListItem;
                    m_SubmitButton.interactable = true;
                }
            });

            // Instantiate FoodListItem Data
            foodListItem.Load(food);

            // Chack availability of the food
            if (!food.isAvailable)
            {
                // Disable the toggle
                toggle.interactable = false;
            }

            // push list item to the list
            m_FoodListItemList.Add(foodListItem);
        }

        private FoodListItem GetFoodListItem(FoodSO food)
        {
            if (food == null) return null;

            return m_FoodListItemList.Where(listItm => listItm.Food == food).FirstOrDefault();
        }

        private FoodListItem SelectFoodListItem(FoodListItem foodListItem = null)
        {
            if (foodListItem != null)
            {
                // Select the current food item if available
                foodListItem.gameObject.GetComponent<Toggle>().isOn = true;
                return foodListItem;
            }
            else
            {
                foodListItem = m_FoodListItemList.FirstOrDefault();
                if (foodListItem == null) return null;

                // Select the first item on the list if available
                foodListItem.gameObject.GetComponent<Toggle>().isOn = true;
                return foodListItem;
            }
        }

        public override void OnSubmit()
        {
            // Change the food selection in the meal component
            m_MealComponent.ChangeFood(m_SelectedFoodListItem.Food);

            // Open the Food Details UI if its closed
            if (!m_FoodDetailsUI.IsOpened)
            {
                m_FoodDetailsUI.Open(m_SelectedFoodListItem.Food);
            }

            base.OnSubmit();
        }
    }
}
