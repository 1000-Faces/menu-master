using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : FormWindow
{
    [SerializeField] InputField m_EmailText;
    [SerializeField] InputField m_PasswordText;

    public override void OnSubmit()
    {
        // Login user
        UserAuthentication.Instance.SignIn(m_EmailText.text, m_PasswordText.text);

        base.OnSubmit();
    }
}
