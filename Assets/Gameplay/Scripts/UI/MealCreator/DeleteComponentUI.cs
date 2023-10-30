using UnityEngine;

public class DeleteComponentUI : FormWindow
{
    [SerializeField] DataStore m_DataStore;

    MealComponent m_MealComponent;

    private void OnEnable()
    {
        m_MealComponent = m_DataStore.SelectedComponent;
    }

    public override void OnSubmit()
    {
        if (m_MealComponent) Destroy(m_MealComponent.gameObject);
        Debug.Log($"Deleted {m_MealComponent}");

        base.OnSubmit();
    }
}
