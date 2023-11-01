using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandButton : MonoBehaviour
{
    [SerializeField] FormWindow m_LoginWindow;

    public void OnClick()
    {
        // check if the user is logged in
        if (UserAuthentication.Instance.IsLoggedIn)
        {
            // if the user is logged in, then we render the next scene
            GetComponent<SceneLoader>().LoadScene();
        }
        else
        {
            // if the user is not logged in, then open the login window
            m_LoginWindow.Open();
        }
    }
}
