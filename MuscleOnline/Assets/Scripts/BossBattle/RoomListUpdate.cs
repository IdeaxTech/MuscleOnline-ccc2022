using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;

public class RoomListUpdate : MonoBehaviourPunCallbacks
{

    public static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    void Start()
    {
        if (PhotonNetwork.IsConnected == false)
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Update Room List");
        Debug.Log(roomList);
        UpdateCachedRoomList(roomList);
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }
}
