using UnityEngine;
using UnityEngine.EventSystems;

public class MapUserWay : MonoBehaviour
{ 
    public Material selectedMaterial;
    public Material unselectedMaterial;
    public Material userPositionMaterial;


    private string prevRoomName = "";
    private string prelastTrackedClassName = "";

    public void ShowTargetRoom()
    {
        GameObject changeAreaComponent;

        if(prevRoomName!="")
        {
            changeAreaComponent = GameObject.Find(prevRoomName);
            changeAreaComponent.GetComponent<MeshRenderer>().material = unselectedMaterial;
        }

        string roomName = EventSystem.current.currentSelectedGameObject.name.TrimEnd('B');
        prevRoomName = roomName;
        changeAreaComponent = GameObject.Find(roomName);
        
        MeshRenderer meshRenderer = changeAreaComponent.GetComponent<MeshRenderer>();
        meshRenderer.material = selectedMaterial;
    }

    public void ShowLastPosition(string lastTrackedClassName)
    {
        if(prelastTrackedClassName != "")
        {
            GameObject.Find(prelastTrackedClassName).GetComponent<MeshRenderer>().material = unselectedMaterial;
        }
        if (lastTrackedClassName.StartsWith("it"))
        {
            GameObject.Find(lastTrackedClassName.ToUpper()).GetComponent<MeshRenderer>().material = userPositionMaterial;
            prelastTrackedClassName = lastTrackedClassName.ToUpper();
        }
    }
}
