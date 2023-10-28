using DineEase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace DineEase.UI
{
    public class MessageBoxResponse : EventArgs
    {
        /// <summary>
        /// Message box response: 0 = close, 1 = submit
        /// </summary>
        public int Response { get; set; }
    }
    
    public class MessageBox : MonoBehaviour
    {
        public static event EventHandler<MessageBoxResponse> OnMessageBoxResponseEvent;

        [SerializeField] private TextMeshProUGUI messageTitle;

        public Action OnCloseExtraFunction { get; set; }

        public void Show(string title, string message)
        {
            // set the title
            messageTitle.text = title;

            // set the message
            transform.Find("Message").GetComponent<TextMeshProUGUI>().text = message;

            // show the message box
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            // hide the message box
            gameObject.SetActive(false);
        }

        public virtual void OnSubmit()
        {
            Hide();

            // fire the event. The message box is closed after this operation is completed
            OnMessageBoxResponseEvent?.Invoke(this, new MessageBoxResponse { Response = 1 });
        }
        
        /// <summary>
        /// Hide the message box
        /// </summary>
        /// <param name="state">0 = terminate, 1 = close in success</param>
        public virtual void OnClose(int state)
        {
            Hide();

            // call the extra function if available
            OnCloseExtraFunction?.Invoke();

            // fire the event. The message box is closed after this operation is complete / incomplete
            OnMessageBoxResponseEvent?.Invoke(this, new MessageBoxResponse { Response = state });
        }
    }
}
