using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;
using UnityEngine.SceneManagement; // Add this namespace

public class SwipeToChangeAnimal : MonoBehaviour
{
    public GameObject[] animals; // Array to hold the animal GameObjects
    public Camera mainCamera; // The camera that will focus on the animals
    public float moveDistance = 1.5f; // Distance to move the camera for each swipe
    public float moveDuration = 0.5f; // Duration of the camera movement

    private int currentAnimalIndex = 0;
    private ManoGestureTrigger lastGesture;
    private ManoGestureContinuous lastGesture1;
    private Coroutine transitionCoroutine;
    private bool isViewingSingleAnimal = false; // Track if we are viewing a single animal

    void Start()
    {
        // Ensure the camera is focusing on the first animal initially
        FocusOnAnimal(currentAnimalIndex);
    }

    void Update()
    {
        // Detect swipe gestures using Manomotion SDK
        GestureInfo gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        lastGesture = gestureInfo.mano_gesture_trigger;

        if(!isViewingSingleAnimal)
        {
            if (lastGesture == ManoGestureTrigger.SWIPE_LEFT)
            {
                ChangeAnimal(-1);
            }
            else if (lastGesture == ManoGestureTrigger.SWIPE_RIGHT)
            {
                ChangeAnimal(1);
            }
            else if (lastGesture == ManoGestureTrigger.GRAB_GESTURE)
            {
                // Point gesture to select the current animal
                SelectAnimal(currentAnimalIndex);
            }
        }
        /*else
        { 
            if (lastGesture == ManoGestureTrigger.GRAB_GESTURE)
            {
                // Point gesture to go back to the swiping view
                GoBackToSwipe();
            }
        }*/
    }

    void UpdateCameraPosition(float moveDistance)
    {
        Vector3 targetPosition = mainCamera.transform.position + new Vector3(moveDistance, 0, 0);

        // Stop the current transition if it is still running
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        transitionCoroutine = StartCoroutine(SmoothTransition(targetPosition, mainCamera.transform.rotation));
    }

    void FocusOnAnimal(int index)
    {
        Vector3 animalPosition = animals[index].transform.position;
        mainCamera.transform.position = new Vector3(animalPosition.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    void ChangeAnimal(int direction)
    {
        currentAnimalIndex += direction;
        if (currentAnimalIndex < 0)
        {
            currentAnimalIndex = animals.Length - 1;
        }
        else if (currentAnimalIndex >= animals.Length)
        {
            currentAnimalIndex = 0;
        }
        UpdateCameraPosition(moveDistance * direction);
    }

    void SelectAnimal(int index)
    {
        //PlayerPrefs.SetInt("SelectedAnimalIndex", index);
        Debug.Log("Selected Animal: " + animals[index].name);

        // Set the state to viewing a single animal
        isViewingSingleAnimal = true;

        // Load the scene corresponding to the selected animal
        LoadAnimalScene(index);
    }

    /*void GoBackToSwipe()
    {

        Debug.Log("Returning to swiping view");

        // Set the state to swiping view
        isViewingSingleAnimal = false;

        string sceneName1 = "TEWCPL_Scene";

        // SceneManager.LoadScene(sceneName1);
        if (Application.CanStreamedLevelBeLoaded(sceneName1))
        {
            SceneManager.LoadScene(sceneName1);
        }
        else
        {
            Debug.LogWarning("Scene " + sceneName1 + " not found!");
        }
    }*/

    void LoadAnimalScene(int index)
    {
        // Assuming each animal has a corresponding scene named "AnimalSceneX" where X is the animal index
        string sceneName = "AnimalScene" + index;

        // SceneManager.LoadScene(sceneName);
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene " + sceneName + " not found!");
        }
    }

    IEnumerator SmoothTransition(Vector3 targetPosition, Quaternion targetRotation)
    {
        float time = 0;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        while(time<1)
        {
            time += Time.deltaTime / moveDuration;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
    }
}