using Google.MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDisplay : MonoBehaviour
{
    [SerializeField] GameObject m_UsernameObject;
    [SerializeField] FormWindow m_UserWindow;
    [SerializeField] FormWindow m_LoginWindow;

    void Start()
    {
        m_UsernameObject.SetActive(false);

        // Subscribe to the UserChanged event
        UserAuthentication.Instance.UserChanged += OnUserChanged;
    }

    private void OnUserChanged(object sender, EventArgs e)
    {
        User user = UserAuthentication.Instance.GetUser();

        // If the user is null, hide the username object
        if (user == null)
        {
            m_UsernameObject.SetActive(false);
            return;
        }
        else
        {
            m_UsernameObject.SetActive(true);
            // Set the username text to the user's username
            GetUsername(UserAuthentication.Instance.GetUser().email);
        }
    }

    public void OnUserButtonClicked()
    {
        // If the user is logged in, show the user window
        if (UserAuthentication.Instance.IsLoggedIn)
        {
            m_UserWindow.Open();
        }
        // If the user is not logged in, show the login window
        else
        {
            m_LoginWindow.Open();
        }
    }

    public void GetUsername(string email)
    {
        StartCoroutine(HttpRequest.GetRequest("https://dineaase.azurewebsites.net/api/user/get?email=" + HttpRequest.MakeEmailString(email), (string jsonResponse) =>
        {
            if (jsonResponse == null)
            {
                Debug.LogError("Error: jsonResponse is null");
                return;
            }

            // Parse the JSON response into a dictionary.
            var dict = Json.Deserialize(jsonResponse) as Dictionary<string, object>;

            // Get the user's username
            string username = dict["username"] as string;

            // Set the username text to the user's username
            m_UsernameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = username;
        }));
    }
}
