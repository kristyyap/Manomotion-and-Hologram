using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;

public class SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        // Ensure the ManoMotionManager is active
        if (ManomotionManager.Instance != null && !ManomotionManager.Instance.gameObject.activeInHierarchy)
        {
            ManomotionManager.Instance.gameObject.SetActive(true);
        }

        // Ensure the ManoMotion canvas is active
        GameObject manoMotionCanvas = GameObject.Find("ManoMotionCanvas");
        if (manoMotionCanvas != null && !manoMotionCanvas.activeInHierarchy)
        {
            manoMotionCanvas.SetActive(true);
        }
    }
}
