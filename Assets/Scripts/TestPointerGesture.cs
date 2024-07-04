using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion; // Ensure this namespace is included

public class TestPointerGesture : MonoBehaviour
{
    private ManoGestureContinuous lastGesture1;

    void Start()
    {
        // Check if Manomotion SDK is initialized
        if (ManomotionManager.Instance != null)
        {
            Debug.Log("Manomotion SDK initialized successfully");
        }
        else
        {
            Debug.LogError("Manomotion SDK initialization failed");
        }
    }

    void Update()
    {
        // Detect continuous gestures using Manomotion SDK
        GestureInfo gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        lastGesture1 = gestureInfo.mano_gesture_continuous;

        // Log the detected continuous gesture
        if (lastGesture1 == ManoGestureContinuous.OPEN_PINCH_GESTURE)
        {
            Debug.Log("Pointer Gesture Detected");
        }
    }
}
