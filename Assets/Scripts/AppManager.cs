using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AppManager : MonoBehaviour
{
    [SerializeField] private AppConfig appConfig;
    [SerializeField] private GameObject id;
    [SerializeField] private GameObject author;
    [SerializeField] private GameObject text;


    private Dictionary<string, int> pictures = new Dictionary<string, int>()
    {
        {"Impress-1-Human", 0 },
        {"Impress-1-Robot", 1 }
    };
    
    public void LoadResources(string className)
    {
        int number = 0;
        try
        {
            number = GetClassNumber(className);
            appConfig = Resources.Load<AppConfig>("Data/ClassConfig");
        }
        catch (Exception)
        {
            number = pictures[className];
            appConfig = Resources.Load<AppConfig>("Data/PictureConfig");
        }
        finally
        {
            var idText = id.GetComponent<TextMeshProUGUI>();
            var authorName = author.GetComponent<TextMeshProUGUI>();
            var textCont = text.GetComponent<TextMeshProUGUI>();
            idText.text = appConfig.content[number].id;
            authorName.text = appConfig.content[number].person;
            textCont.text = appConfig.content[number].textContent;
        }
    }

    private int GetClassNumber(string className)
    {
        int number = Convert.ToInt32(className.Substring(className.IndexOf("-") + 1));
        return number;
    }
}

