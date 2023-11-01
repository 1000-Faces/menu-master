using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginWindow : FormWindow
{
    [SerializeField] TMP_InputField m_EmailText;
    [SerializeField] TMP_InputField m_PasswordText;

    public override void OnSubmit()
    {
        // Login user
        UserAuthentication.Instance.SignIn(m_EmailText.text, m_PasswordText.text);

        base.OnSubmit();
    }
}
