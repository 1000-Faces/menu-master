using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserWindow : FormWindow
{
    [SerializeField] TextMeshProUGUI m_UsernameText;
    [SerializeField] RawImage m_UserAvatar;
    
    private void OnEnable()
    {
        // Get the user from the UserAuthentication singleton
        User user = UserAuthentication.Instance.GetUser();
        if (user != null)
        {
            m_UsernameText.text = user.username;
            // Load the user's avatar from the URL
            StartCoroutine(DownloadImage(user.photoUrl));
        }
    }

    IEnumerator DownloadImage(Uri MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            m_UserAvatar.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    public override void OnSubmit()
    {
        // Logout user
        UserAuthentication.Instance.SignOut();

        base.OnSubmit();
    }
}
