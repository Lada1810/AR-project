using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AppConfig", menuName = "App/AppConfig")]
public class AppConfig : ScriptableObject
{
    public Content[] content;
}

[Serializable]
public class Content
{
    public string id;
    public string person;
    public string textContent;
}
