using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DineEase
{
    public enum MealCategory
    {
        Unknown,
        MainCourse,
        SideDish,
        Beverage,
        Dessert,
    }

    public static class Utils
    {


        public static void ShowToastMessage(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
#else
            Debug.Log("Toast Message: " + message);
#endif
        }
    }
}

