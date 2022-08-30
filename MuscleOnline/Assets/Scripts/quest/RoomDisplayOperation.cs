using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDisplayOperation : MonoBehaviour
{
    [SerializeField] GameObject GetRoomObject;
    [SerializeField] GameObject MakeObject;
    [SerializeField] GameObject JoinObject;

    public void ActiveGetRoomObject()
    {
        Debug.Log("Create!");
        //GetRoomObject.SetActive(true);
        GetRoom.DisplayRoom();
    }

    public void DeactiveGetRoomObject()
    {
        Debug.Log("Destory!");
        GameObject[] RoomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
        foreach(GameObject RoomObject in RoomObjects)
        {
            Destroy(RoomObject);
        }
        //GetRoomObject.SetActive(false);
    }

    public void CloseCreateRoom()
    {
        //GameObject CreateRoomObject = GameObject.Find("PanelMake");
        //CreateRoomObject.SetActive(false);

        MakeObject.SetActive(false);
        JoinObject.SetActive(true);

        Debug.Log("Destory!");
        GameObject[] RoomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
        foreach (GameObject RoomObject in RoomObjects)
        {
            Destroy(RoomObject);
        }


    }
}
