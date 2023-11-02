using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : FormWindow
{
    [SerializeField] TMP_InputField m_EmailText;
    [SerializeField] TMP_InputField m_PasswordText;
    [SerializeField] TMP_InputField m_ConfirmPasswordText;

    public override void OnSubmit()
    {
        // Register user
        UserAuthentication.Instance.CreateAccount(m_EmailText.text, m_PasswordText.text);

        base.OnSubmit();
    }
}
