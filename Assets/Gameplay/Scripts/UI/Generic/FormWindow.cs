using System;
using TMPro;
using UnityEngine;


public class FormResponse : EventArgs
{
    /// <summary>
    /// Message box response: 0 = exit on success, !0 = exit on failure
    /// </summary>
    public int Response { get; set; }
}

public class FormWindow : MonoBehaviour
{
    public static event EventHandler<FormResponse> FormResponseEvent;

    [SerializeField] private TextMeshProUGUI m_Title;

    public string Title
    {
        get => m_Title.text;
        set => m_Title.text = value;
    }

    public bool IsOpened => gameObject.activeSelf;

    public Action OnCloseExtraFunction { get; set; }

    public virtual void Open()
    {
        // show the message box
        gameObject.SetActive(true);
    }

    public virtual void Open(string title)
    {
        // set the title
        m_Title.text = title;

        Open();
    }

    protected virtual void Hide()
    {
        // hide the message box
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Hide the message box
    /// </summary>
    /// <param name="state">0 = terminate, 1 = close in success</param>
    public virtual void Close(int state)
    {
        Hide();

        // call the extra function if available
        OnCloseExtraFunction?.Invoke();

        // fire the event. The message box is closed after this operation is complete / incomplete
        FormResponseEvent?.Invoke(this, new FormResponse { Response = state });
    }

    public virtual void OnSubmit()
    {
        Hide();

        // fire the event. The message box is closed after this operation is completed
        FormResponseEvent?.Invoke(this, new FormResponse { Response = 0 });
    }
}
