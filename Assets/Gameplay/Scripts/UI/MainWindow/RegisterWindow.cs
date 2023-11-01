using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : FormWindow
{
    [SerializeField] InputField m_EmailText;
    [SerializeField] InputField m_PasswordText;
    [SerializeField] InputField m_ConfirmPasswordText;

    public override void OnSubmit()
    {
        // Register user
        UserAuthentication.Instance.CreateAccount(m_EmailText.text, m_PasswordText.text);

        base.OnSubmit();
    }
}
