using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using TMPro;


[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField]
    private MapUserWay mapUserWay;

    private ARTrackedImageManager trackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    private GameObject newARObject;
    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject arObject in arObjectsToPlace)
        {
            newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
            newARObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private static bool IsInList(string name, List<ARTrackedImage> imgs)
    {
        foreach (var img in imgs)
        {
            if (img.referenceImage.name == name &&
                img.trackingState == TrackingState.Tracking)
            {
                return true;
            }
        }

        return false;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var arObject in arObjects)
        {
            if (IsInList(arObject.Key, eventArgs.added) ||
                IsInList(arObject.Key, eventArgs.updated))
            {
                continue;
            }
            arObject.Value.SetActive(false);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            imageTrackedText.text = trackedImage.referenceImage.name;
            mapUserWay.ShowLastPosition(imageTrackedText.text);
            AppManager appManager;
            if (imageTrackedText.text.StartsWith("it"))
            {
                appManager = arObjects[imageTrackedText.text].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<AppManager>();
            }
            else
            {
                appManager = arObjects[imageTrackedText.text].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<AppManager>();
            }
            appManager.LoadResources(imageTrackedText.text);
            arObjects[trackedImage.referenceImage.name].SetActive(true);
            AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
        }
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (arObjectsToPlace != null)
        {
            GameObject goARObject = arObjects[name];
            goARObject.transform.position = newPosition;
            goARObject.transform.localScale = scaleFactor;
        }
    }
}