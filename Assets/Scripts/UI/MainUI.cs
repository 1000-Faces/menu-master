using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnAdd;

    private void Start()
    {
        // make sure the add button is not visible at the start
        btnAdd.gameObject.SetActive(false);
    }

    public void OnMenuClicked()
    {
        Utils.ShowToastMessage("Menu Opened!");
    }
}
