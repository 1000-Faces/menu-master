using DineEase.Meal;

namespace DineEase.UI.HUD
{
    internal interface ILookMealComponent
    {
        void OnMealSelectionChanged(object sender, MealSelectionChangedEventArgs e);
    }
}
