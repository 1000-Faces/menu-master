using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest
{
    public static string MakeEmailString(string email)
    {
        return email.Split('@')[0] + "%40" + email.Split('@')[1];
    }

    public static IEnumerator GetRequest(string uri, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the response.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Handle errors
                Debug.LogError("Error: " + webRequest.error);
                callback(null); // Notify the callback with a null value on error
            }
            else if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Request was successful, and you can access the JSON response as a string.
                string jsonResponse = webRequest.downloadHandler.text;

                // Pass the JSON response to the callback for further processing.
                callback(jsonResponse);
            }
        }
    }

    public static IEnumerator PostRequest(string uri, string jsonData, Action<string> callback)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, jsonData, "application/json");

        // Request and wait for the response.
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.DataProcessingError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Handle errors
            Debug.LogError("Error: " + webRequest.error);
            callback(null); // Notify the callback with a null value on error
        }
        else if (webRequest.result == UnityWebRequest.Result.Success)
        {
            // Request was successful, and you can access the response as a string.
            string response = webRequest.downloadHandler.text;

            // Pass the response to the callback for further processing.
            callback(response);
        }
    }
}
