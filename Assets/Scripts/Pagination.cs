using UnityEngine;
using UnityEngine.UI;

public class Pagination : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject content;
    
    private Material material;
    private float stepsCount;
    private float currentIndex;

    void Start()
    {
        material = GetComponent<Image>().material;

        stepsCount = content.transform.childCount;
        currentIndex = scrollRect.horizontalNormalizedPosition * (stepsCount-1);
        
        material.SetFloat("_count", stepsCount);
        material.SetFloat("_index", currentIndex);
    }

    void Update()
    {
        currentIndex = scrollRect.horizontalNormalizedPosition * (stepsCount-1);
        material.SetFloat("_index", currentIndex);
    }
}