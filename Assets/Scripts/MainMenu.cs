using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ManoMotion;

public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;  // Expose a public field for the camera
    private GameObject manoMotionCanvas;

    private void Start()
    {
        // Ensure the ManoMotionManager is initialized
        if (ManomotionManager.Instance == null)
        {
            Debug.LogError("ManomotionManager instance is not initialized. Make sure ManoMotion components are properly set up.");
            return;
        }

        // Prevent ManoMotionManager from being destroyed on scene load
        DontDestroyOnLoad(ManomotionManager.Instance.gameObject);

        // Initialize ManoMotionManager if needed
        if (!ManomotionManager.Instance.gameObject.activeInHierarchy)
        {
            ManomotionManager.Instance.gameObject.SetActive(true);
        }

        // Initialize the ManoMotion canvas
        manoMotionCanvas = GameObject.Find("ManoMotionCanvas");
        if (manoMotionCanvas == null)
        {
            Debug.LogError("ManoMotion canvas not found. Please ensure it is in the scene.");
            return;
        }

        DontDestroyOnLoad(manoMotionCanvas);
    }

    private void Update()
    {
        // Ensure ManoMotion Manager instance is not null
        if (ManomotionManager.Instance == null)
        {
            Debug.LogWarning("ManomotionManager instance is null.");
            return;
        }

        // Ensure hand information is available
        if (ManomotionManager.Instance.Hand_infos == null || ManomotionManager.Instance.Hand_infos.Length == 0)
        {
            return;
        }

        // Check if the current gesture is a 'Point'
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == ManoGestureContinuous.OPEN_PINCH_GESTURE)
        {
            // Load the next scene
            Debug.LogWarning("open pinch detected");
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        // Load the next scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Deactivate the main camera (optional)
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }
    }
}

