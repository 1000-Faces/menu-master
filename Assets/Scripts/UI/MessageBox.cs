using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageTitle;
    [SerializeField] private TextMeshProUGUI messageContent;
    [SerializeField] private Button messageOKButton;
    [SerializeField] private Button messageCloseButton;

    public Action OnCloseExtraFunction { get; set; }

    public void OnClose()
    {
        // hide the message box
        gameObject.SetActive(false);

        // For debug
        Utils.ShowToastMessage("Message Box Closed!");

        // call the extra function if available
        OnCloseExtraFunction?.Invoke();
    }
}
