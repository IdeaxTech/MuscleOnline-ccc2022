using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDisplayOperation : MonoBehaviour
{
    [SerializeField] GameObject GetRoomObject;

    public void ActiveGetRoomObject()
    {
        Debug.Log("Create!");
        //GetRoomObject.SetActive(true);
        GetRoom.DisplayRoom();
    }

    public void DeactiveGetRoomObject()
    {
        Debug.Log("Destory!");
        Destroy(GameObject.FindWithTag("RoomObject"));
        //GetRoomObject.SetActive(false);
    }
}
