using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DineEase.UI
{
    public class InterctionManager : MonoBehaviour
    {
        private bool backButtonPressed = false;
        private readonly float doubleTapTimeThreshold = 1.0f; // Adjust as needed.

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!backButtonPressed)
                {
                    backButtonPressed = true;
                    Utils.ShowToastMessage("Press again to Exit!");
                    StartCoroutine(ResetBackButtonPressed());
                }
                else
                {
                    // Double-tap detected, trigger the quit application logic.
                    QuitApplication();
                }
            }
        }

        private IEnumerator ResetBackButtonPressed()
        {
            yield return new WaitForSeconds(doubleTapTimeThreshold);
            backButtonPressed = false;
        }

        private void QuitApplication()
        {
            // This function quits the application.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
