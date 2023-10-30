using DineEase.Utilities;
using System.Collections;
using UnityEngine;

public class InterctionManager : MonoBehaviour
{
    private const float DOUBLE_TAP_TIME_THRESHOLD = 1.0f;

    private bool backButtonPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!backButtonPressed)
            {
                backButtonPressed = true;
                AndroidUtilities.ShowToastMessage("Press again to Exit");
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
        yield return new WaitForSeconds(DOUBLE_TAP_TIME_THRESHOLD);
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
